using MySql.Data.MySqlClient;
using QCInventoryF2.Database;
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
    public partial class AddJIG : Form
    {
        public AddJIG()
        {
            InitializeComponent();
        }


        private void reloadJIG()
        {
           dbQueries.LoadGrid ($"SELECT jig_id, control_no, partcode, partname FROM jig_masterlist WHERE section = '{cmbsection.Text}'", datagrid1);

            if (!datagrid1.Columns.Contains("Delete"))
            {
                var btn = new DataGridViewImageColumn
                {
                    HeaderText = "",
                    Name = "Delete",
                    Image = Properties.Resources.trash_bin
                };
                datagrid1.Columns.Add(btn);
            }


            datagrid1.Columns["jig_id"].Visible = false;
            if (!User.isAdmin)
            {
                datagrid1.Columns["delete"].Visible = false;
            }
        }

        private void AddJIG_Load(object sender, EventArgs e)
        {
            cmbsection.Text = Properties.Settings.Default.Section;
            reloadJIG();
        }

        private void cmbsection_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadJIG();
        }

        private void Guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var con = new MySqlConnection(conString.ConnectionString))
                {

                    con.Open();
                    foreach (DataGridViewRow row in datagrid1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string updateQuery = "UPDATE jig_masterlist SET partcode = @partcode, partname = @partname, control_no = @control_no WHERE jig_id = @jig_id";
                        using (var cmd = new MySqlCommand(updateQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@partcode", row.Cells["partcode"].Value.ToString());
                            cmd.Parameters.AddWithValue("@partname", row.Cells["partname"].Value.ToString());
                            cmd.Parameters.AddWithValue("@control_no", row.Cells["control_no"].Value.ToString());
                            cmd.Parameters.AddWithValue("@jig_id", row.Cells["jig_id"].Value.ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                reloadJIG();
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtcontrolno.Text))
            {
                MessageBox.Show("Please input Control Number.");
                return;
            }

            string controlno = txtcontrolno.Text;
            string partcode = txtpartcode.Text;
            string partname = txtpartname.Text;

            if (!InsertToJIG())
            {
                txtcontrolno.Clear();
                txtcontrolno.Focus();
                return;
            }

            reloadJIG();
            txtcontrolno.Clear();
            txtcontrolno.Focus();
        }

        private bool InsertToJIG()
        {
            string query = "INSERT INTO jig_masterlist (control_no, partcode, partname, section) " +
                           "VALUES (@control_no, @partcode, @partname, @section)";

            try
            {
                using (var con = new MySqlConnection(conString.ConnectionString))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@control_no", txtcontrolno.Text);
                        cmd.Parameters.AddWithValue("@partcode", txtpartcode.Text);
                        cmd.Parameters.AddWithValue("@partname", txtpartname.Text);
                        cmd.Parameters.AddWithValue("@section", cmbsection.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data: " + ex.Message);
                return false;
            }
            finally
            {
              
            }
        }

        private void Guna2Button3_Click(object sender, EventArgs e)
        {
            reloadJIG();
        }

        private void datagrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datagrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datagrid1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                string controlNo = datagrid1.Rows[e.RowIndex].Cells["jig_id"].Value.ToString();
                var confirmResult = MessageBox.Show("Are you sure to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        using (var con = new MySqlConnection(conString.ConnectionString))
                        {
                          con.Open();
                            string deleteQuery = "DELETE FROM jig_masterlist WHERE jig_id = @jig_id";
                            using (var cmd = new MySqlCommand(deleteQuery, con))
                            {
                                cmd.Parameters.AddWithValue("@jig_id", controlNo);
                                cmd.ExecuteNonQuery();
                            }
                            MessageBox.Show("Item deleted successfully.");
                            reloadJIG();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting data: " + ex.Message);
                    }
                   
                }
            }
        }
    }
}

