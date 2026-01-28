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
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();
        }

        public void displayForm(Form frm)
        {
           
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;     // better than WindowState.Maximized
            frm.ShowInTaskbar = false;

           panel1.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }


        private void MainFrame_Load(object sender, EventArgs e)
        {
           
            displayForm(new subFrame());
        }
    }
}
