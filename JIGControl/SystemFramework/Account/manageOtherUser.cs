using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using QCInventoryF2.Database;

namespace QCInventoryF2.Account
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public partial class manageOtherUser : Form
    {
        private DataTable usersTable;
        private bool isDirty = false; // track changes

        public manageOtherUser()
        {
            InitializeComponent();
        }

        private void manageOtherUser_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            usersTable = dbQueries.GetDataTable("SELECT * FROM trc_user.jig_users");
            datagridUsers.DataSource = usersTable;

            datagridUsers.Columns["password"].Visible = false;
            datagridUsers.Columns["user_id"].Visible = false;

            AddResetPasswordIcon();

            // Track changes
            usersTable.RowChanged += (s, ev) => isDirty = true;
            usersTable.RowDeleted += (s, ev) => isDirty = true;
        }

        private void AddResetPasswordIcon()
        {
            if (datagridUsers.Columns.Contains("resetPassword"))
                return;

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn
            {
                Name = "resetPassword",
                HeaderText = "",
                Image = Properties.Resources.deleteKey,
                Width = 40
            };

            datagridUsers.Columns.Add(imgCol);
        }

        private void datagridUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == datagridUsers.Columns["resetPassword"].Index)
            {
                string username = datagridUsers.Rows[e.RowIndex]
                    .Cells["username"].Value.ToString();

                int userID = Convert.ToInt32(
                    datagridUsers.Rows[e.RowIndex].Cells["user_id"].Value
                );

                DialogResult result = MessageBox.Show(
                    $"Do you want to reset the password for user: {username}?",
                    "Reset Password",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    if (User.ResetPassword(userID))
                    {
                        MessageBox.Show("Password has been reset.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Action failed.",
                            "Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ===== Prompt to save on closing =====
        private void manageOtherUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isDirty)
                return;

            DialogResult result = MessageBox.Show(
                "You have unsaved changes. Do you want to save before exiting?",
                "Save Changes",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            if (result == DialogResult.Yes)
            {
                if (!SaveAllUsers())
                {
                    MessageBox.Show("Failed to save changes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        private bool SaveAllUsers()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString.ConnectionString))
                {
                    con.Open();

                    foreach (DataRow row in usersTable.Rows)
                    {
                        if (row.RowState == DataRowState.Deleted)
                            continue;

                        if (row["user_id"] == DBNull.Value)
                            continue;

                        int userId = Convert.ToInt32(row["user_id"]);

                        string username = row["username"].ToString();
                        string firstname = row["firstname"].ToString();
                        string lastname = row["lastname"].ToString();

                        // IMPORTANT: These are BOOL columns
                        bool admin = Convert.ToBoolean(row["admin"]);
                      

                        string query = @"
                    UPDATE trc_user.jig_users   
                    SET username = @username,
                        firstname = @firstname,
                        lastname = @lastname,
                        admin = @admin
                       
                    WHERE user_id = @user_id";

                        using (MySqlCommand cmd = new MySqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@firstname", firstname);
                            cmd.Parameters.AddWithValue("@lastname", lastname);
                            cmd.Parameters.AddWithValue("@admin", admin);
             
                            cmd.Parameters.AddWithValue("@user_id", userId);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                usersTable.AcceptChanges();
                isDirty = false;
                return true;
            }
            catch
            {
                return false;
            }
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (SaveAllUsers())
            {
                MessageBox.Show(
                    "All changes have been saved.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    
                );
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Failed to save changes.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            using (AddUser addUserForm = new AddUser())
            {
                if (addUserForm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the users table after adding a new user
                    usersTable = dbQueries.GetDataTable("SELECT * FROM trc_user.jig_users");
                    datagridUsers.DataSource = usersTable;
                    isDirty = false; // reset dirty flag
                }
                LoadUsers();

            }
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
