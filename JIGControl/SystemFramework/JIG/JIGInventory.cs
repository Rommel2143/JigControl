using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using QCInventoryF2.Database;
using QCInventoryF2.JIG.Reporting;
using QCInventoryF2.Model;
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
    public partial class JIGInventory : Form
    {
        Model.JIG jig = new Model.JIG();


        public JIGInventory()
        {
            InitializeComponent();
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
            lblSectionName.Text = "Section: " + Properties.Settings.Default.Section;


        }


        private void JIGInventory_Load(object sender, EventArgs e)
        {
            cmbStatus.SelectedItem = "ALL";
          
        }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                if (txtQR.Text.Contains("TRCJIG|"))
                {
                    string[] qrParts = txtQR.Text.Split('|');

                    if (qrParts.Length == 2 && int.TryParse(qrParts[1], out int jigId))
                    {
                        jig.JigID = jigId;
                        jig.getDetails();

                        lblControlno.Text = "Control No: " + jig.ControlNo;
                        lblPartcode.Text = "Partcode: " + jig.Partcode;
                        lblPartname.Text = "Partname: " + jig.Partname;
                        lblSection.Text = "Section: " + jig.Section;
                        txtStatus.Clear();
                        txtStatus.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Invalid QR Code format.");
                    }
                }
            }
        }



        private void ProcessJIGInsert()
        {
            try
            {
                string input = txtQR.Text.Trim();
                if (!string.IsNullOrEmpty(input))
                {
                    if (JIGQueries.InsertJIG(txtQR.Text,txtStatus.Text ))
                    {
                        Loaddata("");
                      

                    }


                }
                else
                {
                    MessageBox.Show("Please enter a valid input.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtQR.Clear();
                txtQR.Focus();
            }
        }



        private void Loaddata(string search)
        {
            dbQueries.LoadGrid(
                "SELECT ji.record_id, " +
                "jm.control_no, " +
                "jm.partname, " +
                "jm.section, " +
                "ji.inventory_status, " +
                "CONCAT(UPPER(LEFT(ju.Firstname,1)), '. ', " +
                "UPPER(LEFT(ju.Lastname,1)), LOWER(SUBSTRING(ju.Lastname,2))) AS 'User', " +
                "ji.timestamp " +
                "FROM jig_inventory ji " +
                "JOIN jig_masterlist jm ON ji.jig_id = jm.jig_id " +
                "JOIN trc_user.jig_users ju ON ju.user_id = ji.user_id " +
                "WHERE ji.inventory_status LIKE '%" + search + "%' AND ji.scan_month = " + (cmbMonth.SelectedIndex + 1) + " AND ji.scan_year = YEAR(CURRENT_DATE()) AND jm.section = '" + Properties.Settings.Default.Section + "' " +
                "ORDER BY ji.record_id DESC",
                datagrid1
            );
            LoadMissing();
            LoadCount();
            LoadMissingCount();
        }

        private void LoadCount()
        {
            string query = @"SELECT COUNT(ji.record_id) FROM jig_inventory ji
                        JOIN jig_masterlist jm ON jm.jig_id = ji.jig_id
                        WHERE scan_year=YEAR(CURDATE()) AND scan_month=@month AND jm.section=@section";
            using (var con = new MySqlConnection(conString.ConnectionString))
            using (var cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@month", cmbMonth.SelectedIndex + 1);
                cmd.Parameters.AddWithValue("@section", Properties.Settings.Default.Section);
                con.Open();
                object result = cmd.ExecuteScalar();

                lblScanned.Text = "Scanned(" + result +")";
            }
        }


        private void LoadMissingCount()
        {
            string query = @"
        SELECT COUNT(jm.jig_id)
        FROM jig_masterlist jm
        LEFT JOIN jig_inventory ji 
            ON ji.jig_id = jm.jig_id
            AND ji.scan_month = @month
            AND ji.scan_year = YEAR(CURRENT_DATE())
        WHERE jm.section = @section
        AND ji.jig_id IS NULL";

            using (var con = new MySqlConnection(conString.ConnectionString))
            using (var cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@month", cmbMonth.SelectedIndex + 1);
                cmd.Parameters.AddWithValue("@section", Properties.Settings.Default.Section);

                con.Open();
                object result = cmd.ExecuteScalar();

                lblMissing.Text = "Missing (" + result + ")";
            }
        }



        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ExportData.ToExcel(datagrid1, "JIG-Inventory");
        }

        private void guna2TextBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtQR.Text))
                {
                    ProcessJIGInsert();
                    txtStatus.Clear();
                    txtQR.Clear();
                    txtQR.Focus();
                }
             
            }
        }

        private void txtStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            menuReport.Show(btnReport, 0, btnReport.Height);
        }

        private void dataCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printData print= new printData();
            print.ShowDialog();
            print.BringToFront();
        }

        private void dashboardReportingToolStripMenuItem_Click(object sender, EventArgs e)
        {
              printGraph print= new printGraph();
            print.ShowDialog();
            print.BringToFront();
        }

        private void missingJIGReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MissingJigReport print = new MissingJigReport();
            print.ShowDialog();
            print.BringToFront();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbStatus.Text == "ALL")
            {
                Loaddata("");
             
            }
            else
            {
                Loaddata(cmbStatus.Text);
            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.Text == "ALL")
            {
                Loaddata("");
             
            }
            else
            {
                Loaddata(cmbStatus.Text);
            
            }
        }


        public void LoadMissing()
        {
            string query = @"
        SELECT
            jm.control_no,
            jm.partname
        FROM jig_masterlist jm
        LEFT JOIN jig_inventory ji 
            ON ji.jig_id = jm.jig_id 
            AND ji.scan_month = @month
            AND ji.scan_year = YEAR(CURRENT_DATE())
        WHERE ji.record_id IS NULL
          AND jm.section = @section
    ";

            dbQueries.LoadGrid(query, dtMissing,
                new MySqlParameter("@month", cmbMonth.SelectedIndex+1),
                new MySqlParameter("@section", Properties.Settings.Default.Section)
            );
        }
    }
}
