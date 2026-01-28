using MySql.Data.MySqlClient;

namespace QCInventoryF2.Database
{
  public static class conString
    {
        // Change values as needed
        public static string ConnectionString = "server=PTI-027s;user id=Inventory;password=inventory123@;database=trcsystem";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
