// work out how to connect to github
// need a name for this thing!
// add green up and red down indicators on relevant values by comparing new vs prev?
// exception handling for retrieving JSON data
// improve layout. Bigger block size?
// 'about' screen

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;


namespace DigiClock
{

    public partial class Form1 : Form
    {
        
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]  // needed for the code that moves the form
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")] // needed for the code that moves the form
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public Form1()
        {
            InitializeComponent();
        }

        private int intCountdownToRefresh = 10; // countdown to display seconds to next refresh

        private void timer_Tick(object sender, EventArgs e) // update the calendar time and date
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm");
            lblSeconds.Text = DateTime.Now.ToString("ss");
            lblDate.Text = DateTime.Now.ToString("MMMM dd yyyy");
            lblDay.Text = DateTime.Now.ToString("dddd");
            lblSeconds.Location = new Point(lblTime.Location.X + lblTime.Width-10, lblSeconds.Location.Y); // place the seconds according to the width of the minutes/seconds (lblTime)
            lblSecsCountdown.Text = Convert.ToString(intCountdownToRefresh); // update the countdown on the form
            intCountdownToRefresh--; // reduce the countdown by 1 (second)
            
            if (intCountdownToRefresh == 0) // if the counter has reached zero
            {
                intCountdownToRefresh = 20; // reset it
            }
            lblSecsCountdown.Location = new Point(lblStatusMessPart1.Location.X + lblStatusMessPart1.Width - 8, lblSecsCountdown.Location.Y); // place the countdown according to the width of the status message
            if (intCountdownToRefresh < 19) // if more than a second has expired since the data from the blocktimer was refreshed...
            {
                if (lblStatusLight.ForeColor != Color.DarkRed && lblStatusLight.ForeColor != Color.Green) // check whether a data refresh has just occured to see if a status light flash needs dimming
                {
                    if (lblStatusLight.ForeColor == Color.Lime) // successful data refresh has occured
                    {
                        lblStatusLight.ForeColor = Color.Green; // reset the colours to a duller version to give appearance of a flash
                    }
                    else // an error must have just occured
                    {
                        lblStatusLight.ForeColor = Color.DarkRed; // reset the colours to a duller version to give appearance of a flash
                        if (intCountdownToRefresh < 11) // after 10 seconds...
                        {
                            lblErrorMessage.Text = ""; // hide the error message after 10 seconds
                            lblAlert.Text = ""; // and hide the alert icon
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) // on form loading
        {
            updateFields(); // set the initial btc data values to avoid waiting 10 secs for the first clock ticks
            updateJSON(); // set the initial btc JSON values to avoid waiting 10 secs for the first clock ticks
            timerForClock.Start(); // timer used to refresh the clock values
            timerForBlocks.Start(); // timer used to refresh the btc data
        }

        private void btnExit_Click(object sender, EventArgs e) // exit
        {
            this.Close();
        }

        private void btnExit_MouseHover(object sender, EventArgs e) // colour the exit form control on hover
        {
            btnExit.BackColor = System.Drawing.ColorTranslator.FromHtml("#6C411D");
        }

        private void btnExit_MouseLeave(object sender, EventArgs e) // reset colour of exit form control after hover
        {
            btnExit.BackColor = System.Drawing.ColorTranslator.FromHtml("#1D1D1D");
        }

        private void timerForBlocks_Tick(object sender, EventArgs e) // call the function to update the btc fields
        {
            updateFields();
            updateJSON();
        }

        public void updateJSON()
        {   
            // FEES
            var client = new HttpClient();
            var response = client.GetAsync("https://bitcoinexplorer.org/api/mempool/fees").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var data = JObject.Parse(json);
            var firstField = (string)data["nextBlock"];
            lblfeesNextBlock.Text = firstField;
            var secondField = (string)data["30min"];
            lblFees30Mins.Text = secondField;
            var thirdField = (string)data["60min"];
            lblFees60Mins.Text = thirdField;
            var fourthfield = (string)data["1day"];
            lblFees1Day.Text = fourthfield;

            // NEXT BLOCK
            var response2 = client.GetAsync("https://bitcoinexplorer.org/api/mining/next-block").Result;
            var json2 = response2.Content.ReadAsStringAsync().Result;
            var data2 = JObject.Parse(json2);
            var firstField2 = (string)data2["txCount"];
            lblTransInNextBlock.Text = firstField2;
            var secondField2 = (string)data2["minFeeRate"];
            double valuetoround = Convert.ToDouble(secondField2);
            double roundedValue = Math.Round(valuetoround, 2);
            lblNextBlockMinFee.Text = Convert.ToString(roundedValue);
            var thirdField2 = (string)data2["maxFeeRate"];
            valuetoround = Convert.ToDouble(thirdField2);
            roundedValue = Math.Round(valuetoround, 2);
            lblNextBlockMaxFee.Text = Convert.ToString(roundedValue);
            var fourthField2 = (string)data2["totalFees"];
            valuetoround = Convert.ToDouble(fourthField2);
            roundedValue = Math.Round(valuetoround, 2);
            lblNextBlockTotalFees.Text = Convert.ToString(roundedValue);
        }

        public void updateFields()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    lblAlert.Text = ""; // clear any error message
                    lblErrorMessage.Text = ""; // clear any error message
                    lblPriceUSD.Text = client.DownloadString("https://bitcoinexplorer.org/api/price/usd");
                    lblMoscowTime.Text = client.DownloadString("https://bitcoinexplorer.org/api/price/usd/sats");
                    lblMarketCapUSD.Text = client.DownloadString("https://bitcoinexplorer.org/api/price/usd/marketcap");
                    lblDifficultyAdjEst.Text = client.DownloadString("https://bitcoinexplorer.org/api/mining/diff-adj-estimate");
                    lblTXInMempool.Text = client.DownloadString("https://bitcoinexplorer.org/api/mempool/count");
                    lblAvgNoTransactions.Text = client.DownloadString("https://blockchain.info/q/avgtxnumber");
                    lblBlockNumber.Text = client.DownloadString("https://blockchain.info/q/getblockcount");
                    lblBlockReward.Text = client.DownloadString("https://blockchain.info/q/bcperblock");
                    lblEstHashrate.Text = client.DownloadString("https://blockchain.info/q/hashrate");
                    var TotalBTC = client.DownloadString("https://blockchain.info/q/totalbc"); // convert number of circulating sats into bitcoin
                    double dblTotalBTC = Convert.ToDouble(TotalBTC);
                    dblTotalBTC = dblTotalBTC / 100000000;
                    lblBTCInCirc.Text = Convert.ToString(dblTotalBTC);
                    lblHashesToSolve.Text = client.DownloadString("https://blockchain.info/q/hashestowin");
                    lblNextDifficultyChange.Text = client.DownloadString("https://blockchain.info/q/nextretarget");
                    lbl24HourTransCount.Text = client.DownloadString("https://blockchain.info/q/24hrtransactioncount");
                    var TwentFourHrBTCSent = client.DownloadString("https://blockchain.info/q/24hrbtcsent"); // convert number of sats sent in 24 hours to bitcoin
                    double dbl24HrBTCSent = Convert.ToDouble(TwentFourHrBTCSent);
                    dbl24HrBTCSent = dbl24HrBTCSent / 100000000;
                    lbl24HourBTCSent.Text = Convert.ToString(dbl24HrBTCSent);

                    intCountdownToRefresh = 20;
                    lblStatusLight.ForeColor = Color.Lime;
                    lblStatusLight.Text = "🟢"; // green light
                    lblStatusMessPart1.Text = "Data updated successfully. Refreshing in ";
                }
                catch (Exception ex)
                {
                    intCountdownToRefresh = 20;
                    lblAlert.Text = "⚠️";
                    lblErrorMessage.Text = ex.Message;
                    lblStatusLight.ForeColor = Color.Red;
                    lblStatusLight.Text = "🔴"; // red light
                    lblStatusMessPart1.Text = "One or more fields failed to update. Trying again in ";
                }
            }
        }

        private void btnMinimise_Click(object sender, EventArgs e) // minimise the form
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMinimise_MouseHover(object sender, EventArgs e) // colour the minimise form control on hover
        {
            btnMinimise.BackColor = System.Drawing.ColorTranslator.FromHtml("#6C411D");
        }

        private void btnMinimise_MouseLeave(object sender, EventArgs e) // reset colour of minimise form control after hover
        {
            btnMinimise.BackColor = System.Drawing.ColorTranslator.FromHtml("#1D1D1D");
        }

        private void btnMoveWindow_MouseHover(object sender, EventArgs e) // colour the move form control on hover
        {
            btnMoveWindow.BackColor = System.Drawing.ColorTranslator.FromHtml("#6C411D");
        }

        private void btnMoveWindow_MouseLeave(object sender, EventArgs e) // reset colour of move form control after hover
        {
            btnMoveWindow.BackColor = System.Drawing.ColorTranslator.FromHtml("#1D1D1D");
        }

        private void btnMoveWindow_MouseDown(object sender, MouseEventArgs e) // move the form when the move control is used
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMoveWindow_MouseUp(object sender, MouseEventArgs e) // reset colour of the move form control
        {
            btnMoveWindow.BackColor = System.Drawing.ColorTranslator.FromHtml("#1D1D1D");
        }

        private void Form1_Paint(object sender, PaintEventArgs e) // place a 1px border around the form
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
        }
    }
}

