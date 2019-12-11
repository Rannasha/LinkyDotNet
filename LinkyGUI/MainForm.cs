using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.Web;
using EnedisLinky;

namespace LinkyGUI
{
    public partial class MainForm : Form
    {
        LinkyHandler linky;

        private void AppendToOutput(string txt)
        {
            tbOutput.Text = tbOutput.Text + txt + Environment.NewLine;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            linky = new LinkyHandler(tbUsername.Text, tbPassword.Text);

            LoginResult loginResult = linky.Login();

            AppendToOutput("Login result: " + loginResult.ToString());
        }

        private void btnDailyData_Click(object sender, EventArgs e)
        {
            LinkyData ld = linky.GetDailyData(dtpDailyStart.Value, dtpDailyEnd.Value);

            if (ld.Status != DataResult.OK)
            {
                AppendToOutput(ld.Status.ToString());
            }
            else
            {
                foreach (KeyValuePair<DateTime, double> kvp in ld.Entries)
                {
                    AppendToOutput(kvp.Key.ToString("yyyy-MM-dd") + " - " + kvp.Value.ToString() + " kWh");
                }
            }
        }

        private void btnHourlyData_Click(object sender, EventArgs e)
        {
            LinkyData ld = linky.GetHourlyData(dtpHourly.Value);

            if (ld.Status != DataResult.OK)
            {
                AppendToOutput(ld.Status.ToString());
            }
            else
            {
                foreach (KeyValuePair<DateTime, double> kvp in ld.Entries)
                {
                    AppendToOutput(kvp.Key.ToString("yyyy-MM-dd HH:mm") + " - " + kvp.Value.ToString() + " kWh");
                }
            }
        }

        private void btnClearTextBox_Click(object sender, EventArgs e)
        {
            tbOutput.Clear();
        }

        private void btnRawOutput_Click(object sender, EventArgs e)
        {
            AppendToOutput(linky.RawOutput);
        }
    }
}
