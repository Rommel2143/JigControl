using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QCInventoryF2.Database
{
    public static class dbQueries
    {

        private static string connString = conString.ConnectionString;

        public static void LoadGrid(string query, Guna2DataGridView datagrid)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    datagrid.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }

        }

        public static void LoadGrid(string query, DataGridView grid, params MySqlParameter[] parameters)
        {
            using (var con = new MySqlConnection(connString))
            using (var cmd = new MySqlCommand(query, con))
            {
                // Add parameters if any
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    grid.DataSource = dt;
                }
            }
        }
        public static void LoadComboBox(string query, Guna2ComboBox combobox)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                combobox.Items.Clear();

                while (reader.Read())
                {
                    combobox.Items.Add(reader[0].ToString());
                }

                reader.Close();
            }
        }



        public static DataTable GetDataTable(string query)
        {
            try
            {

                DataTable dt = new DataTable();

                using (MySqlConnection con = new MySqlConnection(conString.ConnectionString))
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, con))
                {
                    con.Open();
                    da.Fill(dt);
                }

                return dt;
            }
            catch (Exception ex)
            {
               throw new Exception("Failed to retrieve data table." + ex.Message);
            }
           
        }


        public static object ExecuteScalar(string query, Dictionary<string, object> parameters = null)
        {
            using (var con = new MySqlConnection(connString))
            using (var cmd = new MySqlCommand(query, con))
            {
                // Add parameters if provided
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                con.Open();
                return cmd.ExecuteScalar();
            }
        }



    }

    }
