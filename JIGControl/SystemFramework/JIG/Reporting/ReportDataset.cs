using MySql.Data.MySqlClient;
using System.Data.Common;
using QCInventoryF2.Database;
using System;
namespace QCInventoryF2.JIG.Reporting
{
    partial class ReportDataset
    {


        public void LoadJIGData()
        {
            string query = @"
                SELECT 
                    ji.jig_id,
                    jm.control_no,
                    jm.partname,
                    jm.partcode,
                    jm.section,
                    CONCAT(
                        UPPER(LEFT(ju.Firstname,1)), '. ',
                        UPPER(LEFT(ju.Lastname,1)), LOWER(SUBSTRING(ju.Lastname,2))
                    ) AS user,
                    ji.inventory_status,
                    ji.timestamp
                FROM jig_inventory ji
                JOIN jig_masterlist jm ON ji.jig_id = jm.jig_id
                JOIN trc_user.jig_users ju ON ju.user_id = ji.user_id
                ORDER BY ji.timestamp,jm.section DESC;
            ";

            using (var con = new MySqlConnection(conString.ConnectionString))
            using (var cmd = new MySqlCommand(query, con))
            {
                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    this.JIG_Data.Clear();

                    while (dr.Read())
                    {
                        var row = this.JIG_Data.NewJIG_DataRow();

                        row.jig_id = Convert.ToInt32(dr["jig_id"]);
                        row.control_no = dr["control_no"].ToString();
                        row.partname = dr["partname"].ToString();
                        row.partcode = dr["partcode"].ToString();
                        row.section = dr["section"].ToString();
                        row.user = dr["user"].ToString();
                        row.inventory_status = dr["inventory_status"].ToString();
                        row.timestamp = dr["timestamp"].ToString();

                        this.JIG_Data.Rows.Add(row);
                    }
                }
            }
        }


        public void LoadJIGMissing()
        {
            string query = @"SELECT 
                                jm.jig_id,
                                jm.control_no,
                                jm.partname,
                                jm.partcode,
                                jm.section
                            FROM jig_masterlist jm
                            LEFT JOIN jig_inventory ji ON ji.jig_id = jm.jig_id
                            WHERE ji.jig_id IS NULL
                            ORDER BY jm.section DESC;";


            using (var con = new MySqlConnection(conString.ConnectionString))
            using (var cmd = new MySqlCommand(query, con))
            {
                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    this.JIG_Missing.Clear();

                    while (dr.Read())
                    {
                        var row = this.JIG_Missing.NewJIG_MissingRow();

                        row.jig_id = Convert.ToInt32(dr["jig_id"]);
                        row.control_no = dr["control_no"].ToString();
                        row.partname = dr["partname"].ToString();
                        row.partcode = dr["partcode"].ToString();
                        row.section = dr["section"].ToString();
                        this.JIG_Missing.Rows.Add(row);
                    }
                }
            }
        }

        public void LoadJIGSummary()
        {
            string query = @"
        SELECT
            jm.section,
            COUNT(jm.jig_id) AS total_jig,
            SUM(CASE WHEN ji.jig_id IS NULL THEN 1 ELSE 0 END) AS total_missing,
            ROUND(
                (COUNT(jm.jig_id) - SUM(CASE WHEN ji.jig_id IS NULL THEN 1 ELSE 0 END))
                / COUNT(jm.jig_id) * 100, 2
            ) AS percentage
        FROM jig_masterlist jm
        LEFT JOIN jig_inventory ji ON ji.jig_id = jm.jig_id
        GROUP BY jm.section
        ORDER BY jm.section;
    ";

            using (var con = new MySqlConnection(conString.ConnectionString))
            using (var cmd = new MySqlCommand(query, con))
            {
                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    this.JIG_Summary.Clear();

                    while (dr.Read())
                    {
                        var row = this.JIG_Summary.NewJIG_SummaryRow();

                        row.section = dr["section"].ToString();
                        row.total_jig = Convert.ToInt32(dr["total_jig"]);
                        row.total_missing = Convert.ToInt32(dr["total_missing"]);
                        row.percentage = dr["percentage"].ToString() + "%";

                        this.JIG_Summary.Rows.Add(row);
                    }
                }
            }
        }




    }
}
