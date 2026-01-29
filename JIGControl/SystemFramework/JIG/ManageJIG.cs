using Guna.UI2.WinForms;
using QCInventoryF2.Database;
using QCInventoryF2.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QCInventoryF2.JIG
{
    public partial class ManageJIG : Form
    {
        public ManageJIG()
        {
            InitializeComponent();
        }


        private void reloadJIG()
        {
            if (User.userSection == "")
            {
                MessageBox.Show("User section is not defined. Please set Section first.");
                return;
            }
             dbQueries.LoadGrid($"SELECT jig_id, control_no, partcode, partname ,status,updatestamp FROM jig_masterlist WHERE section = '{User.userSection}'", datagrid1);

            datagrid1.Columns["jig_id"].Visible = false;
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddJIG addJIG = new AddJIG();
            addJIG.ShowDialog();
            addJIG.BringToFront();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                if (User.userSection == "")
                {
                    MessageBox.Show("User section is not defined. Please set Section first.");
                    return;
                }

                string jigID = guna2TextBox1.Text.Split('|')[1].Trim();
                string query = @"SELECT * FROM jig_masterlist WHERE jig_id = '" + jigID + "' AND section ='" + User.userSection + "'";
                using (var conn = Database.conString.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblControlno.Text = "Control No: " + reader["control_no"].ToString();
                                lblPartcode.Text = "Partcode: " + reader["partcode"].ToString();
                                lblPartname.Text = "Partname: " + reader["partname"].ToString();
                                lbljigID.Text = reader["jig_id"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("JIG not found.");
                            }
                        }
                    }
                }

            }
        }

        private void ManageJIG_Load(object sender, EventArgs e)
        {
            reloadJIG();
            lblSection.Text = "Section: " + User.userSection;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (lbljigID.Text == "jigID")
            {
                MessageBox.Show("Please scan a JIG first.");
                return;
            }
            try
            {
                string query = @"UPDATE jig_masterlist SET status = '" + cmbStatus.Text + "' WHERE jig_id = " + Convert.ToInt32(lbljigID.Text) + "";
                using (var conn = Database.conString.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("JIG returned successfully.");
                        reloadJIG();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ExportData.ToExcel(datagrid1, "JIG-Masterlist-" + User.userSection);

        }
    }
}