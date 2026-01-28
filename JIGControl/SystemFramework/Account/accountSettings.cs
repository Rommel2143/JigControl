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
    public partial class accountSettings : Form
    {
        public accountSettings()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            changePassword cp = new changePassword();
            cp.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            changeCredentials cc = new changeCredentials();
            cc.ShowDialog();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            manageOtherUser mou = new manageOtherUser();
            mou.ShowDialog();
        }

        private void accountSettings_Load(object sender, EventArgs e)
        {
                guna2Button3.Enabled = User.isAdmin;
           lblUser.Text= "Hello, " + User.UserName;
            lblFullname.Text = User.Firstname + " " + User.Lastname;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            setSection ss = new setSection();
            
                ss.ShowDialog();
                ss.BringToFront();
            


        }
    }
}
