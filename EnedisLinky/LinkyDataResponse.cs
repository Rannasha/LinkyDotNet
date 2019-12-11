using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EnedisLinky
{
    public enum LoginResult { OK, InvalidCredentials, UnspecifiedError };
    public enum DataResult { OK, NoActiveSession, LoginError, UnspecifiedError };

    // The final output of data requests. The Entries dictionary contains the data, with each entry
    // containing a date/time and a power consumption value.
    // The Status field returns the status of the request. For erroneous outcomes, the Entries dictionary
    // will be empty.
    public class LinkyData
    {
        public Dictionary<DateTime, double> Entries;
        public DataResult Status;

        public LinkyData(DataResult status) : this()
        {
            Status = status;
        }
        public LinkyData()
        {
            Entries = new Dictionary<DateTime, double>();
        }
    }

    // The following classes are modeled after the JSON output of the Enedis website.

    // The state value will contain either "terminee" to indicate success or "erreur" to indicate failure.
    // There is no detailed error information available (i.e. "date out of range" or stuff like that).
    // Presumably because this service was not intended for public consumption.
    public class LinkyDataState
    {
        [JsonProperty("valeur")]
        public string Value;
    }

    public class LinkyDataPeriod
    {
        [JsonProperty("dateFin")]
        public string DateEnd;
        [JsonProperty("dateDebut")]
        public string DateStart;
    }

    public class LinkyDataItem
    {
        [JsonProperty("valeur")]
        public double Value;
        [JsonProperty("ordre")]
        public int Order;
    }

    public class LinkyDataContents
    {
        [JsonProperty("decalage")]
        public int Offset;
        [JsonProperty("puissanceSouscrite")]
        public int SubscribedPower;
        [JsonProperty("periode")]
        public LinkyDataPeriod Period;
        [JsonProperty("data")]
        public List<LinkyDataItem> Data;
    }

    public class LinkyDataResponse
    {
        [JsonProperty("etat")]
        public LinkyDataState State;
        [JsonProperty("graphe")]
        public LinkyDataContents Data;
    }
}
