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

namespace QCInventoryF2.Account
{
    public partial class setSection : Form
    {
        public setSection()
        {
            InitializeComponent();
        }

        private void setSection_Load(object sender, EventArgs e)
        {
         cmbsection.Text = Properties.Settings.Default.Section;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Section = cmbsection.Text;

            Properties.Settings.Default.Save();

            MessageBox.Show("Section saved!");
        }
    }
}
