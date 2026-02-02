using MySql.Data.MySqlClient;
using QCInventoryF2.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCInventoryF2.Model
{
   public class JIG
    {
        public int JigID {  get; set; }
        public string ControlNo { get; set; }
        public string Partname { get; set; }
        public string Partcode { get; set; }    
        public string Section { get; set; }
        public string Status { get; set; }

        public void getDetails()
        {
            string query = @"SELECT `jig_id`, `control_no`, `partname`, `partcode`, `section`, `status` FROM `jig_masterlist` WHERE jig_id =" + JigID + " ";
            using (MySqlConnection con = new MySqlConnection(conString.ConnectionString))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(query,con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader()) 
                    { 
                        if (reader.Read())
                        {
                            JigID = reader.GetInt32("jig_id");
                            ControlNo = reader.GetString("control_no");
                            Partname = reader.GetString("partname");
                            Partcode = reader.GetString("partcode");
                            Status = reader.GetString("status");
                            Section = reader.GetString("section");
                        }
                    }

                }
            }
        }









    }
}
