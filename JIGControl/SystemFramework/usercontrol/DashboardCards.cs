using DocumentFormat.OpenXml.Bibliography;
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

namespace QCInventoryF2.usercontrol
{
    public partial class DashboardCards : UserControl
    {
        public DashboardCards(int month,int year, int total, int value)
        {
            InitializeComponent();
            string monthName = new DateTime(year, month, 1)
                                         .ToString("MMMM yyyy");
            lblCardTitle.Text = monthName;
            lblCardValue.Text = value.ToString("N0") +" / "+ total.ToString("N0");
            LoadSummary(month);

        }

        public void LoadSummary(int monthselect)
        {
            string query = @"
        SELECT
            jm.section AS Section,
            SUM(CASE WHEN ji.jig_id IS NOT NULL THEN 1 ELSE 0 END) AS Scanned,
            SUM(CASE WHEN ji.jig_id IS NULL THEN 1 ELSE 0 END) AS Missing,
            COUNT(jm.jig_id) AS Total   
           
        FROM jig_masterlist jm
        LEFT JOIN jig_inventory ji 
            ON ji.jig_id = jm.jig_id 
           AND ji.scan_month = @month
           AND ji.scan_year = YEAR(CURRENT_DATE())
        GROUP BY jm.section
        ORDER BY jm.section;
    ";

            dbQueries.LoadGrid(query, dtSummary,
                new MySqlParameter("@month", monthselect)
            );
        }


    }
}
