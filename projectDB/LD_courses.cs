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
    public partial class LD_courses : Form
    {
        private int user_id;
        public LD_courses(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
        }
        private void PopulateCheckedListBox()
        {
            // Assuming you have a list of course names
            string[] courseNames = {
                "Programming Fundamentals Lab",
                "Operating Systems Lab",
                "Digital Logic Design Lab",
                "English Lab",
                "Functional English Lab",
                "Data Base Lab",
                "Object Oriented Programming Lab",
                "Data Structures Lab"
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

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedCourses = checkedListBox1.CheckedItems;

            // Validate GPA before proceeding
            if (IsValidGPA(textBox1.Text) && IsValidGPA(textBox2.Text) && IsValidGPA(textBox3.Text))
            {
                if (selectedCourses.Count <= 3)
                {
                    int LD_id = 0;
                    string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                        "Database=TA Management system;Integrated Security=True;";

                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();

                    string querygetTAID = "SELECT LD_id from LD where user_id = @user_id";
                    SqlCommand commandTAID = new SqlCommand(querygetTAID, connection);
                    commandTAID.Parameters.AddWithValue("@user_id", user_id);

                    object result = commandTAID.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        LD_id = (int)result;
                    }

                    foreach (var courseName in selectedCourses)
                    {
                        using (SqlCommand command = new SqlCommand(
                            "INSERT INTO Courses_LD (LDgrade, LD_id, course_id) " +
                            "SELECT @grade, @LD_id, c.course_id " +
                            "FROM Course c " +
                            "WHERE c.course_name = @courseName;",
                            connection))
                        {
                            command.Parameters.AddWithValue("@grade", GetCourseGrade(courseName.ToString()));
                            command.Parameters.AddWithValue("@LD_id", LD_id);
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
                MessageBox.Show("Sorry you are not eligible for LDship. If you want to choose some other course make sure your grade is between (A+, A-, A, B+, B-, or B).");
            }
        }

        private bool IsValidGPA(string gpa)
        {
            string[] validGPAs = { "A+", "A-", "A", "B+", "B-", "B" };
            return validGPAs.Contains(gpa.ToUpper());
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LD_portal LD_portal = new LD_portal(user_id);
            LD_portal.Show();

        }

        private void LD_courses_Load(object sender, EventArgs e)
        {

        }
    }
}

