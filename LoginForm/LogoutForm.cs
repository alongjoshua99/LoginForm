using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LoginForm
{
    public partial class LogoutForm : Form
    {
        private string userId;

        public LogoutForm(string firstName, string timeIn, string userId)
        {
            InitializeComponent();
            labelname.Text = "" + firstName;
            timein.Text = "" + timeIn;
            this.userId = userId;
        }

        private void LogoutForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.labeltime.Text = datetime.ToString();
        }

        private void SignButt_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=cl_ams_db;Uid=root;Pwd=''"))
                {
                    connection.Open();

                    string updateQuery = "UPDATE attendance_logs SET time_out = @time_out WHERE id = (SELECT MAX(id) FROM attendance_logs WHERE student_id = @user_id)";

                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@time_out", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        updateCommand.Parameters.AddWithValue("@user_id", userId);

                        updateCommand.ExecuteNonQuery();
                    }
                }

                // Refresh the data in the form
                RefreshData();

                // Hide the form or navigate to another form
                this.Hide();
                ComlabLogin comlabLogin = new ComlabLogin();
                comlabLogin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Public method to refresh data
        public void RefreshData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=cl_ams_db;Uid=root;Pwd=''"))
                {
                    connection.Open();

                    // Re-query data from the database using the userId
                    string selectQuery = "SELECT first_name, time_in FROM students WHERE id = @user_id";

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@user_id", userId);

                        using (MySqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update your UI controls with the refreshed data
                                labelname.Text = reader["first_name"].ToString();
                                timein.Text = reader["time_in"].ToString();
                                // Update other controls as needed
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}");
            }
        }
    }
}