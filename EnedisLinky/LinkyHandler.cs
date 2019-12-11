using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;

namespace EnedisLinky
{
    public class LinkyHandler
    {
        // If true, the Login() method will be called automatically if a data request fails because
        // of an expired or non-existent session.
        public bool AutoLogin { get; set; }

        // Allows access to the raw output (the HTML page or the data JSON) after a query.
        public string RawOutput => rawOutputStr;

        private HttpClient client;

        private string username;
        private string password;
        private string rawOutputStr;

        // URL constants
        private const string HOST_LOGIN = "espace-client-connexion.enedis.fr";
        private const string HOST_DATA = "espace-client-particuliers.enedis.fr";

        private const string PATH_LOGIN = "/auth/UI/Login";
        private const string PATH_DATA = "/group/espace-particuliers/suivi-de-consommation";

        // Data modes
        private const string MODE_DAILY = "urlCdcJour";
        private const string MODE_HOURLY = "urlCdcHeure";

        // Sets the headers of 'msg'. Host should either be the login host (HOST_LOGIN) or the 
        // API host (HOST_DATA).
        private void SetHeaders(HttpRequestMessage msg, string host)
        {
            Dictionary<string, string> queryHeader = new Dictionary<string, string>()
            {
                { "Accept", "application/json, text/javascript, */*; q=0.01" },
                { "Accept-Language", "fr,fr-FR;q=0.8,en;q=0.6" },
                { "User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:59.0) Gecko/20100101 Firefox/59.0" },
                { "Origin", "https://espace-client-connexion.enedis.fr" },
                { "Referer", "https://espace-client-connexion.enedis.fr/auth/UI/Login?realm=particuliers&goto=https://espace-client-particuliers.enedis.fr/group/espace-particuliers/accueil" },
                { "Host", host }
            };
            foreach (KeyValuePair<string, string> kvp in queryHeader)
            {
                msg.Headers.Add(kvp.Key, kvp.Value);
            }
            msg.Headers.Add(HttpRequestHeader.ContentType.ToString(), "application/x-www-form-urlencoded");
        }

        // The application can't verify the authenticity of the SSL certificate. For now, we override
        // validation so that it always validates. A better solution is needed.
        private void DisableCertificateValidation()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        // Determine the outcome of a login attempt.
        private LoginResult ParseLoginResult(string respStr)
        {
            if (respStr.Contains("Votre identifiant ou mot de passe est incorrect."))
            {
                return LoginResult.InvalidCredentials;
            }
            if (respStr.Contains("Ma consommation d'électricité"))
            {
                return LoginResult.OK;
            }
            return LoginResult.UnspecifiedError;
        }

        // Check if we received valid data and if so, parse the returned JSON. Uses Newtonsoft.Json.
        private LinkyDataResponse ParseDataResult(string resStr)
        {
            if (resStr.Contains("function LoginSubmit(value)"))
            {
                // The server returned the login page instead of the requested data.
                // This probably means that the session has expired.
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<LinkyDataResponse>(resStr);
            }
        }

        // The core method to communicate with the Enedis website. The URL is composed of the host and path constants defined above,
        // the host variable is passed for the purpose of the request headers. The query parameters are added to the URL in the request
        // and the formData parameter contains the POST payload.
        private string SendRequest(string url, string host, Dictionary<string, string> queryParams, Dictionary<string, string> formData)
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(formData);
            string queryParamsStr = DictToString(queryParams);
            HttpRequestMessage msg = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url + (queryParamsStr.Length > 0 ? "?" + queryParamsStr : "")),
                Content = content
            };

            SetHeaders(msg, host);
            DisableCertificateValidation();

            HttpResponseMessage response = client.SendAsync(msg).Result;
            string resStr = response.Content.ReadAsStringAsync().Result;

            rawOutputStr = resStr;

            return resStr;
        }

        // Send a data request and parse the outcome. modeStr defines what type of data to obtain (daily, hourly).
        private LinkyDataResponse GetData(string modeStr, DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("End date can't be less than start date.");
            }

            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "p_p_id", "lincspartdisplaycdc_WAR_lincspartcdcportlet" },
                { "p_p_lifecycle", "2" },
                { "p_p_state", "normal" },
                { "p_p_mode", "view" },
                { "p_p_resource_id", modeStr },
                { "p_p_cacheability", "cacheLevelPage" },
                { "p_p_col_id", "column-1" },
                { "p_p_col_count", "2" }
            };
            Dictionary<string, string> formData = new Dictionary<string, string>()
            {
                { "_lincspartdisplaycdc_WAR_lincspartcdcportlet_dateDebut", FormatDateTime(startDate) },
                { "_lincspartdisplaycdc_WAR_lincspartcdcportlet_dateFin", FormatDateTime(endDate) }
            };

            string resStr = SendRequest("https://" + HOST_DATA + PATH_DATA, HOST_DATA, queryParams, formData);
            return ParseDataResult(resStr);
        }

        // Attempt to log in to the website.
        public LoginResult Login()
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "IDToken1", username },
                { "IDToken2", password },

                // base64 encoding of "https://espace-client-particuliers.enedis.fr/"
                { "goto" , "aHR0cHM6Ly9lc3BhY2UtY2xpZW50LXBhcnRpY3VsaWVycy5lbmVkaXMuZnIv" },
                { "gotoOnFail" , "" },

                // base64 encoding of "realm=particuliers"
                { "SunQueryParamsString", "cmVhbG09cGFydGljdWxpZXJz" },
                { "encoded", "true" },
                { "gx_charset", "UTF-8" }
            };

            string resStr = SendRequest("https://" + HOST_LOGIN + PATH_LOGIN, HOST_LOGIN, new Dictionary<string, string>(), queryParams);

            return ParseLoginResult(resStr);
        }

        // Attempt to request data for the specified mode (daily, hourly). If the attempt returns a session-related
        // error, attempt to log (back) in if AutoLogin is set to true.
        // Once the data is obtained and parsed, convert itinto the final output format, the LinkyData class.
        public LinkyData GetDataAndConvert(string mode, DateTime startDate, DateTime endDate)
        {
            LinkyDataResponse ldr = GetData(mode, startDate, endDate);

            // No active session: either not logged in or session expired.
            if (ldr == null)
            {
                // Automatic (re-)login requested
                if (AutoLogin)
                {
                    // Try logging in. If OK, try to fetch the data again.
                    if (Login() == LoginResult.OK)
                    {
                        ldr = GetData(mode, startDate, endDate);
                        // After logging back in, we still don't receive valid data. Something unknown is wrong.
                        if (ldr == null)
                        {
                            return new LinkyData(DataResult.UnspecifiedError);
                        }
                        // If the data is obtained, we'll roll out of this if-branch and proceed as if the data was obtained
                        // on the first attempt.
                    }
                    // We were not able to log back in.
                    else
                    {
                        return new LinkyData(DataResult.LoginError);
                    }
                }
                // No automatic (re-)login configured
                else
                {
                    return new LinkyData(DataResult.NoActiveSession);
                }
            }
            // We received some data back.

            // The server returned a data-JSON, but with an error. This could indicate something like the date being out of range,
            // but also one of several other causes.
            if (ldr.State.Value == "erreur")
            {
                return new LinkyData(DataResult.UnspecifiedError);
            }

            // Finally, we have data and no error-flag is set.
            LinkyData ld = new LinkyData(DataResult.OK);
            int offset = ldr.Data.Offset;

            for (int i = offset; i < ldr.Data.Data.Count; i++)
            {
                // The system always returns the same number of data points, empty points have values below zero, so we skip them.
                if (ldr.Data.Data[i].Order < 0 || ldr.Data.Data[i].Value < 0)
                {
                    break;
                }
                if (mode == MODE_DAILY)
                {
                    ld.Entries.Add(DateTime.Parse(ldr.Data.Period.DateStart).AddDays(i - offset), ldr.Data.Data[i].Value);
                }
                if (mode == MODE_HOURLY)
                {
                    // In hourly mode, the data returned is actually in half hour intervals.
                    // The values, instead of the total energy use in kWh like in daily mode, are actually the average
                    // power (in kW) during the half hour segment. To obtain the total energy used in each segment, we can simply divide the
                    // power by 2. This gives us the energy use in kWh.
                    ld.Entries.Add(DateTime.Parse(ldr.Data.Period.DateStart).AddMinutes(30 * (i - offset)), ldr.Data.Data[i].Value / 2);
                }
            }

            return ld;
        }

        // Wrapper for GetDataAndConvert for hourly data.
        public LinkyData GetHourlyData(DateTime date)
        {
            return GetDataAndConvert(MODE_HOURLY, date, date.AddDays(1));
        }

        // Wrapper for GetDataAndConvert for daily data.
        public LinkyData GetDailyData(DateTime startDate, DateTime endDate)
        {
            return GetDataAndConvert(MODE_DAILY, startDate, endDate);
        }

        public LinkyHandler(string username, string password)
        {
            this.username = username;
            this.password = password;

            client = new HttpClient();
        }

        // Utility methods
        private string FormatDateTime(DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy");
        }

        private string DictToString(Dictionary<string, string> dict)
        {
            bool first = true;
            string s = "";
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                s = s + (first ? "" : "&") + kvp.Key + "=" + HttpUtility.UrlEncode(kvp.Value);
                first = false;
            }

            return s;
        }
    }
}
