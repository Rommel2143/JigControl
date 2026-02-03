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
                "UPPER(LEFT(ju.Lastname,1)), LOWER(SUBSTRING(ju.Lastname,2))) AS 'User Responsible', " +
                "ji.timestamp " +
                "FROM jig_inventory ji " +
                "JOIN jig_masterlist jm ON ji.jig_id = jm.jig_id " +
                "JOIN trc_user.jig_users ju ON ju.user_id = ji.user_id " +
                "WHERE ji.inventory_status LIKE '%" + search + "%' " +
                "ORDER BY jm.section,ji.inventory_status DESC",
                datagrid1
            );
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
    }
}
