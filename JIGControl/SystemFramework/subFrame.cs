using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            btnFOrm.PerformClick();
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
    }
}
