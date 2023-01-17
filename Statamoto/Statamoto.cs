
// add green up and red down indicators on relevant values by comparing new vs prev?
// improve layout. 
// disable all focus if possible. 


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


namespace Statamoto
{

    public partial class Statamoto : Form
    {
        
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]  // needed for the code that moves the form
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")] // needed for the code that moves the form
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public Statamoto()
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

        private void timerForBlocks_Tick(object sender, EventArgs e) // call the function to update the btc fields
        {
            updateFields();
            updateJSON();
        }

        public void updateJSON()
        {
            try
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
                var firstField2 = (string)data2["txCount"]; //transaction count
                lblTransInNextBlock.Text = firstField2;
                var secondField2 = (string)data2["minFeeRate"]; // minimum fee rate
                double valuetoround = Convert.ToDouble(secondField2);
                double roundedValue = Math.Round(valuetoround, 2);
                lblNextBlockMinFee.Text = Convert.ToString(roundedValue);
                var thirdField2 = (string)data2["maxFeeRate"]; // maximum fee rate
                valuetoround = Convert.ToDouble(thirdField2);
                roundedValue = Math.Round(valuetoround, 2);
                lblNextBlockMaxFee.Text = Convert.ToString(roundedValue);
                var fourthField2 = (string)data2["totalFees"]; // total fees
                valuetoround = Convert.ToDouble(fourthField2);
                roundedValue = Math.Round(valuetoround, 2);
                lblNextBlockTotalFees.Text = Convert.ToString(roundedValue);

                // LATEST BLOCK

                string jsonurl = "https://blockchain.info/rawblock/"; // use this...
                string blocknumber = lblBlockNumber.Text; // combined with current block number...
                string finalurl = jsonurl + blocknumber; // to build a url to the json feed

                var response3 = client.GetAsync(finalurl).Result;
                var json3 = response3.Content.ReadAsStringAsync().Result;
                var data3 = JObject.Parse(json3);
                var firstField3 = (string)data3["n_tx"]; // number of transactions
                lblTransactions.Text = Convert.ToString(firstField3) + " transactions";
                var secondField3 = ((double)data3["size"]/1000); // size in bytes divided by 1000 to get kb
                secondField3 = Math.Round(secondField3, 2); // round to 2 decimal places
                if (secondField3 < 1024) // if less than 1MB
                {
                    lblBlockSize.Text = Convert.ToString(secondField3) + " KB";
                }
                else 
                {
                    secondField3 = Math.Round((secondField3/1000), 2); // if more than 1MB
                    lblBlockSize.Text = Convert.ToString(secondField3) + " MB";
                }

                // NEXT DIFFICULTY ADJUSTMENT BLOCK

                var response4 = client.GetAsync("https://api.blockchain.info/stats").Result;
                var json4 = response4.Content.ReadAsStringAsync().Result;
                var data4 = JObject.Parse(json4);
                var firstField4 = (string)data4["nextretarget"];
                lblNextDifficultyChange.Text = Convert.ToString(firstField4);

                // ATH & 24hr data
                var response5 = client.GetAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=100&page=1&sparkline=false").Result;
                var json5 = response5.Content.ReadAsStringAsync().Result;
                var data5 = JArray.Parse(json5);
                var ath = data5.Where(x => (string)x["symbol"] == "btc").Select(x => (string)x["ath"]).FirstOrDefault();
                var athDate = data5.Where(x => (string)x["symbol"] == "btc").Select(x => (string)x["ath_date"]).FirstOrDefault();
                var athDifference = data5.Where(x => (string)x["symbol"] == "btc").Select(x => (string)x["ath_change_percentage"]).FirstOrDefault();
                var twentyFourHourHigh = data5.Where(x => (string)x["symbol"] == "btc").Select(x => (string)x["high_24h"]).FirstOrDefault();
                var twentyFourHourLow = data5.Where(x => (string)x["symbol"] == "btc").Select(x => (string)x["low_24h"]).FirstOrDefault();
                DateTime date = DateTime.Parse(athDate);
                string strATHDate = date.ToString("dd MMM yyyy");
                lblATH.Text = ath;
                lblATHDate.Location = new Point(lblATH.Location.X + lblATH.Width, lblATHDate.Location.Y); // place the ATH date according to the width of the ATH (future proofed for hyperbitcoinization!)
                lblATHDate.Text = "(" + strATHDate + ")";
                double dblATHDifference = Convert.ToDouble(athDifference);
                dblATHDifference = Math.Round(dblATHDifference, 2);
                lblATHDifference.Text = Convert.ToString(dblATHDifference) + "%";
                lbl24HrHigh.Text = twentyFourHourHigh;
                lbl24HrLow.Text = twentyFourHourLow;
            }
            catch (Exception ex)
            {
                intCountdownToRefresh = 20;
                lblAlert.Text = "⚠️";
                lblErrorMessage.Text = ex.Message;
                lblStatusLight.ForeColor = Color.Red;
                lblStatusLight.Text = "🔴"; // red light
                lblStatusMessPart1.Text = "A json error has occurred. Trying again in ";
            }
        }

        public void updateFields()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    lblAlert.Text = ""; // clear any error message
                    lblErrorMessage.Text = ""; // clear any error message
                    lblPriceUSD.Text = client.DownloadString("https://bitcoinexplorer.org/api/price/usd"); // price of 1BTC in USD
                    lblMoscowTime.Text = client.DownloadString("https://bitcoinexplorer.org/api/price/usd/sats"); // value of 1USD in sats
                    lblMarketCapUSD.Text = client.DownloadString("https://bitcoinexplorer.org/api/price/usd/marketcap"); // BTC marketcap
                    lblDifficultyAdjEst.Text = client.DownloadString("https://bitcoinexplorer.org/api/mining/diff-adj-estimate") + "%"; // next difficulty adjustment estimate as a percentage
                    lblTXInMempool.Text = client.DownloadString("https://bitcoinexplorer.org/api/mempool/count"); // transactions in mempool
                    var AvgNoTransactions = client.DownloadString("https://blockchain.info/q/avgtxnumber"); // average number of transactions in last 100 blocks (to about 6 decimal places!)
                    double dblAvgNoTransactions = Convert.ToDouble(AvgNoTransactions);
                    dblAvgNoTransactions = Math.Round(dblAvgNoTransactions); // so lets get it down to an integer
                    lblAvgNoTransactions.Text = Convert.ToString(dblAvgNoTransactions);
                    lblBlockNumber.Text = client.DownloadString("https://blockchain.info/q/getblockcount"); // most recent block number
                    lblBlockReward.Text = client.DownloadString("https://blockchain.info/q/bcperblock"); // current block reward
                    lblEstHashrate.Text = client.DownloadString("https://blockchain.info/q/hashrate"); // hashrate estimate
                    var secondsBetweenBlocks = client.DownloadString("https://blockchain.info/q/interval"); // average time between blocks in seconds
                    double dblSecondsBetweenBlocks = Convert.ToDouble(secondsBetweenBlocks);
                    TimeSpan time = TimeSpan.FromSeconds(dblSecondsBetweenBlocks);
                    string timeString = string.Format("{0:%m}m {0:%s}s", time);
                    lblAvgTimeBetweenBlocks.Text = timeString;

                    var TotalBTC = client.DownloadString("https://blockchain.info/q/totalbc"); // total sats in circulation
                    double dblTotalBTC = Convert.ToDouble(TotalBTC);
                    dblTotalBTC = dblTotalBTC / 100000000; // convert sats to bitcoin
                    lblBTCInCirc.Text = Convert.ToString(dblTotalBTC);
                    lblHashesToSolve.Text = client.DownloadString("https://blockchain.info/q/hashestowin"); // avg number of hashes to win a block
                    lbl24HourTransCount.Text = client.DownloadString("https://blockchain.info/q/24hrtransactioncount"); // number of transactions in last 24 hours
                    var TwentFourHrBTCSent = client.DownloadString("https://blockchain.info/q/24hrbtcsent"); // number of sats sent in 24 hours
                    double dbl24HrBTCSent = Convert.ToDouble(TwentFourHrBTCSent);
                    dbl24HrBTCSent = dbl24HrBTCSent / 100000000; // convert sats to bitcoin
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

        private void btnSplash_Click(object sender, EventArgs e)
        {
            splash splash = new splash(); // invoke the about/splash screen
            splash.ShowDialog();
        }

        // Mousehover button effects for all buttons
        private void button_MouseHover(object sender, EventArgs e) 
        {
            Button button = (Button)sender;
            button.BackColor = Color.Gray;
            button.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1D1D1D");
        }

        // Mouseleave button effects for all buttons
        private void button_MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = System.Drawing.ColorTranslator.FromHtml("#1D1D1D");
            button.ForeColor = Color.Gray;
        }
    }
}

