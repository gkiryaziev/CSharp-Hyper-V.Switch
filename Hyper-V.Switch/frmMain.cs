using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hyper_V.Switch
{
    public partial class frmMain : Form
    {
        SwitchManager sm = new SwitchManager();
        Parser parser = new Parser();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            GetStatus();
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            // switch to Auto
            sm.Run(RunAction.AUTO);

            // get new status
            GetStatus();

            // force reboot
            if (MessageBox.Show(this, "You must reboot to apply changes.\n\nReboot NOW?",
                "Reboot", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
                sm.Run(RunAction.REBOOT);
            }
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            // switch to Off
            sm.Run(RunAction.OFF);

            // get new status
            GetStatus();

            // force reboot
            if (MessageBox.Show(this, "You must reboot to apply changes.\n\nReboot NOW?",
                "Reboot", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
                sm.Run(RunAction.REBOOT);
            }
        }

        // ======================================
        // GetStatus()
        // ======================================
        private void GetStatus()
        {
            Result res = sm.Run(RunAction.STATUS);
            if (res.Error == "" && res.Output != "")
            {
                lblStatus.Text = parser.Parse(RunAction.STATUS, res.Output);

                if (lblStatus.Text == "Auto")
                {
                    btnAuto.Enabled = false;
                    btnOff.Enabled = true;
                }
                else if (lblStatus.Text == "Off")
                {
                    btnAuto.Enabled = true;
                    btnOff.Enabled = false;
                }
            }
        }
    }
}
