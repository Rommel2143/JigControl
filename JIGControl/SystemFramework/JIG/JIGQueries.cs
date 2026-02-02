using DocumentFormat.OpenXml.Spreadsheet;
using QCInventoryF2.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace QCInventoryF2.JIG
{
    public static class JIGQueries
    {



        public static bool InsertJIG(string JigQRCode,string status)
        {
            if (JigQRCode.Split('|').Length != 2 || !JigQRCode.Contains("TRCJIG"))
                throw new Exception("Invalid JIG QR Code format.");

            int jigID = int.Parse(JigQRCode.Split('|')[1]);

            string checkQuery = "SELECT COUNT(*) FROM jig_masterlist WHERE jig_id = @jigID";

            try
            {
                using (var con = new MySqlConnection(conString.ConnectionString))
                {
                    con.Open();

                    using (var cmd = new MySqlCommand(checkQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@jigID", jigID);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 0)
                            throw new Exception("JIG QR Code does not exist in the database.");

                        return InserttoDB(JigQRCode, jigID, con,status);
                    }
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                // 🔒 UNIQUE index violation (QRcode + month/year)
                throw new Exception("This JIG was already scanned for the current month.");
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error verifying JIG QR Code: " + ex.Message);
            }
        }


        private static bool InserttoDB(string JigQRCode, int jigID, MySqlConnection con,string status)
        {
            string insertQuery ="INSERT INTO jig_inventory (QRcode, jig_id, user_id, inventory_status,inventory_remarks) VALUES (@qr, @jigID, @user_id,@inventory_status,@inventory_remarks)";

            using (var insertCmd = new MySqlCommand(insertQuery, con))
            {
                insertCmd.Parameters.AddWithValue("@qr", JigQRCode);
                insertCmd.Parameters.AddWithValue("@jigID", jigID);
                insertCmd.Parameters.AddWithValue("@user_id", User.UserID);
                insertCmd.Parameters.AddWithValue("@inventory_status",status);
                insertCmd.Parameters.AddWithValue("@inventory_remarks", "");

                int rowsAffected = insertCmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }















        //////////////////////////////
    }
}
