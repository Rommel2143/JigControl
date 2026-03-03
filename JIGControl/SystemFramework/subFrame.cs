using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QCInventoryF2
{
    public partial class subFrame : Form
    {
        public subFrame()
        {
            InitializeComponent();
        }


        public void displayForm(Form frm)
        {
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.ShowInTaskbar = false;
            panelForm.Controls.Add(frm);
            lblTittle.Text = frm.Text;
            frm.BringToFront();
            frm.Show();
        }


        private void subFrame_Load(object sender, EventArgs e)
        {

            btnDashboard.PerformClick();
          

        }

      


        private void btnFOrm_Click(object sender, EventArgs e)
        {
            displayForm(new JIG.ManageJIG());

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            userMenu.Show(btnUser,
                  new Point(0, btnUser.Height));
        }

        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayForm(new Account.accountSettings());
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            displayForm(new JIG.JIGInventory());
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            displayForm(new Dashboard());
        }

        private void checkUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string updatePath = @"\\ptif1-ds\SystemServer\JIGControl\setup.exe";

            try
            {
                if (File.Exists(updatePath))
                {
                    DialogResult result = MessageBox.Show(
                        "New update found.\nDo you want to install now?",
                        "Update Available",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        Process.Start(updatePath);
                        Application.Exit(); // Close current app before updating
                    }
                }
                else
                {
                    MessageBox.Show(
                        "No updates available.",
                        "Check Updates",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error checking for updates:\n" + ex.Message,
                    "Update Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
