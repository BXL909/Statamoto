namespace Statamoto
{
    partial class Statamoto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Statamoto));
            this.lblTime = new System.Windows.Forms.Label();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblDay = new System.Windows.Forms.Label();
            this.timerForClock = new System.Windows.Forms.Timer(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.lblBlockNumber = new System.Windows.Forms.Label();
            this.timerForBlocks = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblBlockReward = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPriceUSD = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMoscowTime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMarketCapUSD = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDifficultyAdjEst = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblBTCInCirc = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblEstHashrate = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblTXInMempool = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.lblHashesToSolve = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblNextDifficultyChange = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl24HourTransCount = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl24HourBTCSent = new System.Windows.Forms.Label();
            this.btnMinimise = new System.Windows.Forms.Button();
            this.btnMoveWindow = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.lblAvgNoTransactions = new System.Windows.Forms.Label();
            this.lblStatusMessPart1 = new System.Windows.Forms.Label();
            this.lblStatusLight = new System.Windows.Forms.Label();
            this.lblSecsCountdown = new System.Windows.Forms.Label();
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.lblAlert = new System.Windows.Forms.Label();
            this.lblFees30Mins = new System.Windows.Forms.Label();
            this.lblfeesNextBlock = new System.Windows.Forms.Label();
            this.lblFees60Mins = new System.Windows.Forms.Label();
            this.lblFees1Day = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblTransInNextBlock = new System.Windows.Forms.Label();
            this.lblNextBlockMaxFee = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblNextBlockMinFee = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblNextBlockTotalFees = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label22 = new System.Windows.Forms.Label();
            this.btnSplash = new System.Windows.Forms.Button();
            this.lblTransactions = new System.Windows.Forms.Label();
            this.lblBlockSize = new System.Windows.Forms.Label();
            this.lblATH = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblATHDate = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblATHDifference = new System.Windows.Forms.Label();
            this.lbl24HrHigh = new System.Windows.Forms.Label();
            this.lbl24HrLow = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblAvgTimeBetweenBlocks = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTime.Font = new System.Drawing.Font("Technology", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.Gray;
            this.lblTime.Location = new System.Drawing.Point(30, 56);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(142, 55);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "88:88";
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSeconds.Font = new System.Drawing.Font("Technology", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeconds.ForeColor = System.Drawing.Color.Gray;
            this.lblSeconds.Location = new System.Drawing.Point(148, 73);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(47, 34);
            this.lblSeconds.TabIndex = 1;
            this.lblSeconds.Text = "88";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDate.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.Silver;
            this.lblDate.Location = new System.Drawing.Point(36, 105);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(152, 18);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "XXXXXXXX nnxx nnnn";
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDay.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDay.ForeColor = System.Drawing.Color.Silver;
            this.lblDay.Location = new System.Drawing.Point(35, 124);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(80, 18);
            this.lblDay.TabIndex = 3;
            this.lblDay.Text = "XXXXXXXXX";
            // 
            // timerForClock
            // 
            this.timerForClock.Interval = 1000;
            this.timerForClock.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Consolas", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Gray;
            this.btnExit.Location = new System.Drawing.Point(771, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(18, 20);
            this.btnExit.TabIndex = 4;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "x";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnExit.MouseHover += new System.EventHandler(this.button_MouseHover);
            // 
            // lblBlockNumber
            // 
            this.lblBlockNumber.AutoSize = true;
            this.lblBlockNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblBlockNumber.Font = new System.Drawing.Font("Technology", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlockNumber.ForeColor = System.Drawing.Color.Gray;
            this.lblBlockNumber.Location = new System.Drawing.Point(274, 56);
            this.lblBlockNumber.Name = "lblBlockNumber";
            this.lblBlockNumber.Size = new System.Drawing.Size(186, 55);
            this.lblBlockNumber.TabIndex = 5;
            this.lblBlockNumber.Text = "888888";
            // 
            // timerForBlocks
            // 
            this.timerForBlocks.Interval = 20000;
            this.timerForBlocks.Tick += new System.EventHandler(this.timerForBlocks_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label1.Location = new System.Drawing.Point(281, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Block height";
            // 
            // lblBlockReward
            // 
            this.lblBlockReward.AutoSize = true;
            this.lblBlockReward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblBlockReward.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlockReward.Location = new System.Drawing.Point(277, 208);
            this.lblBlockReward.Name = "lblBlockReward";
            this.lblBlockReward.Size = new System.Drawing.Size(90, 27);
            this.lblBlockReward.TabIndex = 7;
            this.lblBlockReward.Text = "888888";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label2.Location = new System.Drawing.Point(279, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Block reward (BTC)";
            // 
            // lblPriceUSD
            // 
            this.lblPriceUSD.AutoSize = true;
            this.lblPriceUSD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblPriceUSD.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceUSD.Location = new System.Drawing.Point(33, 208);
            this.lblPriceUSD.Name = "lblPriceUSD";
            this.lblPriceUSD.Size = new System.Drawing.Size(90, 27);
            this.lblPriceUSD.TabIndex = 10;
            this.lblPriceUSD.Text = "888888";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label4.Location = new System.Drawing.Point(35, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "1 BTC / USD";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Silver;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label5.Location = new System.Drawing.Point(36, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "1 USD / Sats";
            // 
            // lblMoscowTime
            // 
            this.lblMoscowTime.AutoSize = true;
            this.lblMoscowTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMoscowTime.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoscowTime.Location = new System.Drawing.Point(33, 248);
            this.lblMoscowTime.Name = "lblMoscowTime";
            this.lblMoscowTime.Size = new System.Drawing.Size(90, 27);
            this.lblMoscowTime.TabIndex = 12;
            this.lblMoscowTime.Text = "888888";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label6.Location = new System.Drawing.Point(36, 274);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "Market cap (USD)";
            // 
            // lblMarketCapUSD
            // 
            this.lblMarketCapUSD.AutoSize = true;
            this.lblMarketCapUSD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMarketCapUSD.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarketCapUSD.Location = new System.Drawing.Point(34, 289);
            this.lblMarketCapUSD.Name = "lblMarketCapUSD";
            this.lblMarketCapUSD.Size = new System.Drawing.Size(90, 27);
            this.lblMarketCapUSD.TabIndex = 14;
            this.lblMarketCapUSD.Text = "888888";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label3.Location = new System.Drawing.Point(279, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Next difficulty adjustment*";
            this.toolTip1.SetToolTip(this.label3, "(estimated)");
            // 
            // lblDifficultyAdjEst
            // 
            this.lblDifficultyAdjEst.AutoSize = true;
            this.lblDifficultyAdjEst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDifficultyAdjEst.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDifficultyAdjEst.Location = new System.Drawing.Point(277, 288);
            this.lblDifficultyAdjEst.Name = "lblDifficultyAdjEst";
            this.lblDifficultyAdjEst.Size = new System.Drawing.Size(90, 27);
            this.lblDifficultyAdjEst.TabIndex = 16;
            this.lblDifficultyAdjEst.Text = "888888";
            this.toolTip1.SetToolTip(this.lblDifficultyAdjEst, "(estimated)");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label7.Location = new System.Drawing.Point(36, 435);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(175, 15);
            this.label7.TabIndex = 23;
            this.label7.Text = "Total BTC in circulation";
            // 
            // lblBTCInCirc
            // 
            this.lblBTCInCirc.AutoSize = true;
            this.lblBTCInCirc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblBTCInCirc.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTCInCirc.Location = new System.Drawing.Point(33, 450);
            this.lblBTCInCirc.Name = "lblBTCInCirc";
            this.lblBTCInCirc.Size = new System.Drawing.Size(90, 27);
            this.lblBTCInCirc.TabIndex = 22;
            this.lblBTCInCirc.Text = "888888";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Silver;
            this.label13.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label13.Location = new System.Drawing.Point(279, 354);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(175, 15);
            this.label13.TabIndex = 21;
            this.label13.Text = "Est. hashrate (gigahash)";
            // 
            // lblEstHashrate
            // 
            this.lblEstHashrate.AutoSize = true;
            this.lblEstHashrate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblEstHashrate.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstHashrate.Location = new System.Drawing.Point(277, 369);
            this.lblEstHashrate.Name = "lblEstHashrate";
            this.lblEstHashrate.Size = new System.Drawing.Size(90, 27);
            this.lblEstHashrate.TabIndex = 20;
            this.lblEstHashrate.Text = "888888";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label11.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Silver;
            this.label11.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label11.Location = new System.Drawing.Point(555, 233);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(168, 15);
            this.label11.TabIndex = 19;
            this.label11.Text = "Transactions in Mempool";
            // 
            // lblTXInMempool
            // 
            this.lblTXInMempool.AutoSize = true;
            this.lblTXInMempool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTXInMempool.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTXInMempool.Location = new System.Drawing.Point(553, 248);
            this.lblTXInMempool.Name = "lblTXInMempool";
            this.lblTXInMempool.Size = new System.Drawing.Size(90, 27);
            this.lblTXInMempool.TabIndex = 18;
            this.lblTXInMempool.Text = "888888";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Silver;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label8.Location = new System.Drawing.Point(279, 394);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(245, 15);
            this.label8.TabIndex = 25;
            this.label8.Text = "Avg no. of attempts to solve block";
            // 
            // lblHashesToSolve
            // 
            this.lblHashesToSolve.AutoSize = true;
            this.lblHashesToSolve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblHashesToSolve.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHashesToSolve.Location = new System.Drawing.Point(277, 409);
            this.lblHashesToSolve.Name = "lblHashesToSolve";
            this.lblHashesToSolve.Size = new System.Drawing.Size(90, 27);
            this.lblHashesToSolve.TabIndex = 24;
            this.lblHashesToSolve.Text = "888888";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Silver;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label9.Location = new System.Drawing.Point(279, 314);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(252, 15);
            this.label9.TabIndex = 27;
            this.label9.Text = "Block of next difficulty adjustment";
            // 
            // lblNextDifficultyChange
            // 
            this.lblNextDifficultyChange.AutoSize = true;
            this.lblNextDifficultyChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNextDifficultyChange.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextDifficultyChange.Location = new System.Drawing.Point(277, 329);
            this.lblNextDifficultyChange.Name = "lblNextDifficultyChange";
            this.lblNextDifficultyChange.Size = new System.Drawing.Size(90, 27);
            this.lblNextDifficultyChange.TabIndex = 26;
            this.lblNextDifficultyChange.Text = "888888";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Silver;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label10.Location = new System.Drawing.Point(279, 435);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(182, 15);
            this.label10.TabIndex = 29;
            this.label10.Text = "24 hour transaction count";
            // 
            // lbl24HourTransCount
            // 
            this.lbl24HourTransCount.AutoSize = true;
            this.lbl24HourTransCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl24HourTransCount.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl24HourTransCount.Location = new System.Drawing.Point(277, 450);
            this.lbl24HourTransCount.Name = "lbl24HourTransCount";
            this.lbl24HourTransCount.Size = new System.Drawing.Size(90, 27);
            this.lbl24HourTransCount.TabIndex = 28;
            this.lbl24HourTransCount.Text = "888888";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Silver;
            this.label12.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label12.Location = new System.Drawing.Point(279, 475);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(217, 15);
            this.label12.TabIndex = 31;
            this.label12.Text = "24 hour number of bitcoin sent";
            // 
            // lbl24HourBTCSent
            // 
            this.lbl24HourBTCSent.AutoSize = true;
            this.lbl24HourBTCSent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl24HourBTCSent.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl24HourBTCSent.Location = new System.Drawing.Point(277, 490);
            this.lbl24HourBTCSent.Name = "lbl24HourBTCSent";
            this.lbl24HourBTCSent.Size = new System.Drawing.Size(90, 27);
            this.lbl24HourBTCSent.TabIndex = 30;
            this.lbl24HourBTCSent.Text = "888888";
            // 
            // btnMinimise
            // 
            this.btnMinimise.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnMinimise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimise.Font = new System.Drawing.Font("Consolas", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimise.ForeColor = System.Drawing.Color.Gray;
            this.btnMinimise.Location = new System.Drawing.Point(748, 11);
            this.btnMinimise.Name = "btnMinimise";
            this.btnMinimise.Size = new System.Drawing.Size(18, 20);
            this.btnMinimise.TabIndex = 32;
            this.btnMinimise.TabStop = false;
            this.btnMinimise.Text = "-";
            this.btnMinimise.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMinimise.UseVisualStyleBackColor = false;
            this.btnMinimise.Click += new System.EventHandler(this.btnMinimise_Click);
            this.btnMinimise.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnMinimise.MouseHover += new System.EventHandler(this.button_MouseHover);
            // 
            // btnMoveWindow
            // 
            this.btnMoveWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnMoveWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveWindow.Font = new System.Drawing.Font("Consolas", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveWindow.ForeColor = System.Drawing.Color.Gray;
            this.btnMoveWindow.Location = new System.Drawing.Point(699, 11);
            this.btnMoveWindow.Name = "btnMoveWindow";
            this.btnMoveWindow.Size = new System.Drawing.Size(44, 20);
            this.btnMoveWindow.TabIndex = 33;
            this.btnMoveWindow.TabStop = false;
            this.btnMoveWindow.Text = "move";
            this.btnMoveWindow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMoveWindow.UseVisualStyleBackColor = false;
            this.btnMoveWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMoveWindow_MouseDown);
            this.btnMoveWindow.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnMoveWindow.MouseHover += new System.EventHandler(this.button_MouseHover);
            this.btnMoveWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMoveWindow_MouseUp);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label14.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Silver;
            this.label14.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label14.Location = new System.Drawing.Point(555, 193);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(196, 15);
            this.label14.TabIndex = 35;
            this.label14.Text = "Avg transactions per block*";
            this.toolTip1.SetToolTip(this.label14, "calculated over the\r\nlast 100 blocks");
            // 
            // lblAvgNoTransactions
            // 
            this.lblAvgNoTransactions.AutoSize = true;
            this.lblAvgNoTransactions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAvgNoTransactions.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgNoTransactions.Location = new System.Drawing.Point(553, 208);
            this.lblAvgNoTransactions.Name = "lblAvgNoTransactions";
            this.lblAvgNoTransactions.Size = new System.Drawing.Size(90, 27);
            this.lblAvgNoTransactions.TabIndex = 34;
            this.lblAvgNoTransactions.Text = "888888";
            this.toolTip1.SetToolTip(this.lblAvgNoTransactions, "calculated over the\r\nlast 100 blocks");
            // 
            // lblStatusMessPart1
            // 
            this.lblStatusMessPart1.AutoSize = true;
            this.lblStatusMessPart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStatusMessPart1.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMessPart1.ForeColor = System.Drawing.Color.Gray;
            this.lblStatusMessPart1.Location = new System.Drawing.Point(27, 565);
            this.lblStatusMessPart1.Name = "lblStatusMessPart1";
            this.lblStatusMessPart1.Size = new System.Drawing.Size(294, 15);
            this.lblStatusMessPart1.TabIndex = 36;
            this.lblStatusMessPart1.Text = "Data updated successfully. Refreshing in ";
            // 
            // lblStatusLight
            // 
            this.lblStatusLight.AutoSize = true;
            this.lblStatusLight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStatusLight.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusLight.ForeColor = System.Drawing.Color.Silver;
            this.lblStatusLight.Location = new System.Drawing.Point(5, 565);
            this.lblStatusLight.Name = "lblStatusLight";
            this.lblStatusLight.Size = new System.Drawing.Size(23, 18);
            this.lblStatusLight.TabIndex = 37;
            this.lblStatusLight.Text = "🟢";
            // 
            // lblSecsCountdown
            // 
            this.lblSecsCountdown.AutoSize = true;
            this.lblSecsCountdown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSecsCountdown.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecsCountdown.ForeColor = System.Drawing.Color.Gray;
            this.lblSecsCountdown.Location = new System.Drawing.Point(257, 565);
            this.lblSecsCountdown.Name = "lblSecsCountdown";
            this.lblSecsCountdown.Size = new System.Drawing.Size(21, 15);
            this.lblSecsCountdown.TabIndex = 39;
            this.lblSecsCountdown.Text = "10";
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.AutoSize = true;
            this.lblErrorMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblErrorMessage.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMessage.ForeColor = System.Drawing.Color.Gray;
            this.lblErrorMessage.Location = new System.Drawing.Point(27, 546);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(0, 15);
            this.lblErrorMessage.TabIndex = 40;
            // 
            // lblAlert
            // 
            this.lblAlert.AutoSize = true;
            this.lblAlert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAlert.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlert.ForeColor = System.Drawing.Color.Gold;
            this.lblAlert.Location = new System.Drawing.Point(5, 542);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(0, 22);
            this.lblAlert.TabIndex = 41;
            // 
            // lblFees30Mins
            // 
            this.lblFees30Mins.AutoSize = true;
            this.lblFees30Mins.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblFees30Mins.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFees30Mins.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            this.lblFees30Mins.Location = new System.Drawing.Point(606, 103);
            this.lblFees30Mins.Name = "lblFees30Mins";
            this.lblFees30Mins.Size = new System.Drawing.Size(45, 20);
            this.lblFees30Mins.TabIndex = 42;
            this.lblFees30Mins.Text = "fees";
            // 
            // lblfeesNextBlock
            // 
            this.lblfeesNextBlock.AutoSize = true;
            this.lblfeesNextBlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblfeesNextBlock.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfeesNextBlock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            this.lblfeesNextBlock.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblfeesNextBlock.Location = new System.Drawing.Point(554, 103);
            this.lblfeesNextBlock.Name = "lblfeesNextBlock";
            this.lblfeesNextBlock.Size = new System.Drawing.Size(45, 20);
            this.lblfeesNextBlock.TabIndex = 43;
            this.lblfeesNextBlock.Text = "fees";
            // 
            // lblFees60Mins
            // 
            this.lblFees60Mins.AutoSize = true;
            this.lblFees60Mins.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblFees60Mins.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFees60Mins.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            this.lblFees60Mins.Location = new System.Drawing.Point(654, 103);
            this.lblFees60Mins.Name = "lblFees60Mins";
            this.lblFees60Mins.Size = new System.Drawing.Size(45, 20);
            this.lblFees60Mins.TabIndex = 44;
            this.lblFees60Mins.Text = "fees";
            // 
            // lblFees1Day
            // 
            this.lblFees1Day.AutoSize = true;
            this.lblFees1Day.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblFees1Day.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFees1Day.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            this.lblFees1Day.Location = new System.Drawing.Point(708, 103);
            this.lblFees1Day.Name = "lblFees1Day";
            this.lblFees1Day.Size = new System.Drawing.Size(45, 20);
            this.lblFees1Day.TabIndex = 45;
            this.lblFees1Day.Text = "fees";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label15.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Silver;
            this.label15.Location = new System.Drawing.Point(550, 85);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 18);
            this.label15.TabIndex = 46;
            this.label15.Text = "10m";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label16.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Silver;
            this.label16.Location = new System.Drawing.Point(551, 68);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(216, 18);
            this.label16.TabIndex = 47;
            this.label16.Text = "Transaction fees (sats/vB)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label17.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Silver;
            this.label17.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label17.Location = new System.Drawing.Point(555, 273);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(196, 15);
            this.label17.TabIndex = 48;
            this.label17.Text = "Transactions in next block*";
            this.toolTip1.SetToolTip(this.label17, "(estimated)");
            // 
            // lblTransInNextBlock
            // 
            this.lblTransInNextBlock.AutoSize = true;
            this.lblTransInNextBlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTransInNextBlock.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransInNextBlock.Location = new System.Drawing.Point(553, 288);
            this.lblTransInNextBlock.Name = "lblTransInNextBlock";
            this.lblTransInNextBlock.Size = new System.Drawing.Size(90, 27);
            this.lblTransInNextBlock.TabIndex = 49;
            this.lblTransInNextBlock.Text = "888888";
            this.toolTip1.SetToolTip(this.lblTransInNextBlock, "(estimated)");
            // 
            // lblNextBlockMaxFee
            // 
            this.lblNextBlockMaxFee.AutoSize = true;
            this.lblNextBlockMaxFee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNextBlockMaxFee.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextBlockMaxFee.Location = new System.Drawing.Point(553, 369);
            this.lblNextBlockMaxFee.Name = "lblNextBlockMaxFee";
            this.lblNextBlockMaxFee.Size = new System.Drawing.Size(90, 27);
            this.lblNextBlockMaxFee.TabIndex = 51;
            this.lblNextBlockMaxFee.Text = "888888";
            this.toolTip1.SetToolTip(this.lblNextBlockMaxFee, "(estimated)");
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label19.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Silver;
            this.label19.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label19.Location = new System.Drawing.Point(555, 354);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(161, 15);
            this.label19.TabIndex = 50;
            this.label19.Text = "Max fee in next block*";
            this.toolTip1.SetToolTip(this.label19, "(estimated)");
            // 
            // lblNextBlockMinFee
            // 
            this.lblNextBlockMinFee.AutoSize = true;
            this.lblNextBlockMinFee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNextBlockMinFee.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextBlockMinFee.Location = new System.Drawing.Point(553, 329);
            this.lblNextBlockMinFee.Name = "lblNextBlockMinFee";
            this.lblNextBlockMinFee.Size = new System.Drawing.Size(90, 27);
            this.lblNextBlockMinFee.TabIndex = 53;
            this.lblNextBlockMinFee.Text = "888888";
            this.toolTip1.SetToolTip(this.lblNextBlockMinFee, "(estimated)");
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label21.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Silver;
            this.label21.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label21.Location = new System.Drawing.Point(555, 314);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(161, 15);
            this.label21.TabIndex = 52;
            this.label21.Text = "Min fee in next block*";
            this.toolTip1.SetToolTip(this.label21, "(estimated)");
            // 
            // lblNextBlockTotalFees
            // 
            this.lblNextBlockTotalFees.AutoSize = true;
            this.lblNextBlockTotalFees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNextBlockTotalFees.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextBlockTotalFees.Location = new System.Drawing.Point(553, 409);
            this.lblNextBlockTotalFees.Name = "lblNextBlockTotalFees";
            this.lblNextBlockTotalFees.Size = new System.Drawing.Size(90, 27);
            this.lblNextBlockTotalFees.TabIndex = 55;
            this.lblNextBlockTotalFees.Text = "888888";
            this.toolTip1.SetToolTip(this.lblNextBlockTotalFees, "(estimated)");
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label20.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Silver;
            this.label20.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label20.Location = new System.Drawing.Point(555, 394);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(224, 15);
            this.label20.TabIndex = 54;
            this.label20.Text = "Total fees in next block (BTC)*";
            this.toolTip1.SetToolTip(this.label20, "(estimated)");
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 750;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label22.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Silver;
            this.label22.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label22.Location = new System.Drawing.Point(36, 45);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(77, 15);
            this.label22.TabIndex = 57;
            this.label22.Text = "Local time";
            // 
            // btnSplash
            // 
            this.btnSplash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnSplash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSplash.Font = new System.Drawing.Font("Consolas", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSplash.ForeColor = System.Drawing.Color.Gray;
            this.btnSplash.Location = new System.Drawing.Point(10, 11);
            this.btnSplash.Name = "btnSplash";
            this.btnSplash.Size = new System.Drawing.Size(18, 20);
            this.btnSplash.TabIndex = 58;
            this.btnSplash.Text = "?";
            this.btnSplash.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSplash.UseVisualStyleBackColor = false;
            this.btnSplash.Click += new System.EventHandler(this.btnSplash_Click);
            this.btnSplash.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnSplash.MouseHover += new System.EventHandler(this.button_MouseHover);
            // 
            // lblTransactions
            // 
            this.lblTransactions.AutoSize = true;
            this.lblTransactions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTransactions.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactions.ForeColor = System.Drawing.Color.Silver;
            this.lblTransactions.Location = new System.Drawing.Point(280, 105);
            this.lblTransactions.Name = "lblTransactions";
            this.lblTransactions.Size = new System.Drawing.Size(80, 18);
            this.lblTransactions.TabIndex = 59;
            this.lblTransactions.Text = "XXXXXXXXX";
            // 
            // lblBlockSize
            // 
            this.lblBlockSize.AutoSize = true;
            this.lblBlockSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblBlockSize.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlockSize.ForeColor = System.Drawing.Color.Silver;
            this.lblBlockSize.Location = new System.Drawing.Point(280, 124);
            this.lblBlockSize.Name = "lblBlockSize";
            this.lblBlockSize.Size = new System.Drawing.Size(80, 18);
            this.lblBlockSize.TabIndex = 60;
            this.lblBlockSize.Text = "XXXXXXXXX";
            // 
            // lblATH
            // 
            this.lblATH.AutoSize = true;
            this.lblATH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblATH.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblATH.Location = new System.Drawing.Point(33, 329);
            this.lblATH.Name = "lblATH";
            this.lblATH.Size = new System.Drawing.Size(90, 27);
            this.lblATH.TabIndex = 61;
            this.lblATH.Text = "888888";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label18.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Silver;
            this.label18.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label18.Location = new System.Drawing.Point(36, 314);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(140, 15);
            this.label18.TabIndex = 62;
            this.label18.Text = "All time high (USD)";
            // 
            // lblATHDate
            // 
            this.lblATHDate.AutoSize = true;
            this.lblATHDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblATHDate.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblATHDate.Location = new System.Drawing.Point(117, 336);
            this.lblATHDate.Name = "lblATHDate";
            this.lblATHDate.Size = new System.Drawing.Size(72, 18);
            this.lblATHDate.TabIndex = 63;
            this.lblATHDate.Text = "(888888)";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label23.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Silver;
            this.label23.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label23.Location = new System.Drawing.Point(36, 354);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(112, 15);
            this.label23.TabIndex = 65;
            this.label23.Text = "Change from ATH";
            // 
            // lblATHDifference
            // 
            this.lblATHDifference.AutoSize = true;
            this.lblATHDifference.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblATHDifference.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblATHDifference.Location = new System.Drawing.Point(33, 369);
            this.lblATHDifference.Name = "lblATHDifference";
            this.lblATHDifference.Size = new System.Drawing.Size(90, 27);
            this.lblATHDifference.TabIndex = 64;
            this.lblATHDifference.Text = "888888";
            // 
            // lbl24HrHigh
            // 
            this.lbl24HrHigh.AutoSize = true;
            this.lbl24HrHigh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl24HrHigh.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl24HrHigh.Location = new System.Drawing.Point(33, 410);
            this.lbl24HrHigh.Name = "lbl24HrHigh";
            this.lbl24HrHigh.Size = new System.Drawing.Size(90, 27);
            this.lbl24HrHigh.TabIndex = 66;
            this.lbl24HrHigh.Text = "888888";
            // 
            // lbl24HrLow
            // 
            this.lbl24HrLow.AutoSize = true;
            this.lbl24HrLow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl24HrLow.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl24HrLow.Location = new System.Drawing.Point(135, 410);
            this.lbl24HrLow.Name = "lbl24HrLow";
            this.lbl24HrLow.Size = new System.Drawing.Size(90, 27);
            this.lbl24HrLow.TabIndex = 67;
            this.lbl24HrLow.Text = "888888";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label26.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Silver;
            this.label26.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label26.Location = new System.Drawing.Point(36, 395);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(70, 15);
            this.label26.TabIndex = 68;
            this.label26.Text = "24hr high";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label24.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Silver;
            this.label24.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label24.Location = new System.Drawing.Point(135, 395);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(105, 15);
            this.label24.TabIndex = 69;
            this.label24.Text = "24hr low (USD)";
            // 
            // lblAvgTimeBetweenBlocks
            // 
            this.lblAvgTimeBetweenBlocks.AutoSize = true;
            this.lblAvgTimeBetweenBlocks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAvgTimeBetweenBlocks.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgTimeBetweenBlocks.Location = new System.Drawing.Point(278, 248);
            this.lblAvgTimeBetweenBlocks.Name = "lblAvgTimeBetweenBlocks";
            this.lblAvgTimeBetweenBlocks.Size = new System.Drawing.Size(90, 27);
            this.lblAvgTimeBetweenBlocks.TabIndex = 71;
            this.lblAvgTimeBetweenBlocks.Text = "888888";
            this.toolTip1.SetToolTip(this.lblAvgTimeBetweenBlocks, "(estimated)");
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label27.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Silver;
            this.label27.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label27.Location = new System.Drawing.Point(280, 233);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(196, 15);
            this.label27.TabIndex = 70;
            this.label27.Text = "Average time between blocks";
            this.toolTip1.SetToolTip(this.label27, "(estimated)");
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label25.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.Silver;
            this.label25.Location = new System.Drawing.Point(600, 85);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(32, 18);
            this.label25.TabIndex = 72;
            this.label25.Text = "30m";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label28.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Silver;
            this.label28.Location = new System.Drawing.Point(650, 85);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(32, 18);
            this.label28.TabIndex = 73;
            this.label28.Text = "60m";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label29.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.Silver;
            this.label29.Location = new System.Drawing.Point(700, 85);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(40, 18);
            this.label29.TabIndex = 74;
            this.label29.Text = "1day";
            // 
            // Statamoto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(800, 588);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.lblAvgTimeBetweenBlocks);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.lbl24HrLow);
            this.Controls.Add(this.lbl24HrHigh);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.lblATHDifference);
            this.Controls.Add(this.lblATHDate);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.lblATH);
            this.Controls.Add(this.lblBlockSize);
            this.Controls.Add(this.lblTransactions);
            this.Controls.Add(this.btnSplash);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.lblNextBlockTotalFees);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.lblNextBlockMinFee);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.lblNextBlockMaxFee);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.lblTransInNextBlock);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblFees1Day);
            this.Controls.Add(this.lblFees60Mins);
            this.Controls.Add(this.lblfeesNextBlock);
            this.Controls.Add(this.lblFees30Mins);
            this.Controls.Add(this.lblAlert);
            this.Controls.Add(this.lblErrorMessage);
            this.Controls.Add(this.lblSecsCountdown);
            this.Controls.Add(this.lblStatusLight);
            this.Controls.Add(this.lblStatusMessPart1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblAvgNoTransactions);
            this.Controls.Add(this.btnMoveWindow);
            this.Controls.Add(this.btnMinimise);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lbl24HourBTCSent);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbl24HourTransCount);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblNextDifficultyChange);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblHashesToSolve);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblBTCInCirc);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblEstHashrate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblTXInMempool);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDifficultyAdjEst);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblMarketCapUSD);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblMoscowTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblPriceUSD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblBlockReward);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBlockNumber);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblSeconds);
            this.Controls.Add(this.lblTime);
            this.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Statamoto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statamoto";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblSeconds;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.Timer timerForClock;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblBlockNumber;
        private System.Windows.Forms.Timer timerForBlocks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBlockReward;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPriceUSD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMoscowTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMarketCapUSD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDifficultyAdjEst;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblBTCInCirc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblEstHashrate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblTXInMempool;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblHashesToSolve;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblNextDifficultyChange;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl24HourTransCount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl24HourBTCSent;
        private System.Windows.Forms.Button btnMinimise;
        private System.Windows.Forms.Button btnMoveWindow;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblAvgNoTransactions;
        private System.Windows.Forms.Label lblStatusMessPart1;
        private System.Windows.Forms.Label lblStatusLight;
        private System.Windows.Forms.Label lblSecsCountdown;
        private System.Windows.Forms.Label lblErrorMessage;
        private System.Windows.Forms.Label lblAlert;
        private System.Windows.Forms.Label lblFees30Mins;
        private System.Windows.Forms.Label lblfeesNextBlock;
        private System.Windows.Forms.Label lblFees60Mins;
        private System.Windows.Forms.Label lblFees1Day;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblTransInNextBlock;
        private System.Windows.Forms.Label lblNextBlockMaxFee;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblNextBlockMinFee;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblNextBlockTotalFees;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnSplash;
        private System.Windows.Forms.Label lblTransactions;
        private System.Windows.Forms.Label lblBlockSize;
        private System.Windows.Forms.Label lblATH;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblATHDate;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblATHDifference;
        private System.Windows.Forms.Label lbl24HrHigh;
        private System.Windows.Forms.Label lbl24HrLow;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblAvgTimeBetweenBlocks;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
    }
}

