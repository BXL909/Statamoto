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
        public bool BitcoinExplorerEndpointsEnabled { get; set; }
        public bool BlockchainInfoEndpointsEnabled { get; set; }

        public bool BitcoinExplorerOrgJSONEnabled { get; set; }
        public bool BlockchainInfoJSONEnabled { get; set; }

        public bool CoingeckoComJSONEnabled { get; set; }

        public bool BlockchairComJSONEnabled { get; set; }

        public bool MempoolSpaceLightningJSONEnabled { get; set; }

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
        }

        private void btnExitSettings_Click(object sender, EventArgs e)
        {
            
        }

        private void settings_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
        }

        private void lblBitcoinExplorerEndpoints_Click(object sender, EventArgs e)
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

        private void lblBlockchainInfoEndpoints_Click(object sender, EventArgs e)
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

        private void lblBlockchainExplorerJSON_Click(object sender, EventArgs e)
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

        private void lblBlockchainInfoJSON_Click(object sender, EventArgs e)
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

        private void lblCoingeckoComJSON_Click(object sender, EventArgs e)
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

        private void lblBlockchairComJSON_Click(object sender, EventArgs e)
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

        private void lblMempoolLightningJSON_Click(object sender, EventArgs e)
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

        private void settingsScreen_FormClosing(object sender, FormClosingEventArgs e)
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
            // APIGroup1RefreshInMinsSelection = (int)numericUpDownAPIGroup1.Value;
        }

        private void button_MouseHover(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.Gray;
            button.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1D1D1D");
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = System.Drawing.ColorTranslator.FromHtml("#1D1D1D");
            button.ForeColor = Color.Gray;
        }

        private void numericUpDownAPIGroup1_ValueChanged(object sender, EventArgs e)
        {
            APIGroup1RefreshInMinsSelection = (int)numericUpDownAPIGroup1.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            APIGroup2RefreshInHoursSelection = (int)numericUpDownAPIGroup2.Value;
        }


    }
}
