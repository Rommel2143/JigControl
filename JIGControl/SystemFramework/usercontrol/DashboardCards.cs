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

        //    public void LoadSummary(int monthselect)
        //    {
        //        var year = DateTime.Now.Year;

        //        var startDate = new DateTime(year, monthselect, 1);
        //        var endDate = startDate.AddMonths(1);

        //        string query = @"
        //    SELECT
        //        jm.section AS Section,
        //         SUM(CASE WHEN ji.jig_id IS NOT NULL THEN 1 ELSE 0 END) AS Scanned,

        //        SUM(CASE WHEN ji.jig_id IS NULL  AND DATE(jm.timestamp) <= @fromDate THEN 1 ELSE 0 END) AS Missing,
        //        COUNT(jm.jig_id) AS Total



        //    FROM jig_masterlist jm
        //    LEFT JOIN jig_inventory ji 
        //        ON ji.jig_id = jm.jig_id 
        //       AND ji.scan_month = @month
        //       AND ji.scan_year = @year

        //    WHERE jm.timestamp < @endDate

        //    GROUP BY jm.section
        //    ORDER BY jm.section;
        //";

        //        dbQueries.LoadGrid(query, dtSummary,
        //            new MySqlParameter("@month", monthselect),
        //            new MySqlParameter("@year", year),
        //             new MySqlParameter("@fromDate", startDate),
        //            new MySqlParameter("@endDate", endDate)
        //        );
        //    }
        public void LoadSummary(int monthselect)
        {
            var year = DateTime.Now.Year;

            var startDate = new DateTime(year, monthselect, 1);
            var endDate = startDate.AddMonths(1);

            string query = @"
        SELECT
            jm.section AS Section,

            -- Total jigs existing BEFORE the month
            COUNT(jm.jig_id) AS Total,

            -- Scanned only within the selected month
            SUM(CASE 
                    WHEN ji.jig_id IS NOT NULL THEN 1 
                    ELSE 0 
                END) AS Scanned,

            -- Missing = existed before month but not scanned in that month
            SUM(CASE 
                    WHEN ji.jig_id IS NULL THEN 1 
                    ELSE 0 
                END) AS Missing

        FROM jig_masterlist jm

        LEFT JOIN jig_inventory ji 
            ON ji.jig_id = jm.jig_id
            AND ji.scan_month = @month
            AND ji.scan_year = @year

        WHERE jm.timestamp < @startDate

        GROUP BY jm.section
        ORDER BY jm.section;
    ";

            dbQueries.LoadGrid(query, dtSummary,
                new MySqlParameter("@month", monthselect),
                new MySqlParameter("@year", year),
                new MySqlParameter("@startDate", startDate)
            );
        }
    }
}
