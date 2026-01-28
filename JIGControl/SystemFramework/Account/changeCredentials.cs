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
    public partial class changeCredentials : Form
    {
        public changeCredentials()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtFirstname.Text == "" || txtLastname.Text == "")
            {
                MessageBox.Show("All Fields Required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Capture the result
            DialogResult result = MessageBox.Show(
                "Are you sure you want to change your details?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success = User.UpdateCredential(txtUsername.Text, txtFirstname.Text, txtLastname.Text);

            if (success)
            {
                MessageBox.Show("Details changed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                User.UserName = txtUsername.Text;
                User.Firstname = txtFirstname.Text;
                User.Lastname = txtLastname.Text;

                this.Close();
            }
            else
            {
                MessageBox.Show("Unable to Update Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void changeCredentials_Load(object sender, EventArgs e)
        {
            txtUsername.Text = User.UserName;
            txtFirstname.Text = User.Firstname;
            txtLastname.Text = User.Lastname;
        }
    }
}
