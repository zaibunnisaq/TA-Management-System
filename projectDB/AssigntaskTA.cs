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
using System.Xml.Linq;

namespace projectDB
{
    public partial class AssigntaskTA : Form
    {
        private int user_id;
        public AssigntaskTA(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(deleteButtonColumn);

            showdatgrid(user_id);
            showdatgrid2(user_id);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            faculty_portal faculty_portal = new faculty_portal(user_id);
            faculty_portal.Show();
        }
        private void showdatgrid(int user_id)
        {

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                "Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select Course.course_name as course, AllocationTA.TAalloc_id as [Allocation TA ID], TA.first_name as [First Name], TA.last_name as [Last Name],  Section.sec_name as Section, TA.email as [Email] " +
                "from Course " +
                "join SectionCourse on SectionCourse.course_id = Course.course_id " +
                "join Section on Section.section_id = SectionCourse.section_id " +
                "join AllocationTA on AllocationTA.SC_id = SectionCourse.SC_id " +
                "join TA on TA.TA_id = AllocationTA.TA_id " +
                "join FacultyAllocationTA on FacultyAllocationTA.alloc_id = AllocationTA.TAalloc_id " +
                "where FacultyAllocationTA.faculty_id in (select faculty_id " +
                                                       "from Faculty " +
                                                       "where user_id = @user_id);";




                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@user_id", user_id);


                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

            }
        }

        private void assignTask(int user_id)
        {
            string task_name = comboBox1.SelectedItem.ToString();
            string TAalloc_id = textBox1.Text;
            string t_descp = textBox2.Text;
            string t_deadline = maskedTextBox2.Text;

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
               
                    string query = "INSERT INTO Tasks (t_name, t_status, t_descp, t_deadline, TAalloc_id) " +
                                   "VALUES (@task_name, 'Incomplete', @t_descp, @t_deadline, @TAalloc_id)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@task_name", task_name);
                    command.Parameters.AddWithValue("@t_descp", t_descp);
                    command.Parameters.AddWithValue("@t_deadline", t_deadline);
                    command.Parameters.AddWithValue("@TAalloc_id", TAalloc_id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Task assigned successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to assign the task");
                    }
                
            }
        }
        private void showdatgrid2(int user_id)
        {

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                "Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Tasks.task_id AS 'TaskID', Tasks.t_name as Task, Tasks.t_status as Status, TA.TA_id ,TA.first_name " +
                "FROM Tasks " +
                "JOIN AllocationTA ON Tasks.TAalloc_id = AllocationTA.TAalloc_id " +
                "JOIN TA ON TA.TA_id = AllocationTA.TA_id " +
                "JOIN FacultyAllocationTA ON FacultyAllocationTA.alloc_id = AllocationTA.TAalloc_id " +
                "WHERE faculty_id IN (SELECT faculty_id FROM Faculty WHERE user_id = @user_id);";





                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@user_id", user_id);


                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;

            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            showdatgrid(user_id);

        }

        private void viewAllocFaculty_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            assignTask(user_id);
            showdatgrid2(user_id);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                int taskID = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["TaskID"].Value);

                deleteTask(taskID);
                showdatgrid2(user_id);
            }
        }


        private void deleteTask(int taskID)
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                                          "Database=TA Management system;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = "DELETE FROM Tasks WHERE task_id = @TaskID";

                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@TaskID", taskID);
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Task deleted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the task.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting task: {ex.Message}");
            }
        }

    }
}
