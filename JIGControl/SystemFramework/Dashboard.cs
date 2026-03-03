using DocumentFormat.OpenXml.Presentation;
using MySql.Data.MySqlClient;
using QCInventoryF2.usercontrol;    
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QCInventoryF2.Database;

namespace QCInventoryF2
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            LoadDashboard();
        }

        public void LoadDashboard()
        {
            LoadCards();
        }

        private void LoadCards()
        {
            FlowCard.Controls.Clear();

            using (MySqlConnection con = new MySqlConnection(conString.ConnectionString))
            {
                con.Open();

                // 1️⃣ Get total expected jigs from masterlist
                int totalMaster = 0;

                using (MySqlCommand cmdMaster = new MySqlCommand(
                    "SELECT COUNT(jig_id) FROM jig_masterlist", con))
                {
                    totalMaster = Convert.ToInt32(cmdMaster.ExecuteScalar());
                }

                // 2️⃣ Get monthly scanned totals
                string query = @"
            SELECT scan_year, scan_month,
                   COUNT(DISTINCT jig_id) AS TotalScanned
            FROM jig_inventory
            GROUP BY scan_year, scan_month
            ORDER BY scan_year, scan_month";

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int year = Convert.ToInt32(reader["scan_year"]);
                        int month = Convert.ToInt32(reader["scan_month"]);
                        int scanned = Convert.ToInt32(reader["TotalScanned"]);

                      

                        DashboardCards card = new DashboardCards(
                           month,year,
                            totalMaster,  // Expected
                            scanned       // Actual
                        );

                        FlowCard.Controls.Add(card);
                    }
                }
            }
        }


    }
}
   