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
    public partial class manageLD : Form
    {
        private int user_id;
         public manageLD(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
        }

        private void showdatgrid(int user_id)
        {

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                "Database=TA Management system;Integrated Security=True;";

            string selectedcourse = comboBox1.SelectedItem.ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select ld.LD_id,ld.first_name, ld.last_name, cld.LDgrade " +
               "from LD ld " +
               "join Courses_LD cld on ld.LD_id = cld.LD_id " +
               "join Course c on c.course_id = cld.Course_id " +
               "where c.course_name = @selectedcourse;";


                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@selectedcourse", selectedcourse);


                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

            }
        }

        private void showdatgrid2(int user_id)
        {
            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                                      "Database=TA Management system;Integrated Security=True;";

            string selectedcourse = comboBox1.SelectedItem.ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "select s.sec_name " +
            "from Section s " +
            "join SectionCourse sc on sc.section_id = s.section_id " +
            "join Course c on c.course_id = sc.course_id " +
            "where c.course_name = @selectedcourse";


                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@selectedcourse", selectedcourse);


                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
            }
        }

        private void LoadCoursesComboBox()
        {
            comboBox1.Items.Add("Programming Fundamentals Lab");
            comboBox1.Items.Add("Operating Systems Lab");
            comboBox1.Items.Add("Digital Logic Design Lab");
            comboBox1.Items.Add("English Lab");
            comboBox1.Items.Add("Functional English Lab");
            comboBox1.Items.Add("Database Lab");
            comboBox1.Items.Add("Object-Oriented Programming Lab");
            comboBox1.Items.Add("Data Structures Lab");
            comboBox1.SelectedIndex = 0;
        }
        private void manageLD_Load(object sender, EventArgs e)
        {
            LoadCoursesComboBox();
        }

        private void newallocation(int user_id)
        {
            string selectedcourse = comboBox1.SelectedItem.ToString();
            string section = maskedTextBox1.Text;
            string LD_id = textBox1.Text;
            string date = maskedTextBox2.Text;

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string checkQuery = "SELECT COUNT(*) FROM AllocationLD WHERE LD_id = @LD_id AND SC_id = " +
                                    "(SELECT SC_id FROM SectionCourse WHERE course_id = " +
                                    "(SELECT course_id FROM Course WHERE course_name = @selectedcourse) " +
                                    "AND section_id = (SELECT section_id FROM Section WHERE sec_name = @section))";

                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@LD_id", LD_id);
                checkCommand.Parameters.AddWithValue("@selectedcourse", selectedcourse);
                checkCommand.Parameters.AddWithValue("@section", section);

                int existingAllocations = (int)checkCommand.ExecuteScalar();

                if (existingAllocations > 0)
                {
                    MessageBox.Show("Allocation already exists!");
                }
                else
                {
                    string query = "INSERT INTO AllocationLD " +
                                   "VALUES (@date, @LD_id, @user_id, " +
                                   "(SELECT SC_id FROM SectionCourse " +
                                   "WHERE course_id = (SELECT course_id FROM Course WHERE course_name = @selectedcourse) " +
                                   "AND section_id = (SELECT section_id FROM Section WHERE sec_name = @section)))";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@selectedcourse", selectedcourse);
                    command.Parameters.AddWithValue("@section", section);
                    command.Parameters.AddWithValue("@LD_id", LD_id);
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@date", date);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Allocation done!");
                    }
                    else
                    {
                        MessageBox.Show("Invalid Entry");
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_portal admin_portal = new admin_portal(user_id);
            admin_portal.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            newallocation(user_id);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string selectedcourse = comboBox1.SelectedItem.ToString();
            showdatgrid(user_id);
            showdatgrid2(user_id);
        }
    }
}
