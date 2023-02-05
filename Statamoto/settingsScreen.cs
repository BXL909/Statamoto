using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Statamoto
{
    public partial class settingsScreen : Form
    {
        private bool hasAPISelectorComboBoxJustBeenPainted = true;
        public bool BitcoinExplorerEndpointsEnabled { get; set; }
        public bool BlockchainInfoEndpointsEnabled { get; set; }
        public bool BitcoinExplorerOrgJSONEnabled { get; set; }
        public bool BlockchainInfoJSONEnabled { get; set; }
        public bool CoingeckoComJSONEnabled { get; set; }
        public bool BlockchairComJSONEnabled { get; set; }
        public bool MempoolSpaceLightningJSONEnabled { get; set; }
        public string NodeURL { get; set; }
        public int APIGroup1RefreshInMinsSelection { get; set; } = 1;
        public int APIGroup2RefreshInHoursSelection { get; set; } = 24;
        public static settingsScreen Instance { get; private set; }
        
        public static void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = new settingsScreen();
            }
        }

        public settingsScreen()
        {
            InitializeComponent();
            comboAPISelectorForQueries.SelectedIndex = 1;
        }

        private void BtnExitSettings_Click(object sender, EventArgs e)
        {
            
        }

        private void Settings_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
        }

        private void LblBitcoinExplorerEndpoints_Click(object sender, EventArgs e)
          {
              if (lblBitcoinExplorerEndpoints.Text == "✔️")
              {
                  lblBitcoinExplorerEndpoints.ForeColor= Color.Red;
                  lblBitcoinExplorerEndpoints.Text = "❌";
              }
              else
              {
                  lblBitcoinExplorerEndpoints.ForeColor = Color.Green;
                  lblBitcoinExplorerEndpoints.Text = "✔️";
              }
         }

        private void LblBlockchainInfoEndpoints_Click(object sender, EventArgs e)
        {
            if (lblBlockchainInfoEndpoints.Text == "✔️")
            {
                lblBlockchainInfoEndpoints.ForeColor = Color.Red;
                lblBlockchainInfoEndpoints.Text = "❌";
            }
            else
            {
                lblBlockchainInfoEndpoints.ForeColor = Color.Green;
                lblBlockchainInfoEndpoints.Text = "✔️";
            }
        }

        private void LblBlockchainExplorerJSON_Click(object sender, EventArgs e)
        {
            if (lblBlockchainExplorerJSON.Text == "✔️")
            {
                lblBlockchainExplorerJSON.ForeColor = Color.Red;
                lblBlockchainExplorerJSON.Text = "❌";
            }
            else
            {
                lblBlockchainExplorerJSON.ForeColor = Color.Green;
                lblBlockchainExplorerJSON.Text = "✔️";
            }
        }

        private void LblBlockchainInfoJSON_Click(object sender, EventArgs e)
        {
            if (lblBlockchainInfoJSON.Text == "✔️")
            {
                lblBlockchainInfoJSON.ForeColor = Color.Red;
                lblBlockchainInfoJSON.Text = "❌";
            }
            else
            {
                lblBlockchainInfoJSON.ForeColor = Color.Green;
                lblBlockchainInfoJSON.Text = "✔️";
            }
        }

        private void LblCoingeckoComJSON_Click(object sender, EventArgs e)
        {
            if (lblCoingeckoComJSON.Text == "✔️")
            {
                lblCoingeckoComJSON.ForeColor = Color.Red;
                lblCoingeckoComJSON.Text = "❌";
            }
            else
            {
                lblCoingeckoComJSON.ForeColor = Color.Green;
                lblCoingeckoComJSON.Text = "✔️";
            }
        }

        private void LblBlockchairComJSON_Click(object sender, EventArgs e)
        {
            if (lblBlockchairComJSON.Text == "✔️")
            {
                lblBlockchairComJSON.ForeColor = Color.Red;
                lblBlockchairComJSON.Text = "❌";
            }
            else
            {
                lblBlockchairComJSON.ForeColor = Color.Green;
                lblBlockchairComJSON.Text = "✔️";
            }
        }

        private void LblMempoolLightningJSON_Click(object sender, EventArgs e)
        {
            if (lblMempoolLightningJSON.Text == "✔️")
            {
                lblMempoolLightningJSON.ForeColor = Color.Red;
                lblMempoolLightningJSON.Text = "❌";
            }
            else
            {
                lblMempoolLightningJSON.ForeColor = Color.Green;
                lblMempoolLightningJSON.Text = "✔️";
            }
        }

        private void SettingsScreen_FormClosing(object sender, FormClosingEventArgs e)  // make sure all values are passed back to calling screen
        {
            if (lblBitcoinExplorerEndpoints.Text == "✔️")
            {
                BitcoinExplorerEndpointsEnabled = true;
            }
            else
            {
                BitcoinExplorerEndpointsEnabled = false;
            }
            if (lblBlockchainInfoEndpoints.Text == "✔️")
            {
                BlockchainInfoEndpointsEnabled = true;
            }
            else
            {
                BlockchainInfoEndpointsEnabled = false;
            }
            if (lblBlockchainExplorerJSON.Text == "✔️")
            {
                BitcoinExplorerOrgJSONEnabled = true;
            }
            else
            {
                BitcoinExplorerOrgJSONEnabled = false;
            }
            if (lblBlockchainInfoJSON.Text == "✔️")
            {
                BlockchainInfoJSONEnabled = true;
            }
            else
            {
                BlockchainInfoJSONEnabled = false;
            }
            if (lblCoingeckoComJSON.Text == "✔️")
            {
                CoingeckoComJSONEnabled = true;
            }
            else
            {
                CoingeckoComJSONEnabled = false;
            }
            if (lblBlockchairComJSON.Text == "✔️")
            {
                BlockchairComJSONEnabled = true;
            }
            else
            {
                BlockchairComJSONEnabled = false;
            }
            if (lblMempoolLightningJSON.Text == "✔️")
            {
                MempoolSpaceLightningJSONEnabled = true;
            }
            else
            {
                MempoolSpaceLightningJSONEnabled = false;
            }
            if (comboAPISelectorForQueries.SelectedIndex == 0)
            {
                NodeURL = "https://blockstream.info/api/";
            }
            if (comboAPISelectorForQueries.SelectedIndex == 1)
            {
                NodeURL = "https://mempool.space/api/";
            }
        }

        private void NumericUpDownAPIGroup1_ValueChanged(object sender, EventArgs e) // number of mins between refreshes for API group 1 has been changed
        {
            APIGroup1RefreshInMinsSelection = (int)numericUpDownAPIGroup1.Value;
        }

        private void NumericUpDownAPIGroup2_ValueChanged(object sender, EventArgs e) // number of mins between refreshes for API group 2 has been changed
        {
            APIGroup2RefreshInHoursSelection = (int)numericUpDownAPIGroup2.Value;
        }

        private void ComboAPISelectorForQueries_SelectedIndexChanged(object sender, EventArgs e) // set the node to be used for user queries
        {
            if (!hasAPISelectorComboBoxJustBeenPainted) // ignore the selectedindexchanged event that occurs during form paint
            {
                if (comboAPISelectorForQueries.SelectedIndex == 0)
                {
                    NodeURL = "https://blockstream.info/api/";
                }
                if (comboAPISelectorForQueries.SelectedIndex == 1)
                {
                NodeURL = "https://mempool.space/api/";
                }
            }
            hasAPISelectorComboBoxJustBeenPainted = false; // ignored first event now
        }
    }
}
