/* 
────██──██─────  ╔═╗┌┬┐┌─┐┌┬┐┌─┐┌┬┐┌─┐┌┬┐┌─┐
███████████▄───  ╚═╗ │ ├─┤ │ ├─┤││││ │ │ │ │
──███████████▄─  ╚═╝ ┴ ┴ ┴ ┴ ┴ ┴┴ ┴└─┘ ┴ └─┘
──███────▀████─  Version history
──███──────███─  1.0 initial release
──███────▄███▀─  1.1 Used threading on API calls for speed and UI responsiveness. Added 4 new fields (number of hodling addresses, Blockchain size, 24 hour number of blocks mined, Number of discoverable nodes)
──█████████▀───      Added settings screen. Added ability to disable individual API calls. Added options to change API call refresh frequency. Added a 'last updated' timer.
──███████████▄─  
──███─────▀████  To do:
──███───────███  Add Lightning data
──███─────▄████  
──████████████─
████████████▀──
────██──██─────
*/

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
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Statamoto
{
    public partial class Statamoto : Form
    {
        //====================================================================================================================
        //---------------------------INITIALISE-------------------------------

        private int intDisplayCountdownToRefresh; // countdown in seconds to next refresh, for display only
        private int intAPIGroup1TimerIntervalMillisecsConstant; // milliseconds, used to reset the interval of the timer for api group1 refresh
        private int APIGroup1DisplayTimerIntervalSecsConstant; // seconds, used to reset the countdown display to its original number
        private int intAPIGroup2TimerIntervalMillisecsConstant; // milliseconds, used to reset the interval of the timer for api group2 refresh
        // booleans used to say whether to run individual API's or not. All on/true by default.
        private bool RunBitcoinExplorerEndpointAPI = true;
        private bool RunBlockchainInfoEndpointAPI = true;
        private bool RunBitcoinExplorerOrgJSONAPI = true;
        private bool RunBlockchainInfoJSONAPI = true;
        private bool RunCoingeckoComJSONAPI = true;
        private bool RunBlockchairComJSONAPI = true;
        private int APIGroup1RefreshFrequency = 1; // mins. Default value 1. Initial value only
        private int APIGroup2RefreshFrequency = 24; // hours. Default value 2. Initial value only
        private int intDisplaySecondsElapsedSinceUpdate = 0; // used to count seconds since the data was last refreshed, for display only.

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]  // needed for the code that moves the form as not using a standard control
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")] // needed for the code that moves the form as not using a standard control
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public Statamoto()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) // on form loading
        {
            updateAPIGroup1DataFields(); // setting them now avoids waiting a whole minute for the first refresh
            updateAPIGroup2DataFields(); // set the initial data for the daily updates to avoid waiting a whole day for the first data
            startTheClocksTicking(); // start all the timers
        }

        //--------------------------END INITIALISE--------------------------
        //====================================================================================================================
        // -------------------------CLOCK TICKS-----------------------------

        private void startTheClocksTicking()
        {
            intDisplayCountdownToRefresh = (APIGroup1RefreshFrequency * 60); //turn minutes into seconds. This is the number used to display remaning time until refresh
            APIGroup1DisplayTimerIntervalSecsConstant = (APIGroup1RefreshFrequency * 60); //turn minutes into seconds. This is kept constant and used to reset the timer to this number
            intAPIGroup1TimerIntervalMillisecsConstant = ((APIGroup1RefreshFrequency * 60) * 1000); // turn minutes into seconds, then into milliseconds
            timerAPIGroup1.Interval = intAPIGroup1TimerIntervalMillisecsConstant; // set the timer interval
            intAPIGroup2TimerIntervalMillisecsConstant = (((APIGroup2RefreshFrequency * 60) *60) * 1000);  // turn hours to minutes, then seconds, then milliseconds
            timerAPIGroup2.Interval = intAPIGroup2TimerIntervalMillisecsConstant; // set the time interval
            timer1Sec.Start(); // timer used to refresh the clock values
            timerAPIGroup1.Start(); // timer used to refresh most btc data
            timerAPIGroup2.Start(); // start timer for less frequent api calls
        }
        private void timer1Sec_Tick(object sender, EventArgs e) // update the calendar time and date
        {
            updateOnScreenClock();
            updateOnScreenCountdownAndFlashLights();
            intDisplaySecondsElapsedSinceUpdate ++; // increment displayed time elapsed since last update
            if (intDisplaySecondsElapsedSinceUpdate == 1)
            {
                lblElapsedSinceUpdate.Text = "Last updated " + intDisplaySecondsElapsedSinceUpdate.ToString() + " second ago";
            }
            else
            {
                lblElapsedSinceUpdate.Text = "Last updated " + intDisplaySecondsElapsedSinceUpdate.ToString() + " seconds ago";
            }
        }

        private void timerAPIGroup1_Tick(object sender, EventArgs e) // call the function to update the btc fields
        {
            clearAlertAndErrorMessage(); // wipe anything that may be showing in the error area (it should be empty anyway)
            updateAPIGroup1DataFields(); // fetch data and populate fields
        }

        private void timerAPIGroup2_Tick(object sender, EventArgs e)
        {
            updateAPIGroup2DataFields(); // fetch data and populate fields
        }

        //-------------------------END CLOCK TICKS-----------------------------
        //====================================================================================================================
        //-------------------------UPDATE FORM FIELDS---------------------------

        public async void updateAPIGroup1DataFields()
        {
            using (WebClient client = new WebClient())
            {
                bool errorOccurred = false;
                Task task1 = Task.Run(() =>  // call bitcoinexplorer.org endpoints and populate the fields on the form
                {
                    try
                    {
                        if (RunBitcoinExplorerEndpointAPI)
                        {
                            var result = bitcoinExplorerOrgEndpointsRefresh();
                            // move returned data to the labels on the form
                            lblPriceUSD.Invoke((MethodInvoker)delegate
                            {
                                lblPriceUSD.Text = result.priceUSD;
                            });
                            lblMoscowTime.Invoke((MethodInvoker)delegate
                            {
                                lblMoscowTime.Text = result.moscowTime;
                            });
                            lblMarketCapUSD.Invoke((MethodInvoker)delegate
                            {
                                lblMarketCapUSD.Text = result.marketCapUSD;
                            });
                            lblDifficultyAdjEst.Invoke((MethodInvoker)delegate
                            {
                                lblDifficultyAdjEst.Text = result.difficultyAdjEst;
                            });
                            lblTXInMempool.Invoke((MethodInvoker)delegate
                            {
                                lblTXInMempool.Text = result.txInMempool;
                            });
                        }
                        else
                        {
                            lblPriceUSD.Invoke((MethodInvoker)delegate
                            {
                                lblPriceUSD.Text = "disabled";
                            });
                            lblMoscowTime.Invoke((MethodInvoker)delegate
                            {
                                lblMoscowTime.Text = "disabled";
                            });
                            lblMarketCapUSD.Invoke((MethodInvoker)delegate
                            {
                                lblMarketCapUSD.Text = "disabled";
                            });
                            lblDifficultyAdjEst.Invoke((MethodInvoker)delegate
                            {
                                lblDifficultyAdjEst.Text = "disabled";
                            });
                            lblTXInMempool.Invoke((MethodInvoker)delegate
                            {
                                lblTXInMempool.Text = "disabled";
                            });
                        }
                        // set successful lights and messages on the form
                        lblStatusLight.Invoke((MethodInvoker)delegate
                        {
                            lblStatusLight.ForeColor = Color.Lime; // for a bright green flash
                        });
                        lblStatusLight.Invoke((MethodInvoker)delegate
                        {
                            lblStatusLight.Text = "🟢"; // circle/light
                        });
                        lblStatusMessPart1.Invoke((MethodInvoker)delegate
                        {
                            lblStatusMessPart1.Text = "Data updated successfully. Refreshing in ";
                        });
                        intDisplayCountdownToRefresh = APIGroup1DisplayTimerIntervalSecsConstant; // reset the timer
                    }
                    catch (Exception ex)
                    {
                        errorOccurred = true; 
                        lblErrorMessage.Invoke((MethodInvoker)delegate
                        {
                            lblErrorMessage.Text = ex.Message; // move returned error to the error message label on the form
                        });
                    }
                });

                Task task2 = Task.Run(() => // call blockchain.info endpoints and populate the fields on the form
                {
                    try
                    {
                        if (RunBlockchainInfoEndpointAPI)
                        {
                            var result2 = blockchainInfoEndpointsRefresh();
                            // move returned data to the labels on the form
                            lblAvgNoTransactions.Invoke((MethodInvoker)delegate
                            {
                                lblAvgNoTransactions.Text = result2.avgNoTransactions;
                            });
                            lblBlockNumber.Invoke((MethodInvoker)delegate
                            {
                                lblBlockNumber.Text = result2.blockNumber;
                            });
                            lblBlockReward.Invoke((MethodInvoker)delegate
                            {
                                lblBlockReward.Text = result2.blockReward;
                            });
                            lblEstHashrate.Invoke((MethodInvoker)delegate
                            {
                                lblEstHashrate.Text = result2.estHashrate;
                            });
                            lblAvgTimeBetweenBlocks.Invoke((MethodInvoker)delegate
                            {
                                lblAvgTimeBetweenBlocks.Text = result2.avgTimeBetweenBlocks;
                            });
                            lblBTCInCirc.Invoke((MethodInvoker)delegate
                            {
                                lblBTCInCirc.Text = result2.btcInCirc;
                            });
                            lblHashesToSolve.Invoke((MethodInvoker)delegate
                            {
                                lblHashesToSolve.Text = result2.hashesToSolve;
                            });
                            lbl24HourTransCount.Invoke((MethodInvoker)delegate
                            {
                                lbl24HourTransCount.Text = result2.twentyFourHourTransCount;
                            });
                            lbl24HourBTCSent.Invoke((MethodInvoker)delegate
                            {
                                lbl24HourBTCSent.Text = result2.twentyFourHourBTCSent;
                            });
                        }
                        else
                        {
                            lblAvgNoTransactions.Invoke((MethodInvoker)delegate
                            {
                                lblAvgNoTransactions.Text = "disabled";
                            });
                            lblBlockNumber.Invoke((MethodInvoker)delegate
                            {
                                lblBlockNumber.Text = "disabled";
                            });
                            lblBlockReward.Invoke((MethodInvoker)delegate
                            {
                                lblBlockReward.Text = "disabled";
                            });
                            lblEstHashrate.Invoke((MethodInvoker)delegate
                            {
                                lblEstHashrate.Text = "disabled";
                            });
                            lblAvgTimeBetweenBlocks.Invoke((MethodInvoker)delegate
                            {
                                lblAvgTimeBetweenBlocks.Text = "disabled";
                            });
                            lblBTCInCirc.Invoke((MethodInvoker)delegate
                            {
                                lblBTCInCirc.Text = "disabled";
                            });
                            lblHashesToSolve.Invoke((MethodInvoker)delegate
                            {
                                lblHashesToSolve.Text = "disabled";
                            });
                            lbl24HourTransCount.Invoke((MethodInvoker)delegate
                            {
                                lbl24HourTransCount.Text = "disabled";
                            });
                            lbl24HourBTCSent.Invoke((MethodInvoker)delegate
                            {
                                lbl24HourBTCSent.Text = "disabled";
                            });
                        }

                        // set successful lights and messages on the form
                        lblStatusLight.Invoke((MethodInvoker)delegate
                        {
                            lblStatusLight.ForeColor = Color.Lime; // for a bright green flash
                        });
                        lblStatusLight.Invoke((MethodInvoker)delegate
                        {
                            lblStatusLight.Text = "🟢"; // circle/light
                        });
                        lblStatusMessPart1.Invoke((MethodInvoker)delegate
                        {
                            lblStatusMessPart1.Text = "Data updated successfully. Refreshing in ";
                        });
                        intDisplayCountdownToRefresh = APIGroup1DisplayTimerIntervalSecsConstant; // reset the timer
                    }
                    catch (Exception ex)
                    {
                        errorOccurred = true;
                        lblErrorMessage.Invoke((MethodInvoker)delegate
                        {
                            lblErrorMessage.Text = ex.Message; // move returned error to the error message label on the form
                        });
                    }
                });

                Task task3 = Task.Run(() => // call Bitcoinexplorer.org JSON
                {
                    try
                    {
                        if (RunBitcoinExplorerOrgJSONAPI)
                        {
                            var result3 = bitcoinExplorerOrgJSONRefresh();
                            // move returned data to the labels on the form
                            lblfeesNextBlock.Invoke((MethodInvoker)delegate
                            {
                                lblfeesNextBlock.Text = result3.nextBlockFee;
                            });
                            lblFees30Mins.Invoke((MethodInvoker)delegate
                            {
                                lblFees30Mins.Text = result3.thirtyMinFee;
                            });
                            lblFees60Mins.Invoke((MethodInvoker)delegate
                            {
                                lblFees60Mins.Text = result3.sixtyMinFee;
                            });
                            lblFees1Day.Invoke((MethodInvoker)delegate
                            {
                                lblFees1Day.Text = result3.oneDayFee;
                            });
                            lblTransInNextBlock.Invoke((MethodInvoker)delegate
                            {
                                lblTransInNextBlock.Text = result3.txInNextBlock;
                            });
                            lblNextBlockMinFee.Invoke((MethodInvoker)delegate
                            {
                                lblNextBlockMinFee.Text = result3.nextBlockMinFee;
                            });
                            lblNextBlockMaxFee.Invoke((MethodInvoker)delegate
                            {
                                lblNextBlockMaxFee.Text = result3.nextBlockMaxFee;
                            });
                            lblNextBlockTotalFees.Invoke((MethodInvoker)delegate
                            {
                                lblNextBlockTotalFees.Text = result3.nextBlockTotalFees;
                            });
                        }
                        else
                        {
                            lblfeesNextBlock.Invoke((MethodInvoker)delegate
                            {
                                lblfeesNextBlock.Text = "n/a";
                            });
                            lblFees30Mins.Invoke((MethodInvoker)delegate
                            {
                                lblFees30Mins.Text = "n/a";
                            });
                            lblFees60Mins.Invoke((MethodInvoker)delegate
                            {
                                lblFees60Mins.Text = "n/a";
                            });
                            lblFees1Day.Invoke((MethodInvoker)delegate
                            {
                                lblFees1Day.Text = "n/a";
                            });
                            lblTransInNextBlock.Invoke((MethodInvoker)delegate
                            {
                                lblTransInNextBlock.Text = "disabled";
                            });
                            lblNextBlockMinFee.Invoke((MethodInvoker)delegate
                            {
                                lblNextBlockMinFee.Text = "disabled";
                            });
                            lblNextBlockMaxFee.Invoke((MethodInvoker)delegate
                            {
                                lblNextBlockMaxFee.Text = "disabled";
                            });
                            lblNextBlockTotalFees.Invoke((MethodInvoker)delegate
                            {
                                lblNextBlockTotalFees.Text = "disabled";
                            });

                        }
                        // set successful lights and messages on the form
                        lblStatusLight.Invoke((MethodInvoker)delegate
                        {
                            lblStatusLight.ForeColor = Color.Lime; // for a bright green flash
                        });
                        lblStatusLight.Invoke((MethodInvoker)delegate
                        {
                            lblStatusLight.Text = "🟢"; // circle/light
                        });
                        lblStatusMessPart1.Invoke((MethodInvoker)delegate
                        {
                            lblStatusMessPart1.Text = "Data updated successfully. Refreshing in ";
                        });
                        intDisplayCountdownToRefresh = APIGroup1DisplayTimerIntervalSecsConstant; // reset the timer
                    }
                    catch (Exception ex)
                    {
                        errorOccurred = true;
                        lblErrorMessage.Invoke((MethodInvoker)delegate
                        {
                            lblErrorMessage.Text = ex.Message; // move returned error to the error message label on the form
                        });
                    }
                });

                Task task4 = Task.Run(() => //call blockchain.info JSON
                {
                    try
                    {
                        if (RunBlockchainInfoJSONAPI)
                        {
                            var result4 = blockchainInfoJSONRefresh();
                            // move returned data to the labels on the form
                            lblTransactions.Invoke((MethodInvoker)delegate
                            {
                                lblTransactions.Text = result4.n_tx;
                            });
                            lblBlockSize.Invoke((MethodInvoker)delegate
                            {
                                lblBlockSize.Text = result4.size;
                            });
                            lblNextDifficultyChange.Invoke((MethodInvoker)delegate
                            {
                                lblNextDifficultyChange.Text = result4.nextretarget;
                            });
                        }
                        else
                        {
                            lblTransactions.Invoke((MethodInvoker)delegate
                            {
                                lblTransactions.Text = "disabled";
                            });
                            lblBlockSize.Invoke((MethodInvoker)delegate
                            {
                                lblBlockSize.Text = "disabled";
                            });
                            lblNextDifficultyChange.Invoke((MethodInvoker)delegate
                            {
                                lblNextDifficultyChange.Text = "disabled";
                            });

                        }
                        // set successful lights and messages on the form
                        lblStatusLight.Invoke((MethodInvoker)delegate
                        {
                            lblStatusLight.ForeColor = Color.Lime; // for a bright green flash
                        });
                        lblStatusLight.Invoke((MethodInvoker)delegate
                        {
                            lblStatusLight.Text = "🟢"; // circle/light
                        });
                        lblStatusMessPart1.Invoke((MethodInvoker)delegate
                        {
                            lblStatusMessPart1.Text = "Data updated successfully. Refreshing in ";
                        });
                        intDisplayCountdownToRefresh = APIGroup1DisplayTimerIntervalSecsConstant; // reset the timer
                    }
                    catch (Exception ex)
                    {
                        errorOccurred = true;
                        lblErrorMessage.Invoke((MethodInvoker)delegate
                        {
                            lblErrorMessage.Text = ex.Message; // move returned error to the error message label on the form
                        });
                    }
                });

                Task task5 = Task.Run(() => // call CoinGecko.com JSON
                {
                    try
                    {
                        if (RunCoingeckoComJSONAPI)
                        {
                            var result5 = coingeckoComJSONRefresh();
                            // move returned data to the labels on the form
                            lblATH.Invoke((MethodInvoker)delegate
                            {
                                lblATH.Text = result5.ath;
                            });
                            lblATHDate.Invoke((MethodInvoker)delegate
                            {
                                lblATHDate.Location = new Point(lblATH.Location.X + lblATH.Width, lblATHDate.Location.Y); // place the ATH date according to the width of the ATH (future proofed for hyperbitcoinization!)
                            });
                            lblATHDate.Invoke((MethodInvoker)delegate
                            {
                                lblATHDate.Text = "(" + result5.athDate + ")";
                            });
                            lblATHDifference.Invoke((MethodInvoker)delegate
                            {
                                lblATHDifference.Text = result5.athDifference;
                            });
                            lbl24HrHigh.Invoke((MethodInvoker)delegate
                            {
                                lbl24HrHigh.Text = result5.twentyFourHourHigh;
                            });
                            lbl24HrLow.Invoke((MethodInvoker)delegate
                            {
                                lbl24HrLow.Text = result5.twentyFourHourLow;
                            });
                        }
                        else
                        {
                            lblATH.Invoke((MethodInvoker)delegate
                            {
                                lblATH.Text = "disabled";
                            });
                            lblATHDate.Invoke((MethodInvoker)delegate
                            {
                                lblATHDate.Location = new Point(lblATH.Location.X + lblATH.Width, lblATHDate.Location.Y); // place the ATH date according to the width of the ATH (future proofed for hyperbitcoinization!)
                            });
                            lblATHDate.Invoke((MethodInvoker)delegate
                            {
                                lblATHDate.Text = "(" + "disabled" + ")";
                            });
                            lblATHDifference.Invoke((MethodInvoker)delegate
                            {
                                lblATHDifference.Text = "disabled";
                            });
                            lbl24HrHigh.Invoke((MethodInvoker)delegate
                            {
                                lbl24HrHigh.Text = "disabled";
                            });
                            lbl24HrLow.Invoke((MethodInvoker)delegate
                            {
                                lbl24HrLow.Text = "disabled";
                            });
                        }

                        // set successful lights and messages on the form
                        lblStatusLight.Invoke((MethodInvoker)delegate
                        {
                            lblStatusLight.ForeColor = Color.Lime; // for a bright green flash
                        });
                        lblStatusLight.Invoke((MethodInvoker)delegate
                        {
                            lblStatusLight.Text = "🟢"; // circle/light
                        });
                        lblStatusMessPart1.Invoke((MethodInvoker)delegate
                        {
                            lblStatusMessPart1.Text = "Data updated successfully. Refreshing in ";
                        });
                        intDisplayCountdownToRefresh = APIGroup1DisplayTimerIntervalSecsConstant; // reset the timer
                        intDisplaySecondsElapsedSinceUpdate = 0;
                    }
                    catch (Exception ex)
                    {
                        errorOccurred = true;
                        lblErrorMessage.Invoke((MethodInvoker)delegate
                        {
                            lblErrorMessage.Text = ex.Message; // move returned error to the error message label on the form
                        });
                    }
                });

                await Task.WhenAll(task1, task2, task3, task4, task5);

                // If any errors occurred with any of the API calls, a decent error message has already been displayed. Now display the red light and generic error.
                if (errorOccurred)
                {
                    intDisplayCountdownToRefresh = APIGroup1DisplayTimerIntervalSecsConstant;
                    lblAlert.Invoke((MethodInvoker)delegate
                    {
                        lblAlert.Text = "⚠️";
                    });
                    lblStatusLight.Invoke((MethodInvoker)delegate
                    {
                        lblStatusLight.ForeColor = Color.Red;
                    });
                    lblStatusLight.Invoke((MethodInvoker)delegate
                    {
                        lblStatusLight.Text = "🔴"; // red light
                    });
                    lblStatusMessPart1.Invoke((MethodInvoker)delegate
                    {
                        lblStatusMessPart1.Text = "One or more fields failed to update. Trying again in ";
                    });
                }
            }
        }

        private void updateAPIGroup2DataFields()
        {
            try
            {
                if (RunBlockchairComJSONAPI)
                {
                    var client = new HttpClient();
                    string json6 = client.GetStringAsync("https://api.blockchair.com/bitcoin/stats").Result;
                    dynamic data6 = JsonConvert.DeserializeObject(json6);
                    int hodling_addresses = data6.data.hodling_addresses;
                    if (hodling_addresses > 0) // this api sometimes doesn't populate this field with anything but 0
                    {
                        lblHodlingAddresses.Text = hodling_addresses.ToString();
                    }
                    else
                    {
                        lblHodlingAddresses.Text = "no data";
                    }
                    int blocksIn24Hours = data6.data.blocks_24h;
                    lblBlocksIn24Hours.Text = blocksIn24Hours.ToString();
                    int numberOfNodes = data6.data.nodes;
                    lblNodes.Text = numberOfNodes.ToString();
                    dynamic blockchainSize = data6.data.blockchain_size;
                    double blockchainSizeGB = Math.Round(Convert.ToDouble(blockchainSize) / 1073741824.0, 2);
                    lblBlockchainSize.Text = blockchainSizeGB.ToString();
                }
                else
                {
                    lblHodlingAddresses.Text = "disabled";
                    lblBlocksIn24Hours.Text = "disabled";
                    lblNodes.Text = "disabled";
                    lblBlockchainSize.Text = "disabled";
                }
            }
            catch (Exception ex)
            {
                lblAlert.Text = "⚠️";
                lblErrorMessage.Text = ex.Message;
                lblStatusLight.ForeColor = Color.Red;
                lblStatusLight.Text = "🔴"; // red light
                lblStatusMessPart1.Text = "One or more fields failed to update. Trying again in ";
            }
        }

        //--------------------------END UPDATE FORM FIELDS----------------------------
        //====================================================================================================================        
        //-------------------------- FORM NAVIGATION CONTROLS--------------------------

        private void btnSplash_Click(object sender, EventArgs e)
        {
            splash splash = new splash(); // invoke the about/splash screen
            splash.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e) // exit
        {
            this.Close();
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

        // Mousehover button effects for nav buttons
        private void button_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            button.BackColor = Color.Gray;
            button.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1D1D1D");
        }

        // Mouseleave button effects for nav buttons
        private void button_MouseLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            button.BackColor = System.Drawing.ColorTranslator.FromHtml("#1D1D1D");
            button.ForeColor = Color.Gray;
        }

        //-----------------------END FORM NAVIGATION CONTROLS--------------------------
        //====================================================================================================================
        //--------------------COUNTDOWN, ERROR MESSAGES AND STATUS LIGHTS--------------

        private void updateOnScreenCountdownAndFlashLights()
        {
            lblSecsCountdown.Text = Convert.ToString(intDisplayCountdownToRefresh); // update the countdown on the form
            intDisplayCountdownToRefresh--; // reduce the countdown of the 1 minute timer by 1 second
            if (intDisplayCountdownToRefresh <= 0) // if the 1 minute timer countdown has reached zero...
            {
                intDisplayCountdownToRefresh = APIGroup1DisplayTimerIntervalSecsConstant; // reset it
            }
            lblSecsCountdown.Location = new Point(lblStatusMessPart1.Location.X + lblStatusMessPart1.Width - 8, lblSecsCountdown.Location.Y); // place the countdown according to the width of the status message
            if (intDisplayCountdownToRefresh < (APIGroup1DisplayTimerIntervalSecsConstant - 1)) // if more than a second has expired since the data from the blocktimer was refreshed...
            {
                changeStatusLightAndClearErrorMessage();
            }
        }

        private void changeStatusLightAndClearErrorMessage()
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
                    if (intDisplayCountdownToRefresh < 11) // when there are only 10 seconds left until the refresh...
                    {
                        lblErrorMessage.Text = ""; // hide any previous error message
                        lblAlert.Text = ""; // and hide the alert icon
                    }
                }
            }
        }

        private void clearAlertAndErrorMessage()
        {
            lblAlert.Text = ""; // clear any error message
            lblErrorMessage.Text = ""; // clear any error message
        }

        //----------------END COUNTDOWN, ERROR MESSAGES AND STATUS LIGHTS--------------
        //====================================================================================================================
        //------------------------------------API CALLS----------------------------

        private (string priceUSD, string moscowTime, string marketCapUSD, string difficultyAdjEst, string txInMempool) bitcoinExplorerOrgEndpointsRefresh()
        {
            using (WebClient client = new WebClient())
            {
                string priceUSD = client.DownloadString("https://bitcoinexplorer.org/api/price/usd"); // 1 bitcoin = ? usd
                string moscowTime = client.DownloadString("https://bitcoinexplorer.org/api/price/usd/sats"); // 1 usd = ? sats
                string marketCapUSD = client.DownloadString("https://bitcoinexplorer.org/api/price/usd/marketcap"); // bitcoin market cap in usd
                string difficultyAdjEst = client.DownloadString("https://bitcoinexplorer.org/api/mining/diff-adj-estimate") + "%"; // difficulty adjustment as a percentage
                string txInMempool = client.DownloadString("https://bitcoinexplorer.org/api/mempool/count"); // total number of transactions in the mempool
                return (priceUSD, moscowTime, marketCapUSD, difficultyAdjEst, txInMempool);
            }
        }

        private (string avgNoTransactions, string blockNumber, string blockReward, string estHashrate, string avgTimeBetweenBlocks, string btcInCirc, string hashesToSolve, string twentyFourHourTransCount, string twentyFourHourBTCSent) blockchainInfoEndpointsRefresh()
        {
            using (WebClient client = new WebClient())
            {
                string avgNoTransactions = client.DownloadString("https://blockchain.info/q/avgtxnumber"); // average number of transactions in last 100 blocks (to about 6 decimal places!)
                double dblAvgNoTransactions = Convert.ToDouble(avgNoTransactions);
                dblAvgNoTransactions = Math.Round(dblAvgNoTransactions); // so lets get it down to an integer
                string avgNoTransactionsText = Convert.ToString(dblAvgNoTransactions);
                string blockNumber = client.DownloadString("https://blockchain.info/q/getblockcount"); // most recent block number
                string blockReward = client.DownloadString("https://blockchain.info/q/bcperblock"); // current block reward
                string estHashrate = client.DownloadString("https://blockchain.info/q/hashrate"); // hashrate estimate
                string secondsBetweenBlocks = client.DownloadString("https://blockchain.info/q/interval"); // average time between blocks in seconds
                double dblSecondsBetweenBlocks = Convert.ToDouble(secondsBetweenBlocks);
                TimeSpan time = TimeSpan.FromSeconds(dblSecondsBetweenBlocks);
                string timeString = string.Format("{0:%m}m {0:%s}s", time);
                string avgTimeBetweenBlocks = timeString;
                string totalBTC = client.DownloadString("https://blockchain.info/q/totalbc"); // total sats in circulation
                double dblTotalBTC = Convert.ToDouble(totalBTC);
                dblTotalBTC = dblTotalBTC / 100000000; // convert sats to bitcoin
                string btcInCirc = Convert.ToString(dblTotalBTC);
                string hashesToSolve = client.DownloadString("https://blockchain.info/q/hashestowin"); // avg number of hashes to win a block
                string twentyFourHourTransCount = client.DownloadString("https://blockchain.info/q/24hrtransactioncount"); // number of transactions in last 24 hours
                string twentyFourHourBTCSent = client.DownloadString("https://blockchain.info/q/24hrbtcsent"); // number of sats sent in 24 hours
                double dbl24HrBTCSent = Convert.ToDouble(twentyFourHourBTCSent);
                dbl24HrBTCSent = dbl24HrBTCSent / 100000000; // convert sats to bitcoin
                twentyFourHourBTCSent = Convert.ToString(dbl24HrBTCSent);
                return (avgNoTransactionsText, blockNumber, blockReward, estHashrate, avgTimeBetweenBlocks, btcInCirc, hashesToSolve, twentyFourHourTransCount, twentyFourHourBTCSent);
            }
        }

        private (string nextBlockFee, string thirtyMinFee, string sixtyMinFee, string oneDayFee, string txInNextBlock, string nextBlockMinFee, string nextBlockMaxFee, string nextBlockTotalFees) bitcoinExplorerOrgJSONRefresh()
        {
            // fees
            var client = new HttpClient();
            var response = client.GetAsync("https://bitcoinexplorer.org/api/mempool/fees").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var data = JObject.Parse(json);
            var nextBlockFee = (string)data["nextBlock"];
            var thirtyMinFee = (string)data["30min"];
            var sixtyMinFee = (string)data["60min"];
            var oneDayFee = (string)data["1day"];
            // next block
            var response2 = client.GetAsync("https://bitcoinexplorer.org/api/mining/next-block").Result;
            var json2 = response2.Content.ReadAsStringAsync().Result;
            var data2 = JObject.Parse(json2);
            var txInNextBlock = (string)data2["txCount"]; //transaction count
            var nextBlockMinFee = (string)data2["minFeeRate"]; // minimum fee rate
            double valuetoround = Convert.ToDouble(nextBlockMinFee); 
            double roundedValue = Math.Round(valuetoround, 2);
            nextBlockMinFee = Convert.ToString(roundedValue);
            var nextBlockMaxFee = (string)data2["maxFeeRate"]; // maximum fee rate
            valuetoround = Convert.ToDouble(nextBlockMaxFee);
            roundedValue = Math.Round(valuetoround, 2);
            nextBlockMaxFee = Convert.ToString(roundedValue);
            var nextBlockTotalFees = (string)data2["totalFees"]; // total fees
            valuetoround = Convert.ToDouble(nextBlockTotalFees);
            roundedValue = Math.Round(valuetoround, 2);
            nextBlockTotalFees = Convert.ToString(roundedValue);
            return (nextBlockFee, thirtyMinFee, sixtyMinFee, oneDayFee, txInNextBlock, nextBlockMinFee, nextBlockMaxFee, nextBlockTotalFees);
        }

        private (string n_tx, string size, string nextretarget) blockchainInfoJSONRefresh()
        {
            using (WebClient client = new WebClient())
            {
                // LATEST BLOCK
                string jsonurl = "https://blockchain.info/rawblock/";  // use this...
                string blockNumberUrl = "https://blockchain.info/q/getblockcount"; 
                string blocknumber = client.DownloadString(blockNumberUrl); //combined with the result of that (we can't rely on already knowing the latest block number)
                string finalurl = jsonurl + blocknumber; // to create a url we can use to get the JSON of the latest block
                string size;
                var response3 = client.DownloadString(finalurl);
                var data3 = JObject.Parse(response3); 
                var n_tx = (string)data3["n_tx"] + " transactions";  // number of transactions
                var sizeInKB = ((double)data3["size"] /1000); // size in bytes divided by 1000 to get kb
                if (sizeInKB < 1024) // if less than 1MB
                {
                    size = sizeInKB + " KB block size";
                }
                else // if more than 1MB
                {
                    size = Convert.ToString(Math.Round((sizeInKB / 1000), 2)) + "MB block size"; 
                }
                // NEXT DIFFICULTY ADJUSTMENT BLOCK
                var response4 = client.DownloadString("https://api.blockchain.info/stats");
                var data4 = JObject.Parse(response4);
                var nextretarget = (string)data4["nextretarget"];
                return (n_tx, size, nextretarget);
            }
        }

        private (string ath, string athDate, string athDifference, string twentyFourHourHigh, string twentyFourHourLow) coingeckoComJSONRefresh()
        {
            using (WebClient client = new WebClient())
            {
                // ATH & 24hr data
                var response5 = client.DownloadString("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=100&page=1&sparkline=false");
                var data5 = JArray.Parse(response5);
                var btcData = data5.Where(x => (string)x["symbol"] == "btc").FirstOrDefault();
                var ath = (string)btcData["ath"];  // all time high value of btc in usd
                var athDate = (string)btcData["ath_date"]; // date of the all time high
                DateTime date = DateTime.Parse(athDate); // change it to dd MMM yyyy format
                string strATHDate = date.ToString("dd MMM yyyy");
                athDate = strATHDate;
                double doubleathDifference = (double)btcData["ath_change_percentage"]; // percentage change from ATH to multiple decimal places
                string formattedAthDifference = doubleathDifference.ToString("0.00"); // round it to 2 decimal places before sending it back
                string athDifference = Convert.ToString(formattedAthDifference);

                var twentyFourHourHigh = (string)btcData["high_24h"]; // highest value of btc in usd over last 24 hours
                var twentyFourHourLow = (string)btcData["low_24h"]; // lowest value of btc in usd over last 24 hours
                return (ath, athDate, athDifference, twentyFourHourHigh, twentyFourHourLow);
            }
        }

        //--------------------------END API CALLS------------------------------
        //====================================================================================================================
        //--------------------------ON-SCREEN CLOCK----------------------------

        private void updateOnScreenClock()
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm");
            lblSeconds.Text = DateTime.Now.ToString("ss");
            lblDate.Text = DateTime.Now.ToString("MMMM dd yyyy");
            lblDay.Text = DateTime.Now.ToString("dddd");
            lblSeconds.Location = new Point(lblTime.Location.X + lblTime.Width - 10, lblSeconds.Location.Y); // place the seconds according to the width of the minutes/seconds (lblTime)
        }

        //----------------------END ON-SCREEN CLOCK----------------------------
        //====================================================================================================================
        //---------------------- BORDER ROUND WINDOW---------------------------

        private void Form1_Paint(object sender, PaintEventArgs e) // place a 1px border around the form
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
        }

        //---------------------END BORDER ROUND WINDOW--------------------------

        private void btnSettings_Click(object sender, EventArgs e)
        {
            settingsScreen.CreateInstance();
            settingsScreen.Instance.ShowDialog();
            // read all fields from the settings screen and set variables for use on the main form
            if (settingsScreen.Instance.BitcoinExplorerEndpointsEnabled)
            {
                RunBitcoinExplorerEndpointAPI = true;
            }
            else
            {
                RunBitcoinExplorerEndpointAPI = false;
            }
            if (settingsScreen.Instance.BlockchainInfoEndpointsEnabled)
            {
                RunBlockchainInfoEndpointAPI = true;
            }
            else
            {
                RunBlockchainInfoEndpointAPI = false;
            }
            if (settingsScreen.Instance.BitcoinExplorerOrgJSONEnabled)
            {
                RunBitcoinExplorerOrgJSONAPI = true;
            }
            else
            {
                RunBitcoinExplorerOrgJSONAPI = false;
            }
            if (settingsScreen.Instance.BlockchainInfoJSONEnabled)
            {
                RunBlockchainInfoJSONAPI = true;
            }
            else
            {
                RunBlockchainInfoJSONAPI = false;
            }
            if (settingsScreen.Instance.CoingeckoComJSONEnabled)
            {
                RunCoingeckoComJSONAPI = true;
            }
            else
            {
                RunCoingeckoComJSONAPI = false;
            }
            if (settingsScreen.Instance.BlockchairComJSONEnabled)
            {
                RunBlockchairComJSONAPI = true;
            }
            else
            {
                RunBlockchairComJSONAPI = false;
            }

            if (APIGroup1DisplayTimerIntervalSecsConstant != (settingsScreen.Instance.APIGroup1RefreshInMinsSelection * 60)) // if user has changed refresh frequency
            {
                APIGroup1DisplayTimerIntervalSecsConstant = settingsScreen.Instance.APIGroup1RefreshInMinsSelection * 60;
                intAPIGroup1TimerIntervalMillisecsConstant = ((settingsScreen.Instance.APIGroup1RefreshInMinsSelection * 60) * 1000);
                intDisplayCountdownToRefresh = APIGroup1DisplayTimerIntervalSecsConstant;
                timerAPIGroup1.Stop();    
                timerAPIGroup1.Interval = intAPIGroup1TimerIntervalMillisecsConstant;
                timerAPIGroup1.Start();
            }

            if (intAPIGroup2TimerIntervalMillisecsConstant != (((settingsScreen.Instance.APIGroup2RefreshInHoursSelection *60) * 60) * 1000))
            {
                intAPIGroup2TimerIntervalMillisecsConstant = (((settingsScreen.Instance.APIGroup2RefreshInHoursSelection * 60) * 60) * 1000);
                timerAPIGroup2.Stop();
                timerAPIGroup2.Interval = intAPIGroup2TimerIntervalMillisecsConstant;
                timerAPIGroup2.Start();
            }
            
        }
    }
}