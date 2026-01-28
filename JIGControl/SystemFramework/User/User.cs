using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QCInventoryF2.Database;
namespace QCInventoryF2
{
    static class User
    {
        public static string UserID;
        public static string UserName;
        public static string Firstname;
        public static string Lastname;

        public static bool isAdmin;
        public static string userSection = Properties.Settings.Default.Section;




        public static bool getUser(string username, string password)
        {
            try
            {
                using (var conn = Database.conString.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM trc_user.jig_users WHERE username=@username AND password=@password";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UserID = reader["user_id"].ToString();
                                UserName = reader["username"].ToString();
                                Firstname = reader["firstname"].ToString();
                                Lastname = reader["lastname"].ToString();
                                isAdmin = Convert.ToBoolean(reader["admin"]);
                                return true;
                            }
                            else
                            {
                               return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public static bool ChangePassword(string currentPassword, string newPassword)
        {
            using (var conn = Database.conString.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE trc_user.jig_users
                         SET password = @newpass 
                         WHERE user_id = @userid 
                         AND password = @oldpass";

                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@newpass", newPassword);
                    cmd.Parameters.AddWithValue("@oldpass", currentPassword);
                    cmd.Parameters.AddWithValue("@userid", User.UserID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // ✅ true = success, false = failed
                }
            }
        }

        public static bool ResetPassword(int userID)
        {
            using (var conn = Database.conString.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE trc_user.jig_users
                         SET password = '' 
                         WHERE user_id = @userid";

                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userid", userID);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // ✅ true = success, false = failed
                }
            }
        }

        public static bool UpdateCredential( string username, string firstName, string lastName)
        {
            using (var conn = Database.conString.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE trc_user.jig_users
                         SET username = @username,
                             Firstname = @firstname,
                             Lastname = @lastname
                         WHERE user_id = @userid";

                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@firstname", firstName);
                    cmd.Parameters.AddWithValue("@lastname", lastName);
                    cmd.Parameters.AddWithValue("@userid", UserID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }






    }

}
