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

                string query = @"
           SELECT 
    ji.scan_year,
    ji.scan_month,
    COUNT(DISTINCT ji.jig_id) AS TotalScanned

FROM jig_inventory ji
INNER JOIN jig_masterlist jm
    ON jm.jig_id = ji.jig_id

GROUP BY ji.scan_year, ji.scan_month
ORDER BY ji.scan_year, ji.scan_month;
        ";

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    var results = new List<(int year, int month, int scanned)>();

                    while (reader.Read())
                    {
                        results.Add((
                            Convert.ToInt32(reader["scan_year"]),
                            Convert.ToInt32(reader["scan_month"]),
                            Convert.ToInt32(reader["TotalScanned"])
                        ));
                    }

                    reader.Close();

                    foreach (var r in results)
                    {
                        var startDate = new DateTime(r.year, r.month, 1);
                        var endDate = startDate.AddMonths(1);

                        using (MySqlCommand cmdTotal = new MySqlCommand(@"
                   SELECT COUNT(jig_id)
                    FROM jig_masterlist
                    WHERE timestamp < @endDate;
                     ", con))
                        {
                            cmdTotal.Parameters.AddWithValue("@endDate", endDate);

                            int totalMaster = Convert.ToInt32(cmdTotal.ExecuteScalar());

                            DashboardCards card = new DashboardCards(
                                r.month,
                                r.year,
                                totalMaster,
                                r.scanned
                            );

                            FlowCard.Controls.Add(card);
                        }
                    }
                }
            }
        }


    }
}
   