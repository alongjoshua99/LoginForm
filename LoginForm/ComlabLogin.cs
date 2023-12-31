using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LoginForm
{
    public partial class ComlabLogin : Form
    {
        public static MySqlConnection connection = new MySqlConnection("Server=localhost;Database=cl_ams_db;Uid=root;Pwd=''");
        MySqlCommand command;
        MySqlDataReader mdr;

        public ComlabLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            timer1.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.BackColor = System.Drawing.Color.Transparent;
        }

        private void SignButt_Click(object sender, EventArgs e)
        {
            string username = txtEmail.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please input Username and Password", "Error");
            }
            else
            {
                using (MySqlConnection connection = ComlabLogin.connection)
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM students WHERE email = @username";
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        using (MySqlDataReader mdr = command.ExecuteReader())
                        {
                            if (mdr.Read())
                            {
                                string email = mdr["email"].ToString();
                                string userId = mdr["id"].ToString(); // Store the value before closing the mdr
                                string firstName = string.Empty;

                                // Retrieve the student's first name using a separate query
                                string selectFirstNameQuery = "SELECT first_name FROM students WHERE email = @email";

                                // Close the mdr DataReader before opening firstNameReader
                                mdr.Close();

                                using (MySqlCommand firstNameCommand = new MySqlCommand(selectFirstNameQuery, connection))
                                {
                                    firstNameCommand.Parameters.AddWithValue("@email", email);

                                    using (MySqlDataReader firstNameReader = firstNameCommand.ExecuteReader())
                                    {
                                        if (firstNameReader.Read())
                                        {
                                            firstName = firstNameReader["first_name"].ToString();
                                        }
                                    }
                                }

                                // Insert a record into the computer_status_logs table
                                string insertLogQuery = "INSERT INTO computer_status_logs (user_id, computer_id, status, response, description, checked_at, created_at, updated_at) VALUES (@user_id, @computer_id, @status, @response, @description, @checked_at, @created_at, @updated_at)";

                                using (MySqlCommand insertLogCommand = new MySqlCommand(insertLogQuery, connection))
                                {
                                    insertLogCommand.Parameters.AddWithValue("@user_id", userId); // Use the stored value
                                    insertLogCommand.Parameters.AddWithValue("@computer_id", 1);
                                    insertLogCommand.Parameters.AddWithValue("@status", "working");
                                    insertLogCommand.Parameters.AddWithValue("@response", DBNull.Value);
                                    insertLogCommand.Parameters.AddWithValue("@description", DBNull.Value);
                                    insertLogCommand.Parameters.AddWithValue("@checked_at", DateTime.Now);
                                    insertLogCommand.Parameters.AddWithValue("@created_at", DateTime.Now);
                                    insertLogCommand.Parameters.AddWithValue("@updated_at", DateTime.Now);

                                    insertLogCommand.ExecuteNonQuery();
                                }

                                // Insert a record into the attendance_logs table
                                string insertAttendanceQuery = "INSERT INTO attendance_logs (teacher_class_id, student_id, faculty_member_id, computer_id, sy_id, semester_id, remarks, time_in, deleted_at, created_at, updated_at) VALUES (@teacher_class_id, @student_id, @faculty_member_id, @computer_id, @sy_id, @semester_id, @remarks, @time_in, @deleted_at, @created_at, @updated_at)";

                                using (MySqlCommand insertAttendanceCommand = new MySqlCommand(insertAttendanceQuery, connection))
                                {
                                    insertAttendanceCommand.Parameters.AddWithValue("@teacher_class_id", 3);
                                    insertAttendanceCommand.Parameters.AddWithValue("@student_id", 4);
                                    insertAttendanceCommand.Parameters.AddWithValue("@faculty_member_id", DBNull.Value); // Assuming it's nullable
                                    insertAttendanceCommand.Parameters.AddWithValue("@computer_id", 1);
                                    insertAttendanceCommand.Parameters.AddWithValue("@sy_id", 1);
                                    insertAttendanceCommand.Parameters.AddWithValue("@semester_id", 1);
                                    insertAttendanceCommand.Parameters.AddWithValue("@remarks", "present");
                                    insertAttendanceCommand.Parameters.AddWithValue("@time_in", DateTime.Now);
                                    insertAttendanceCommand.Parameters.AddWithValue("@deleted_at", DBNull.Value);
                                    insertAttendanceCommand.Parameters.AddWithValue("@created_at", DateTime.Now);
                                    insertAttendanceCommand.Parameters.AddWithValue("@updated_at", DateTime.Now);

                                    insertAttendanceCommand.ExecuteNonQuery();
                                }

                                MessageBox.Show("Login Successful!");

                                // Refresh the data in the LogoutForm
                                this.Hide();
                                LogoutForm logoutForm = new LogoutForm(firstName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userId);

                                // Add a method in LogoutForm to refresh the data

                                logoutForm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Login Information! Try again.");
                            }
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.label4.Text = datetime.ToString();
        }

        private void RegButt_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegForm RegForm = new RegForm();
            RegForm.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Your code for label2 click event
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            // Your code for groupBox1_Enter event
        }
    }
}