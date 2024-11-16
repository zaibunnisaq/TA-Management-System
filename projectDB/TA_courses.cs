using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectDB
{
    public partial class TA_courses : Form
    {
        private int user_id;
        public TA_courses(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void PopulateCheckedListBox()
        {
            // Assuming you have a list of course names
            string[] courseNames = {
                "Programming Fundamentals",
                "Operating Systems",
                "Digital Logic Design",
                "Differential Equations",
                "English",
                "Functional English",
                "Applied Physics",
                "Islamiat",
                "Pakistan Studies",
                "Calculas",
                "Data Base",
                "Software Design and Analysis",
                "Design and Analysis of Algorithms",
                "Object Oriented Programming"
            };

            // Populate the CheckedListBox with course names
            checkedListBox1.Items.AddRange(courseNames);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TA_courses_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedCourses = checkedListBox1.CheckedItems;

            // Validate GPA before proceeding
            if (IsValidGPA(textBox1.Text) && IsValidGPA(textBox2.Text) && IsValidGPA(textBox3.Text))
            {
                if (selectedCourses.Count <= 3)
                {
                    int TA_id = 0;
                    string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                        "Database=TA Management system;Integrated Security=True;";

                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();

                    string querygetTAID = "SELECT TA_id from TA where user_id = @user_id";
                    SqlCommand commandTAID = new SqlCommand(querygetTAID, connection);
                    commandTAID.Parameters.AddWithValue("@user_id", user_id);

                    object result = commandTAID.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        TA_id = (int)result;
                    }

                    foreach (var courseName in selectedCourses)
                    {
                        using (SqlCommand command = new SqlCommand(
                            "INSERT INTO Courses_TA (TAgrade, TA_id, course_id) " +
                            "SELECT @grade, @TA_id, c.course_id " +
                            "FROM Course c " +
                            "WHERE c.course_name = @courseName;",
                            connection))
                        {
                            command.Parameters.AddWithValue("@grade", GetCourseGrade(courseName.ToString()));
                            command.Parameters.AddWithValue("@TA_id", TA_id);
                            command.Parameters.AddWithValue("@courseName", courseName.ToString());

                            command.ExecuteNonQuery();
                        }
                    }

                    connection.Close();

                    MessageBox.Show("Courses saved successfully!");
                }
                else
                {
                    MessageBox.Show("Please select up to three courses ONLY.");
                }
            }
            else
            {
                MessageBox.Show("Sorry you are not eligible for TAship. If you want to choose some other course make sure your grade is between (A+, A-, A, B+, B-, or B).");
            }
        }


        private string GetCourseGrade(string courseName)
        {
            switch (courseName)
            {
                case "Course1": return textBox1.Text;
                case "Course2": return textBox2.Text;
                case "Course3": return textBox3.Text;
                default: return string.Empty;
            }
        }
        private bool IsValidGPA(string gpa)
        {
            string[] validGPAs = { "A+", "A-", "A", "B+", "B-", "B" };
            return validGPAs.Contains(gpa.ToUpper());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            TA_portal TA_portal = new TA_portal(user_id);
            TA_portal.Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

