/* 
────██──██─────  ╔═╗┌┬┐┌─┐┌┬┐┌─┐┌┬┐┌─┐┌┬┐┌─┐
███████████▄───  ╚═╗ │ ├─┤ │ ├─┤││││ │ │ │ │
──███████████▄─  ╚═╝ ┴ ┴ ┴ ┴ ┴ ┴┴ ┴└─┘ ┴ └─┘
──███────▀████─  Version history 
──███──────███─  1.0 initial release
──███────▄███▀─  1.1 Used threading on API calls for speed and UI responsiveness. Added 4 new fields (number of hodling addresses, Blockchain size, 24 hour number of blocks mined, Number of discoverable nodes)
──█████████▀───      Added settings screen. Added ability to disable individual API calls. Added options to change API call refresh frequency. Added a 'last updated' timer.
──███████████▄─  1.2 Added Lightning stats, node rankings, etc. Hover behaviour on buttons much more responsive as is the UI in general. More data fields added. Threading bugs fixed. Various UI improvements.
──███─────▀████  1.3 Added address lookup for all address types via choice of API.
──███───────███  
──███─────▄████  
──████████████─  
████████████▀──  
────██──██─────
*/

/* Stuff to do:
 * transaction list - tidy up column headers & colours.
 * add balance change to transaction list.
 * add status (conf/unconf) to transaction list? Along with checkbox to retrieve conf/unconf/both? 
 * name change? OrangeBit?
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
using static System.Windows.Forms.LinkLabel;
using System.Globalization;
using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;
using NBitcoin.BouncyCastle.Math;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using NBitcoin.RPC;
using NBitcoin.Payment;
using QRCoder;
using System.Windows.Controls;
using ListViewItem = System.Windows.Forms.ListViewItem;
using System.Reflection;

namespace Statamoto
{
    public partial class Statamoto : Form
    {
        //=============================================================================================================
        //---------------------------INITIALISE------------------------------------------------------------------------
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
        private bool RunMempoolSpaceLightningAPI = true;
        private string NodeURL = "https://mempool.space/api/"; // default value. Can be changed by user.
        private int APIGroup1RefreshFrequency = 1; // mins. Default value 1. Initial value only
        private int APIGroup2RefreshFrequency = 24; // hours. Default value 2. Initial value only
        private int intDisplaySecondsElapsedSinceUpdate = 0; // used to count seconds since the data was last refreshed, for display only.
        private bool ObtainedHalveningSecondsRemainingYet = false; // used to check whether we know halvening seconds before we start trying to subtract from them
        private TransactionService _transactionService;
        private int TotalTransactionRowsAdded = 0;

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]  // needed for the code that moves the form as not using a standard control
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")] // needed for the code that moves the form as not using a standard control
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public Statamoto()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            _transactionService = new TransactionService(NodeURL);
        }

        private void Form1_Load(object sender, EventArgs e) // on form loading
        {
            UpdateAPIGroup1DataFields(); // setting them now avoids waiting a whole minute for the first refresh
            UpdateAPIGroup2DataFields(); // set the initial data for the daily updates to avoid waiting a whole day for the first data
            StartTheClocksTicking(); // start all the timers
            tboxSubmittedAddress.Text = "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa";
            CheckBlockchainExplorerApiStatus();
        }

        //=============================================================================================================
        // -------------------------CLOCK TICKS------------------------------------------------------------------------
        private void StartTheClocksTicking()
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
        private void Timer1Sec_Tick(object sender, EventArgs e) // update the calendar time and date
        {
            UpdateOnScreenClock();
            UpdateOnScreenCountdownAndFlashLights();
            intDisplaySecondsElapsedSinceUpdate ++; // increment displayed time elapsed since last update
            if (intDisplaySecondsElapsedSinceUpdate == 1)
            {
                lblElapsedSinceUpdate.Text = "Last updated " + intDisplaySecondsElapsedSinceUpdate.ToString() + " second ago";
            }
            else
            {
                lblElapsedSinceUpdate.Text = "Last updated " + intDisplaySecondsElapsedSinceUpdate.ToString() + " seconds ago";
            }
            if (ObtainedHalveningSecondsRemainingYet) // only want to do this if we've already retrieved seconds remaining until halvening
            {
                string secondsString = lblHalveningSecondsRemaining.Text;
                int SecondsToHalving = int.Parse(secondsString);
                if (SecondsToHalving > 0)
                {
                    SecondsToHalving = SecondsToHalving - 1; // one second closer to the halvening!
                    lblHalveningSecondsRemaining.Text = SecondsToHalving.ToString();
                }
            }
        }

        private void TimerAPIGroup1_Tick(object sender, EventArgs e) // call the function to update the btc fields
        {
            ClearAlertAndErrorMessage(); // wipe anything that may be showing in the error area (it should be empty anyway)
            UpdateAPIGroup1DataFields(); // fetch data and populate fields
        }

        private void TimerAPIGroup2_Tick(object sender, EventArgs e)
        {
            UpdateAPIGroup2DataFields(); // fetch data and populate fields
        }

        //==============================================================================================================================================================================================
        //======================BITCOIN AND LIGHTNING DASHBOARD SPECIFIC STUFF==========================================================================================================================
        //=============================================================================================================
        //-------------------------UPDATE FORM FIELDS------------------------------------------------------------------
        public async void UpdateAPIGroup1DataFields()
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
                        var result = BitcoinExplorerOrgEndpointsRefresh();
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
                        SetLightsMessagesAndResetTimers();
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
                            var result2 = BlockchainInfoEndpointsRefresh();
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
                        SetLightsMessagesAndResetTimers();
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
                            var result3 = BitcoinExplorerOrgJSONRefresh();
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
                                lblNextBlockMinFee.Text = result3.nextBlockMinFee + " / " + result3.nextBlockMaxFee;
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
                            lblNextBlockTotalFees.Invoke((MethodInvoker)delegate
                            {
                                lblNextBlockTotalFees.Text = "disabled";
                            });

                        }
                        SetLightsMessagesAndResetTimers();
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
                            var result4 = BlockchainInfoJSONRefresh();
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
                        SetLightsMessagesAndResetTimers();
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
                            var result5 = CoingeckoComJSONRefresh();
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
                        SetLightsMessagesAndResetTimers();
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

                Task task6 = Task.Run(() => //call mempool.space lightning JSON
                {
                    try
                    {
                        if (RunMempoolSpaceLightningAPI)
                        {
                            var result6 = MempoolSpaceLightningJSONRefresh();
                            // move returned data to the labels on the form
                            lblChannelCount.Invoke((MethodInvoker)delegate
                            {
                                lblChannelCount.Text = result6.channelCount;
                            });
                            lblNodeCount.Invoke((MethodInvoker)delegate
                            {
                                lblNodeCount.Text = result6.nodeCount;
                            });
                            lblTotalCapacity.Invoke((MethodInvoker)delegate
                            {
                                lblTotalCapacity.Text = result6.totalCapacity;
                            });
                            lblTorNodes.Invoke((MethodInvoker)delegate
                            {
                                lblTorNodes.Text = result6.torNodes;
                            });
                            lblClearnetNodes.Invoke((MethodInvoker)delegate
                            {
                                lblClearnetNodes.Text = result6.clearnetNodes;
                            });
                            lblAverageCapacity.Invoke((MethodInvoker)delegate
                            {
                                lblAverageCapacity.Text = result6.avgCapacity;
                            });
                            lblAverageFeeRate.Invoke((MethodInvoker)delegate
                            {
                                lblAverageFeeRate.Text = result6.avgFeeRate;
                            });
                            lblUnannouncedNodes.Invoke((MethodInvoker)delegate
                            {
                                lblUnannouncedNodes.Text = result6.unannouncedNodes;
                            });
                            lblAverageBaseFeeMtokens.Invoke((MethodInvoker)delegate
                            {
                                lblAverageBaseFeeMtokens.Text = result6.avgBaseeFeeMtokens;
                            });
                            lblMedCapacity.Invoke((MethodInvoker)delegate
                            {
                                lblMedCapacity.Text = result6.medCapacity;
                            });
                            lblMedFeeRate.Invoke((MethodInvoker)delegate
                            {
                                lblMedFeeRate.Text = result6.medFeeRate;
                            });
                            lblMedBaseFeeTokens.Invoke((MethodInvoker)delegate
                            {
                                lblMedBaseFeeTokens.Text = result6.medBaseeFeeMtokens;
                            });
                            lblClearnetTorNodes.Invoke((MethodInvoker)delegate
                            {
                                lblClearnetTorNodes.Text = result6.clearnetTorNodes;
                            });
                        }
                        else
                        {
                            lblChannelCount.Invoke((MethodInvoker)delegate
                            {
                                lblChannelCount.Text = "Disabled";
                            });
                            lblNodeCount.Invoke((MethodInvoker)delegate
                            {
                                lblNodeCount.Text = "Disabled";
                            });
                            lblTotalCapacity.Invoke((MethodInvoker)delegate
                            {
                                lblTotalCapacity.Text = "Disabled";
                            });
                            lblTorNodes.Invoke((MethodInvoker)delegate
                            {
                                lblTorNodes.Text = "Disabled";
                            });
                            lblClearnetNodes.Invoke((MethodInvoker)delegate
                            {
                                lblClearnetNodes.Text = "Disabled";
                            });
                            lblAverageCapacity.Invoke((MethodInvoker)delegate
                            {
                                lblAverageCapacity.Text = "Disabled";
                            });
                            lblAverageFeeRate.Invoke((MethodInvoker)delegate
                            {
                                lblAverageFeeRate.Text = "Disabled";
                            });
                            lblUnannouncedNodes.Invoke((MethodInvoker)delegate
                            {
                                lblUnannouncedNodes.Text = "Disabled";
                            });
                            lblAverageBaseFeeMtokens.Invoke((MethodInvoker)delegate
                            {
                                lblAverageBaseFeeMtokens.Text = "Disabled";
                            });
                            lblMedCapacity.Invoke((MethodInvoker)delegate
                            {
                                lblMedCapacity.Text = "Disabled";
                            });
                            lblMedFeeRate.Invoke((MethodInvoker)delegate
                            {
                                lblMedFeeRate.Text = "Disabled";
                            });
                            lblMedBaseFeeTokens.Invoke((MethodInvoker)delegate
                            {
                                lblMedBaseFeeTokens.Text = "Disabled";
                            });
                            lblClearnetTorNodes.Invoke((MethodInvoker)delegate
                            {
                                lblClearnetTorNodes.Text = "Disabled";
                            });
                        }
                        if (RunMempoolSpaceLightningAPI)
                        {
                            var result6 = MempoolSpaceCapacityBreakdownJSONRefresh();
                            lblClearnetCapacity.Invoke((MethodInvoker)delegate
                            {
                                lblClearnetCapacity.Text = result6.clearnetCapacity;
                            });
                            lblTorCapacity.Invoke((MethodInvoker)delegate
                            {
                                lblTorCapacity.Text = result6.torCapacity;
                            });
                            lblUnknownCapacity.Invoke((MethodInvoker)delegate
                            {
                                lblUnknownCapacity.Text = result6.unknownCapacity;
                            });
                        }
                        else
                        {
                            lblClearnetCapacity.Invoke((MethodInvoker)delegate
                            {
                                lblClearnetCapacity.Text = "Disabled";
                            });
                            lblTorCapacity.Invoke((MethodInvoker)delegate
                            {
                                lblTorCapacity.Text = "Disabled";
                            });
                            lblUnknownCapacity.Invoke((MethodInvoker)delegate
                            {
                                lblUnknownCapacity.Text = "Disabled";
                            });
                        }
                        if (RunMempoolSpaceLightningAPI) 
                        {
                            var result6 = MempoolSpaceLiquidityRankingJSONRefresh();
                            for (int i = 0; i < 10; i++)
                            {
                                System.Windows.Forms.Label aliasLabel = (System.Windows.Forms.Label)this.Controls.Find("aliasLabel" + (i + 1), true)[0];
                                aliasLabel.Invoke((MethodInvoker)delegate
                                {
                                    aliasLabel.Text = result6.aliases[i];
                                });
                                System.Windows.Forms.Label capacityLabel = (System.Windows.Forms.Label)this.Controls.Find("capacityLabel" + (i + 1), true)[0];
                                capacityLabel.Invoke((MethodInvoker)delegate
                                {
                                    capacityLabel.Text = result6.capacities[i];
                                });
                            }
                            var result7 = MempoolSpaceConnectivityRankingJSONRefresh();
                            for (int i = 0; i < 10; i++)
                            {
                                System.Windows.Forms.Label aliasLabel = (System.Windows.Forms.Label)this.Controls.Find("aliasConnLabel" + (i + 1), true)[0];
                                aliasLabel.Invoke((MethodInvoker)delegate
                                {
                                    aliasLabel.Text = result7.aliases[i];
                                });
                                System.Windows.Forms.Label channelLabel = (System.Windows.Forms.Label)this.Controls.Find("channelLabel" + (i + 1), true)[0];
                                channelLabel.Invoke((MethodInvoker)delegate
                                {
                                    channelLabel.Text = result7.channels[i];
                                });
                            }
                        }
                        SetLightsMessagesAndResetTimers();
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

                await Task.WhenAll(task1, task2, task3, task4, task5, task6);

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

        public async void UpdateAPIGroup2DataFields()
        {
            using (WebClient client = new WebClient())
            {
                bool errorOccurred = false;
                Task task7 = Task.Run(() =>  // call blockchair.com endpoints and populate the fields on the form
                {
                    try
                    {
                        if (RunBlockchairComJSONAPI)
                        {
                            var result7 = BlockchairComJSONRefresh();
                            int hodling_addresses = int.Parse(result7.hodling_addresses);
                            if (hodling_addresses > 0) // this api sometimes doesn't populate this field with anything but 0
                            {
                                lblHodlingAddresses.Invoke((MethodInvoker)delegate
                                {
                                    lblHodlingAddresses.Text = result7.hodling_addresses;
                                });
                            }
                            else
                            {
                                lblHodlingAddresses.Invoke((MethodInvoker)delegate
                                {
                                    lblHodlingAddresses.Text = "no data";
                                });
                            }
                            lblBlocksIn24Hours.Invoke((MethodInvoker)delegate
                            {
                                lblBlocksIn24Hours.Text = result7.blocks_24h;
                            });
                            lblNodes.Invoke((MethodInvoker)delegate
                            {
                                lblNodes.Text = result7.nodes;
                            });
                            dynamic blockchainSize = result7.blockchain_size;
                            double blockchainSizeGB = Math.Round(Convert.ToDouble(blockchainSize) / 1073741824.0, 2);
                            lblBlockchainSize.Invoke((MethodInvoker)delegate
                            {
                                lblBlockchainSize.Text = blockchainSizeGB.ToString();
                            });
                        }
                        else
                        {
                            lblHodlingAddresses.Invoke((MethodInvoker)delegate
                            {
                                lblHodlingAddresses.Text = "disabled";
                            });
                            lblBlocksIn24Hours.Invoke((MethodInvoker)delegate
                            {
                                lblBlocksIn24Hours.Text = "disabled";
                            });
                            lblNodes.Invoke((MethodInvoker)delegate
                            {
                                lblNodes.Text = "disabled";
                            });
                            lblBlockchainSize.Invoke((MethodInvoker)delegate
                            {
                                lblBlockchainSize.Text = "disabled";
                            });
                        }
                        SetLightsMessagesAndResetTimers();
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
                
                Task task8 = Task.Run(() =>  // call blockchair.com endpoints and populate the fields on the form
                {
                    try
                    {
                        if (RunBlockchairComJSONAPI)
                        {
                            var result8 = BlockchairComHalvingJSONRefresh();
                            lblHalveningBlock.Invoke((MethodInvoker)delegate
                            {
                                lblHalveningBlock.Text = result8.halveningBlock + "/" + result8.blocksLeft;
                            });
                            string halvening_time = result8.halveningTime;
                            DateTime halveningDateTime = DateTime.ParseExact(halvening_time, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            string halveningDate = halveningDateTime.Date.ToString("yyyy-MM-dd");

                            lblEstimatedHalvingDate.Invoke((MethodInvoker)delegate
                            {
                                lblEstimatedHalvingDate.Text = halveningDate + "/";
                            });
                            lblHalveningSecondsRemaining.Invoke((MethodInvoker)delegate
                            {
                                lblHalveningSecondsRemaining.Location = new Point(lblEstimatedHalvingDate.Location.X + lblEstimatedHalvingDate.Width - 8, lblEstimatedHalvingDate.Location.Y);
                                lblHalveningSecondsRemaining.Text = result8.seconds_left;
                                ObtainedHalveningSecondsRemainingYet = true; // signifies that we can now start deducting from this
                            });
                        }
                        else
                        {
                            lblHalveningBlock.Invoke((MethodInvoker)delegate
                            {
                                lblHalveningBlock.Text = "disabled";
                            });
                            lblEstimatedHalvingDate.Invoke((MethodInvoker)delegate
                            {
                                lblEstimatedHalvingDate.Text = "disabled";
                            });
                            lblHalveningSecondsRemaining.Invoke((MethodInvoker)delegate
                            {
                                lblHalveningSecondsRemaining.Text = "disabled";
                            });
                        }
                        SetLightsMessagesAndResetTimers();
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
                
                await Task.WhenAll(task7, task8);

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

        //=============================================================================================================
        //------------------------------------API CALLS----------------------------------------------------------------
        //------BitcoinExplorer and BlockchainInfo endpoints 
        private (string priceUSD, string moscowTime, string marketCapUSD, string difficultyAdjEst, string txInMempool) BitcoinExplorerOrgEndpointsRefresh()
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

        private (string avgNoTransactions, string blockNumber, string blockReward, string estHashrate, string avgTimeBetweenBlocks, string btcInCirc, string hashesToSolve, string twentyFourHourTransCount, string twentyFourHourBTCSent) BlockchainInfoEndpointsRefresh()
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
                string btcInCirc = ConvertSatsToBitcoin(totalBTC).ToString();
                string hashesToSolve = client.DownloadString("https://blockchain.info/q/hashestowin"); // avg number of hashes to win a block
                string twentyFourHourTransCount = client.DownloadString("https://blockchain.info/q/24hrtransactioncount"); // number of transactions in last 24 hours
                string twentyFourHourBTCSent = client.DownloadString("https://blockchain.info/q/24hrbtcsent"); // number of sats sent in 24 hours
                twentyFourHourBTCSent = ConvertSatsToBitcoin(twentyFourHourBTCSent).ToString();
                return (avgNoTransactionsText, blockNumber, blockReward, estHashrate, avgTimeBetweenBlocks, btcInCirc, hashesToSolve, twentyFourHourTransCount, twentyFourHourBTCSent);
            }
        }

        //-----Mempool Lighting JSON
        private (List<string> aliases, List<string> capacities) MempoolSpaceLiquidityRankingJSONRefresh()
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString("https://mempool.space/api/v1/lightning/nodes/rankings/liquidity");
                var data = JArray.Parse(response);

                List<string> aliases = new List<string>();
                List<string> capacities = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    aliases.Add((string)data[i]["alias"]);
                    string capacity = (string)data[i]["capacity"];
                    capacity = ConvertSatsToBitcoin(capacity).ToString();
                    double dblCapacity = Convert.ToDouble(capacity);
                    dblCapacity = Math.Round(dblCapacity, 2); // round to 2 decimal places
                    capacity = Convert.ToString(dblCapacity);
                    capacities.Add(capacity);
                }

                return (aliases, capacities);
            }
        }

        private (List<string> aliases, List<string> channels) MempoolSpaceConnectivityRankingJSONRefresh()
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString("https://mempool.space/api/v1/lightning/nodes/rankings/connectivity");
                var data = JArray.Parse(response);

                List<string> aliases = new List<string>();
                List<string> channels = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    aliases.Add((string)data[i]["alias"]);
                    channels.Add((string)data[i]["channels"]);
                }

                return (aliases, channels);
            }
        }

        private (string clearnetCapacity, string torCapacity, string unknownCapacity) MempoolSpaceCapacityBreakdownJSONRefresh()
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString("https://mempool.space/api/v1/lightning/nodes/isp-ranking");
                var data = JObject.Parse(response);
                string clearnetCapacityString = Convert.ToString(data["clearnetCapacity"]);
                string clearnetCapacity = ConvertSatsToBitcoin(clearnetCapacityString).ToString();
                double dblClearnetCapacity = Convert.ToDouble(clearnetCapacity);
                dblClearnetCapacity = Math.Round(dblClearnetCapacity, 2); // round to 2 decimal places
                clearnetCapacity = Convert.ToString(dblClearnetCapacity);
                string torCapacityString = Convert.ToString(data["torCapacity"]);
                string torCapacity = ConvertSatsToBitcoin(torCapacityString).ToString();
                double dblTorCapacity = Convert.ToDouble(torCapacity);
                dblTorCapacity = Math.Round(dblTorCapacity, 2);
                torCapacity= Convert.ToString(dblTorCapacity);
                string unknownCapacityString = Convert.ToString(data["unknownCapacity"]);
                string unknownCapacity = ConvertSatsToBitcoin(unknownCapacityString).ToString();
                double dblUnknownCapacity = Convert.ToDouble(unknownCapacity);
                dblUnknownCapacity = Math.Round(dblUnknownCapacity, 2);
                unknownCapacity= Convert.ToString(dblUnknownCapacity);
                return (clearnetCapacity, torCapacity, unknownCapacity);
            }
        }

        private (string channelCount, string nodeCount, string totalCapacity, string torNodes, string clearnetNodes, string unannouncedNodes, string avgCapacity, string avgFeeRate, string avgBaseeFeeMtokens, string medCapacity, string medFeeRate, string medBaseeFeeMtokens, string clearnetTorNodes) MempoolSpaceLightningJSONRefresh()
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString("https://mempool.space/api/v1/lightning/statistics/latest");
                var data = JObject.Parse(response);
                var channelCount = (string)data["latest"]["channel_count"];
                var nodeCount = (string)data["latest"]["node_count"];
                string totalCapacityString = Convert.ToString(data["latest"]["total_capacity"]);
                string totalCapacity = ConvertSatsToBitcoin(totalCapacityString).ToString();
                double dblTotalCapacity = Convert.ToDouble(totalCapacity);
                dblTotalCapacity = Math.Round(dblTotalCapacity, 2);
                totalCapacity = Convert.ToString(dblTotalCapacity);
                var torNodes = (string)data["latest"]["tor_nodes"];
                var clearnetNodes = (string)data["latest"]["clearnet_nodes"];
                var unannouncedNodes = (string)data["latest"]["unannounced_nodes"];
                var avgCapacity = (string)data["latest"]["avg_capacity"];
                var avgFeeRate = (string)data["latest"]["avg_fee_rate"];
                var avgBaseeFeeMtokens = (string)data["latest"]["avg_base_fee_mtokens"];
                var medCapacity = (string)data["latest"]["med_capacity"];
                var medFeeRate = (string)data["latest"]["med_fee_rate"];
                var medBaseeFeeMtokens = (string)data["latest"]["med_basee_fee_mtokens"];
                if (medBaseeFeeMtokens == null) 
                {
                    medBaseeFeeMtokens = "0";
                }
                var clearnetTorNodes = (string)data["latest"]["clearnet_tor_nodes"];
                return (channelCount, nodeCount, totalCapacity, torNodes, clearnetNodes, unannouncedNodes, avgCapacity, avgFeeRate, avgBaseeFeeMtokens, medCapacity, medFeeRate, medBaseeFeeMtokens, clearnetTorNodes);
            }
        }

        //-----BitcoinExplorer JSON
        private (string nextBlockFee, string thirtyMinFee, string sixtyMinFee, string oneDayFee, string txInNextBlock, string nextBlockMinFee, string nextBlockMaxFee, string nextBlockTotalFees) BitcoinExplorerOrgJSONRefresh()
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

        //-----BlockchainInfo JSON
        private (string n_tx, string size, string nextretarget) BlockchainInfoJSONRefresh()
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

        //-----CoinGecko JSON
        private (string ath, string athDate, string athDifference, string twentyFourHourHigh, string twentyFourHourLow) CoingeckoComJSONRefresh()
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

        //-----Blockchair JSON
        private (string halveningBlock, string halveningReward, string halveningTime, string blocksLeft, string seconds_left) BlockchairComHalvingJSONRefresh()
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString("https://api.blockchair.com/tools/halvening");
                var data = JObject.Parse(response);
                var halveningBlock = (string)data["data"]["bitcoin"]["halvening_block"];
                var halveningReward = (string)data["data"]["bitcoin"]["halvening_reward"];
                var halveningTime = (string)data["data"]["bitcoin"]["halvening_time"];
                var blocksLeft = (string)data["data"]["bitcoin"]["blocks_left"];
                var seconds_left = (string)data["data"]["bitcoin"]["seconds_left"];
                return (halveningBlock, halveningReward, halveningTime, blocksLeft, seconds_left);
            }
        }

        private (string hodling_addresses, string blocks_24h, string nodes, string blockchain_size) BlockchairComJSONRefresh()
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString("https://api.blockchair.com/bitcoin/stats");
                var data = JObject.Parse(response);
                var hodling_addresses = (string)data["data"]["hodling_addresses"];
                var blocks_24h = (string)data["data"]["blocks_24h"];
                var nodes = (string)data["data"]["nodes"];
                var blockchain_size = (string)data["data"]["blockchain_size"];
                return (hodling_addresses, blocks_24h, nodes, blockchain_size);
            }
        }

        //=============================================================================================================
        //---------------------- CONNECTING LINES BETWEEN FIELDS-------------------------------------------------------
        private void PanelLightningDashboard_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(106, 72, 9), 1))
            {
                // Capacity connecting lines
                e.Graphics.DrawLine(pen, lblTotalCapacity.Right, lblTotalCapacity.Top + (lblTotalCapacity.Height / 2), lblClearnetCapacity.Left, lblClearnetCapacity.Top + (lblClearnetCapacity.Height / 2));
                e.Graphics.DrawLine(pen, (lblTotalCapacity.Right + lblClearnetCapacity.Left) / 2, lblTotalCapacity.Top + (lblTotalCapacity.Height / 2), (lblTotalCapacity.Right + lblClearnetCapacity.Left) / 2, lblUnknownCapacity.Top + (lblUnknownCapacity.Height / 2));
                e.Graphics.DrawLine(pen, (lblTotalCapacity.Right + lblClearnetCapacity.Left) / 2, lblTorCapacity.Top + (lblTorCapacity.Height / 2), lblTorCapacity.Left, lblTorCapacity.Top + (lblTorCapacity.Height / 2));
                e.Graphics.DrawLine(pen, (lblTotalCapacity.Right + lblClearnetCapacity.Left) / 2, lblUnknownCapacity.Top + (lblUnknownCapacity.Height / 2), lblUnknownCapacity.Left, lblUnknownCapacity.Top + (lblUnknownCapacity.Height / 2));
                // Node connecting lines
                e.Graphics.DrawLine(pen, lblNodeCount.Right, lblNodeCount.Top + (lblNodeCount.Height / 2), lblTorNodes.Left, lblTorNodes.Top + (lblTorNodes.Height / 2));
                e.Graphics.DrawLine(pen, (lblTotalCapacity.Right + lblClearnetCapacity.Left) / 2, lblTorNodes.Top + (lblTorNodes.Height / 2), (lblTotalCapacity.Right + lblClearnetCapacity.Left) / 2, lblUnannouncedNodes.Top + (lblUnannouncedNodes.Height / 2));
                e.Graphics.DrawLine(pen, (lblTotalCapacity.Right + lblClearnetCapacity.Left) / 2, lblClearnetNodes.Top + (lblClearnetNodes.Height / 2), lblClearnetNodes.Left, lblClearnetNodes.Top + (lblClearnetNodes.Height / 2));
                e.Graphics.DrawLine(pen, (lblTotalCapacity.Right + lblClearnetCapacity.Left) / 2, lblClearnetTorNodes.Top + (lblClearnetTorNodes.Height / 2), lblClearnetTorNodes.Left, lblClearnetTorNodes.Top + (lblClearnetTorNodes.Height / 2));
                e.Graphics.DrawLine(pen, (lblTotalCapacity.Right + lblClearnetCapacity.Left) / 2, lblUnannouncedNodes.Top + (lblUnannouncedNodes.Height / 2), lblUnannouncedNodes.Left, lblUnannouncedNodes.Top + (lblUnannouncedNodes.Height / 2));
                // Channel connecting lines
                e.Graphics.DrawLine(pen, lblChannelCount.Right, lblChannelCount.Top + (lblChannelCount.Height / 2), lblAverageCapacity.Left, lblAverageCapacity.Top + (lblAverageCapacity.Height / 2));
                e.Graphics.DrawLine(pen, (lblChannelCount.Right + lblAverageCapacity.Left) / 2, lblChannelCount.Top + (lblChannelCount.Height / 2), (lblChannelCount.Right + lblAverageCapacity.Left) / 2, lblMedBaseFeeTokens.Top + (lblMedBaseFeeTokens.Height / 2));
                e.Graphics.DrawLine(pen, (lblChannelCount.Right + lblAverageCapacity.Left) / 2, lblAverageFeeRate.Top + (lblAverageFeeRate.Height / 2), lblAverageFeeRate.Left, lblAverageFeeRate.Top + (lblAverageFeeRate.Height / 2));
                e.Graphics.DrawLine(pen, (lblChannelCount.Right + lblAverageCapacity.Left) / 2, lblAverageBaseFeeMtokens.Top + (lblAverageBaseFeeMtokens.Height / 2), lblAverageBaseFeeMtokens.Left, lblAverageBaseFeeMtokens.Top + (lblAverageBaseFeeMtokens.Height / 2));
                e.Graphics.DrawLine(pen, (lblChannelCount.Right + lblAverageCapacity.Left) / 2, lblMedCapacity.Top + (lblMedCapacity.Height / 2), lblMedCapacity.Left, lblMedCapacity.Top + (lblMedCapacity.Height / 2));
                e.Graphics.DrawLine(pen, (lblChannelCount.Right + lblAverageCapacity.Left) / 2, lblMedFeeRate.Top + (lblMedFeeRate.Height / 2), lblMedFeeRate.Left, lblMedFeeRate.Top + (lblMedFeeRate.Height / 2));
                e.Graphics.DrawLine(pen, (lblChannelCount.Right + lblAverageCapacity.Left) / 2, lblMedBaseFeeTokens.Top + (lblMedBaseFeeTokens.Height / 2), lblMedBaseFeeTokens.Left, lblMedBaseFeeTokens.Top + (lblMedBaseFeeTokens.Height / 2));
            }
        }

        //==============================================================================================================================================================================================
        //======================== ADDRESS TAB =========================================================================================================================================================
        //=============================================================================================================
        //---------------------- DETERMINE BITCOIN ADDRESS TYPE--------------------------------------------------------
        private string DetermineAddressType(string address)
        {
            try
            {
                BitcoinAddress bitcoinAddress = BitcoinAddress.Create(address, Network.Main);
                if (bitcoinAddress is BitcoinPubKeyAddress)
                {
                    return "P2PKH (legacy)"; // Legacy P2PKH
                }
                else if (bitcoinAddress is BitcoinScriptAddress)
                {
                    return "P2SH"; // (pay-to-script-hash) Multisig P2SH
                }
                else if (bitcoinAddress is BitcoinWitPubKeyAddress)
                {
                    return "P2WPKH (segwit)"; // P2WPKH
                }
                else if (bitcoinAddress is BitcoinWitScriptAddress)
                {
                    return "P2WSH"; // (pay-to- witness-script-hash) P2WSH 
                }
                else if (address.StartsWith("bc1p") && (address.Length > 41 || address.Length < 73))
                {
                    for (int i = 4; i < address.Length - 4; i++)
                    {
                        char c = address[i];
                        if (((c >= '0' && c <= '9') || (c >= 'q' && c <= 'z')))
                        {
                            return "P2TT (taproot)";
                        }
                    }
                }
                return "unknown";
            }
            catch (FormatException)
            {
                return "Invalid address format";
            }
        }

        //=============================================================================================================
        //--------------- VALIDATE BITCOIN ADDRESS AND GENERATE QR-----------------------------------------------------
        private void TboxSubmittedAddress_TextChanged(object sender, EventArgs e) // validate the address update the address type label
        {
            listViewTransactions.Items.Clear();
            TotalTransactionRowsAdded = 0;
            btnNextTransactions.Enabled = true;

            if (tboxSubmittedAddress.Text == "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa")
            {
                lblGenesisAddress.Text = "(This is Satoshi's Genesis address. Enter any bitcoin address)";
            }
            else
            {
                lblGenesisAddress.Text = null;
            }
            string addressString = tboxSubmittedAddress.Text; 
            string addressType = DetermineAddressType(addressString);
            if (addressType == "P2PKH (legacy)" || addressType == "P2SH" || addressType == "P2WPKH (segwit)" || addressType == "P2WSH" || addressType == "P2TT (taproot)" || addressType == "unknown") 
            {
                lblAddressType.Text = addressType + " address type";
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(addressString, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                var qrCodeImage = qrCode.GetGraphic(20, Color.FromArgb(255, 192, 192, 192), Color.Black, false);
                qrCodeImage.MakeTransparent(Color.Black);
                QRCodePicturebox.Image = qrCodeImage;
                lblAddressType.Text = addressType + " fetching balance";
                GetAddressBalance(addressString);
                string lastSeenTxId = "";
                GetConfirmedTransactionsForAddress(addressString,lastSeenTxId);
                lblAddressType.Text = addressType + " address type";
            }
            else
            {
                lblAddressType.Text = "Invalid address format";
                QRCodePicturebox.Image = null;
                lblConfirmedReceived.Text = string.Empty;
                lblConfirmedReceivedOutputs.Text = string.Empty;
                lblConfirmedSpent.Text = string.Empty;
                lblConfirmedSpentOutputs.Text = string.Empty;
                lblConfirmedTransactionCount.Text = string.Empty;
                lblConfirmedUnspent.Text = string.Empty;
                lblConfirmedUnspentOutputs.Text = string.Empty;
            }
        }

        //=============================================================================================================
        //------------------------------------------ GET ADDRESS BALANCE-----------------------------------------------
        private async void GetAddressBalance(string addressString) 
        {
            var request = "address/" + addressString;
            var RequestURL = NodeURL + request;
            //var RequestURL = "http://umbrel.local:3006/api/" + request;
            var client = new HttpClient();
            var response = await client.GetAsync($"{RequestURL}");
            if (!response.IsSuccessStatusCode)
            {
                lblNodeStatusLight.ForeColor = Color.Red;
                lblActiveNode.Text = "Disconnected/error";
                return;
            }
            var jsonData = await response.Content.ReadAsStringAsync();
            var addressData = JObject.Parse(jsonData);
            lblConfirmedTransactionCount.Text = Convert.ToString(addressData["chain_stats"]["tx_count"]);
            lblConfirmedReceived.Text = ConvertSatsToBitcoin(Convert.ToString(addressData["chain_stats"]["funded_txo_sum"])).ToString();
            lblConfirmedReceivedOutputs.Location = new Point(lblConfirmedReceived.Location.X + lblConfirmedReceived.Width, lblConfirmedReceivedOutputs.Location.Y);
            lblConfirmedReceivedOutputs.Text = "(" + addressData["chain_stats"]["funded_txo_count"] + " outputs)";
            lblConfirmedSpent.Text = ConvertSatsToBitcoin(Convert.ToString(addressData["chain_stats"]["spent_txo_sum"])).ToString();
            lblConfirmedSpentOutputs.Location = new Point(lblConfirmedSpent.Location.X + lblConfirmedSpent.Width -5, lblConfirmedSpentOutputs.Location.Y);
            lblConfirmedSpentOutputs.Text = " (" + addressData["chain_stats"]["spent_txo_count"] + " outputs)";
            var fundedTx = Convert.ToDouble(addressData["chain_stats"]["funded_txo_count"]);
            var spentTx = Convert.ToDouble(addressData["chain_stats"]["spent_txo_count"]);
            var confirmedReceived = Convert.ToDouble(addressData["chain_stats"]["funded_txo_sum"]);
            var confirmedSpent = Convert.ToDouble(addressData["chain_stats"]["spent_txo_sum"]);
            var confirmedUnspent = confirmedReceived - confirmedSpent;
            var unSpentTxOutputs = fundedTx - spentTx;
            lblConfirmedUnspent.Text = ConvertSatsToBitcoin(Convert.ToString(confirmedUnspent)).ToString();
            lblConfirmedUnspentOutputs.Location = new Point(lblConfirmedUnspent.Location.X + lblConfirmedUnspent.Width, lblConfirmedUnspentOutputs.Location.Y);
            lblConfirmedUnspentOutputs.Text = "(" + Convert.ToString(unSpentTxOutputs) + " outputs)";
        }

        //=============================================================================================================
        //---------------------- GET CONFIRMED TRANSACTIONS FOR ADDRESS -----------------------------------------------
        private async void GetConfirmedTransactionsForAddress(string addressString, string lastSeenTxId)
        {
            var transactionsJson = await _transactionService.GetTransactionsAsync(addressString, lastSeenTxId);
            var transactions = JsonConvert.DeserializeObject<List<Transaction>>(transactionsJson);
            // *************************************************************************************************************************************THIS LINE GIVES OCCASIONAL ERRORS
            List<string> txIds = transactions.Select(t => t.txid).ToList(); 
            // **********************************************************************************************************************************************************************

            // Update lastSeenTxId
            if (transactions.Count > 0)
            {
                lastSeenTxId = transactions.Last().txid;
            }
            //LIST VIEW
            listViewTransactions.Items.Clear();
            listViewTransactions.View = View.Details;
            listViewTransactions.GridLines = false;
            
            // Check if the column header already exists
            if (listViewTransactions.Columns.Count == 0)
            {
                // If not, add the column header
                listViewTransactions.Columns.Add("Transaction ID", 300);
            }

            // Add the block height column header
            if (listViewTransactions.Columns.Count == 1)
            {
                listViewTransactions.Columns.Add("Block Height", 200);
            }

            // Add the items to the ListView
            int counter = 0; // used to count rows in last as they're added

            foreach (Transaction transaction in transactions)
            {
                ListViewItem item = new ListViewItem(transaction.txid);
                item.SubItems.Add(transaction.status.block_height.ToString());
                listViewTransactions.Items.Add(item);
                counter++;
                TotalTransactionRowsAdded++;
                if (TotalTransactionRowsAdded <= 13)
                {
                    btnFirstTransaction.Enabled = false;
                }
                else
                {
                    btnFirstTransaction.Enabled = true;
                }
                lblTXPlaceInfo.Text = "Showing " + (TotalTransactionRowsAdded - counter + 1) + " - " + (TotalTransactionRowsAdded) + " of " + lblConfirmedTransactionCount.Text + " transactions";
                if (Convert.ToString(TotalTransactionRowsAdded) == lblConfirmedTransactionCount.Text)
                {
                    btnNextTransactions.Enabled = false;
                }
                if (counter == 13) // stop adding rows at this point
                {
                    break;
                }
            }
        }

        private void btnGetNextTransactions(object sender, EventArgs e)
        {
            // Get the address from the address text box
            var address = tboxSubmittedAddress.Text;
            // Get the last seen transaction ID from the list view
            var lastSeenTxId = listViewTransactions.Items[listViewTransactions.Items.Count - 1].Text;
            // Call the GetConfirmedTransactionsForAddress method with the updated lastSeenTxId
            GetConfirmedTransactionsForAddress(address, lastSeenTxId);
        }

        private void btnFirstTransaction_Click(object sender, EventArgs e)
        {
            // Get the address from the address text box
            var address = tboxSubmittedAddress.Text;

            // Get the last seen transaction ID from the list view
            var lastSeenTxId = "";
            TotalTransactionRowsAdded = 0;
            btnNextTransactions.Enabled = true;

            // Call the GetConfirmedTransactionsForAddress method with the updated lastSeenTxId
            GetConfirmedTransactionsForAddress(address, lastSeenTxId);
        }

        //=============================================================================================================
        //--------------- OVERRIDE COLOURS FOR LISTVIEW HEADINGS ------------------------------------------------------
        private void listViewTransactions_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            Color headerColor = Color.FromArgb(50, 50, 50);
            SolidBrush brush = new SolidBrush(headerColor);
            e.Graphics.FillRectangle(brush, e.Bounds);
            // Change text color and alignment
            SolidBrush textBrush = new SolidBrush(Color.Silver);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(e.Header.Text, e.Font, textBrush, e.Bounds, format);
        }

        //=============================================================================================================
        //--------------- OVERRIDE COLOURS FOR ROWS IN LISTVIEW -------------------------------------------------------
        private void listViewTransactions_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (e.Item.Selected)
            {
                Color backgroundColor = Color.FromArgb(106, 72, 9);
                SolidBrush brush = new SolidBrush(backgroundColor);
                e.Graphics.FillRectangle(brush, e.Bounds);
                e.Item.ForeColor = Color.White;
            }
            else
            {
                Color backgroundColor = Color.FromArgb(29, 29, 29);
                SolidBrush brush = new SolidBrush(backgroundColor);
                e.Graphics.FillRectangle(brush, e.Bounds);
                e.Item.ForeColor = Color.FromArgb(255, 153, 0);
            }
            e.DrawText();
        }

        //=============================================================================================================
        //--------- DRAW AN ELLIPSIS WHEN STRINGS DONT FIT IN A LISTVIEW COLUMN ---------------------------------------
        private void listViewTransactions_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            var text = e.SubItem.Text;
            var font = listViewTransactions.Font;
            var columnWidth = e.Header.Width;
            var textWidth = TextRenderer.MeasureText(text, font).Width;
            if (textWidth > columnWidth)
            {
                // Truncate the text
                var maxText = text.Substring(0, text.Length * columnWidth / textWidth - 3) + "...";
                var bounds = new Rectangle(e.SubItem.Bounds.Left, e.SubItem.Bounds.Top, columnWidth, e.SubItem.Bounds.Height);
                // Clear the background
                e.Graphics.FillRectangle(new SolidBrush(listViewTransactions.BackColor), bounds);
                TextRenderer.DrawText(e.Graphics, maxText, font, bounds, e.Item.ForeColor, TextFormatFlags.EndEllipsis | TextFormatFlags.Left);
            }
            else if (textWidth < columnWidth)
            {
                // Clear the background
                var bounds = new Rectangle(e.SubItem.Bounds.Left, e.SubItem.Bounds.Top, columnWidth, e.SubItem.Bounds.Height);
                e.Graphics.FillRectangle(new SolidBrush(listViewTransactions.BackColor), bounds);
                TextRenderer.DrawText(e.Graphics, text, font, bounds, e.Item.ForeColor, TextFormatFlags.Left);
            }
        }

        //=============================================================================================================
        //------------------ SHOW STATUS LIGHT FOR NODE ONLINE STATUS -------------------------------------------------
        private async void CheckBlockchainExplorerApiStatus()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    Ping pingSender = new Ping();
                    string pingAddress = null;
                    if (NodeURL == "https://blockstream.info/api/")
                    {
                        pingAddress = "blockstream.info";
                    }
                    if (NodeURL == "https://mempool.space/api/")
                    {
                        pingAddress = "mempool.space";
                    }
                    PingReply reply = await pingSender.SendPingAsync(pingAddress);
                    if (reply.Status == IPStatus.Success)
                    {
                        lblNodeStatusLight.ForeColor = Color.Green;
                        var displayNodeName = "";
                        if (NodeURL == "https://blockstream.info/api/")
                        {
                            displayNodeName = "Blockstream";
                        }
                        if (NodeURL == "https://mempool.space/api/")
                        {
                            displayNodeName = "Mempool.space";
                        }
                        lblActiveNode.Text = displayNodeName + " status";
                    }
                    else
                    {
                        // API is not online
                        lblNodeStatusLight.ForeColor = Color.Red;
                        var displayNodeName = "";
                        if (NodeURL == "https://blockstream.info/api/")
                        {
                            displayNodeName = "Blockstream";
                        }
                        if (NodeURL == "https://mempool.space/api/")
                        {
                            displayNodeName = "Mempool.space";
                        }
                        lblActiveNode.Text = displayNodeName + " status";
                    }
                }
                catch (HttpRequestException)
                {
                    // API is not online
                    lblNodeStatusLight.ForeColor = Color.Red;
                    var displayNodeName = "";
                    if (NodeURL == "https://blockstream.info/api/")
                    {
                        displayNodeName = "Blockstream";
                    }
                    if (NodeURL == "https://mempool.space/api/")
                    {
                        displayNodeName = "Mempool.space";
                    }
                    lblActiveNode.Text = displayNodeName + " status";
                }
            }
        }

        //=============================================================================================================
        //------------------ LIMIT MINIMUM WIDTH OF LISTVIEW COLUMNS --------------------------------------------------
        private void listViewTransactions_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (listViewTransactions.Columns[e.ColumnIndex].Width < 120)
            {
                e.Cancel = true;
                e.NewWidth = 120;
            }
        }

        //==============================================================================================================================================================================================
        //====================== GENERIC STUFF =========================================================================================================================================================
        //=============================================================================================================
        //-------------------- CONVERT SATS TO BITCOIN ----------------------------------------------------------------
        private decimal ConvertSatsToBitcoin(string numerics)
        {
            decimal number = decimal.Parse(numerics);
            decimal result = number / 100000000;
            return result;
        }

        //=============================================================================================================
        //--------------------COUNTDOWN, ERROR MESSAGES AND STATUS LIGHTS----------------------------------------------
        private void UpdateOnScreenCountdownAndFlashLights()
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
                ChangeStatusLightAndClearErrorMessage();
            }
        }

        private void ChangeStatusLightAndClearErrorMessage()
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

        private void ClearAlertAndErrorMessage()
        {
            lblAlert.Text = ""; // clear any error message
            lblErrorMessage.Text = ""; // clear any error message
        }

        private void SetLightsMessagesAndResetTimers()
        {
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
            intDisplaySecondsElapsedSinceUpdate = 0; // reset the seconds since last refresh
        }

        //=============================================================================================================        
        //-------------------------- GENERAL FORM NAVIGATION/BUTTON CONTROLS-------------------------------------------
        private void BtnSplash_Click(object sender, EventArgs e)
        {
            splash splash = new splash(); // invoke the about/splash screen
            splash.ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e) // exit
        {
            this.Close();
        }

        private void BtnMinimise_Click(object sender, EventArgs e) // minimise the form
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnMoveWindow_MouseDown(object sender, MouseEventArgs e) // move the form when the move control is used
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void BtnMoveWindow_MouseUp(object sender, MouseEventArgs e) // reset colour of the move form control
        {
            btnMoveWindow.BackColor = System.Drawing.ColorTranslator.FromHtml("#1D1D1D");
        }

        private void BtnBitcoinDashboard_Click(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.SuspendLayout();
            panelLightningDashboard.Visible = false;
            panelTesting.Visible = false;
            panelBitcoinDashboard.Visible = true;
            this.ResumeLayout();
        }

        private void BtnLightningDashboard_Click(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.SuspendLayout();
            panelBitcoinDashboard.Visible = false;
            panelTesting.Visible = false;
            panelLightningDashboard.Visible = true;
            this.ResumeLayout();
        }

        private void BtnAddress_Click(object sender, EventArgs e)
        {
            panelBitcoinDashboard.Visible = false;
            panelLightningDashboard.Visible = false;
            panelTesting.Visible = true;
        }

        private void BtnSettings_Click(object sender, EventArgs e)
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
            if (settingsScreen.Instance.MempoolSpaceLightningJSONEnabled)
            {
                RunMempoolSpaceLightningAPI = true;
            }
            else
            {
                RunMempoolSpaceLightningAPI = false;
            }
            if (settingsScreen.Instance.NodeURL != NodeURL)
            {
                NodeURL = settingsScreen.Instance.NodeURL;
                _transactionService = new TransactionService(NodeURL);
            }

            CheckBlockchainExplorerApiStatus();

            if (APIGroup1DisplayTimerIntervalSecsConstant != (settingsScreen.Instance.APIGroup1RefreshInMinsSelection * 60)) // if user has changed refresh frequency
            {
                APIGroup1DisplayTimerIntervalSecsConstant = settingsScreen.Instance.APIGroup1RefreshInMinsSelection * 60;
                intAPIGroup1TimerIntervalMillisecsConstant = ((settingsScreen.Instance.APIGroup1RefreshInMinsSelection * 60) * 1000);
                intDisplayCountdownToRefresh = APIGroup1DisplayTimerIntervalSecsConstant;
                timerAPIGroup1.Stop();
                timerAPIGroup1.Interval = intAPIGroup1TimerIntervalMillisecsConstant;
                timerAPIGroup1.Start();
            }

            if (intAPIGroup2TimerIntervalMillisecsConstant != (((settingsScreen.Instance.APIGroup2RefreshInHoursSelection * 60) * 60) * 1000))
            {
                intAPIGroup2TimerIntervalMillisecsConstant = (((settingsScreen.Instance.APIGroup2RefreshInHoursSelection * 60) * 60) * 1000);
                timerAPIGroup2.Stop();
                timerAPIGroup2.Interval = intAPIGroup2TimerIntervalMillisecsConstant;
                timerAPIGroup2.Start();
            }
        }

        //=============================================================================================================
        //--------------------------ON-SCREEN CLOCK--------------------------------------------------------------------
        private void UpdateOnScreenClock()
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm");
            lblSeconds.Text = DateTime.Now.ToString("ss");
            lblDate.Text = DateTime.Now.ToString("MMMM dd yyyy");
            lblDay.Text = DateTime.Now.ToString("dddd");
            lblSeconds.Location = new Point(lblTime.Location.X + lblTime.Width - 10, lblSeconds.Location.Y); // place the seconds according to the width of the minutes/seconds (lblTime)
        }

        //=============================================================================================================
        //---------------------- BORDER AROUND WINDOW------------------------------------------------------------------
        private void Form1_Paint(object sender, PaintEventArgs e) // place a 1px border around the form
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
        }
    }

    //==============================================================================================================================================================================================
    //==============================================================================================================================================================================================

    public class TransactionService
    {
        private readonly string _nodeUrl;

        public TransactionService(string nodeUrl)
        {
            _nodeUrl = nodeUrl;
        }

        //public async Task<string> GetTransactionsAsync(string address)
        public async Task<string> GetTransactionsAsync(string address, string lastSeenTxId = "")
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_nodeUrl);
                    var response = await client.GetAsync($"address/{address}/txs/chain/{lastSeenTxId}");
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    // handle exception
                }
            }
            return string.Empty;
        }
    }
    //=================================================================================================================
    //----------------------- CLASS TO STORE TXID FOR EACH TRANSACTION ------------------------------------------------

    public class Transaction
    {
        public string txid { get; set; }
        public Status status { get; set; }
    }

    public class Status
    {
        public int block_height { get; set; }
    }
}
//==================================================================================================================================================================================================
//======================================================================================== END =====================================================================================================
//==================================================================================================================================================================================================

