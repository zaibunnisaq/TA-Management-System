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
    public partial class AssignTaskLD : Form
    {
        private int user_id;

        public AssignTaskLD(int user_id)
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

        private void AssignTaskLD_Load(object sender, EventArgs e)
        {

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
                string query = "select Course.course_name as course, AllocationLD.LDalloc_id as [Allocation LD ID], LD.first_name as [First Name], LD.last_name as [Last Name], Section.sec_name as Section , LD.email as [Email] " +
                "from Course " +
                "join SectionCourse on SectionCourse.course_id = Course.course_id " +
                "join Section on Section.section_id = SectionCourse.section_id " +
                "join AllocationLD on AllocationLD.SC_id = SectionCourse.SC_id " +
                "join LD on LD.LD_id = AllocationLD.LD_id " +
                "join FacultyAllocationLD on FacultyAllocationLD.LDalloc_id = AllocationLD.LDalloc_id " +
                "where FacultyAllocationLD.faculty_id in (select faculty_id " +
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
            string lt_name = comboBox1.SelectedItem.ToString();
            string LDalloc_id = textBox1.Text;
            string lt_descp = textBox2.Text;
            string lt_deadline = maskedTextBox2.Text;

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO LabTasks (lt_name, lt_status, lt_descp, lt_deadline, LDalloc_id) " +
                               "VALUES (@lt_name, 'Incomplete', @lt_descp, @lt_deadline, @LDalloc_id)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@lt_name", lt_name);
                command.Parameters.AddWithValue("@lt_descp", lt_descp);
                command.Parameters.AddWithValue("@lt_deadline", lt_deadline);
                command.Parameters.AddWithValue("@LDalloc_id", LDalloc_id);

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
                string query = "SELECT LabTasks.ltask_id as 'TaskID', LabTasks.lt_name as Task, LabTasks.lt_status as Status, LD.LD_id, LD.first_name " +
               "FROM LabTasks " +
               "JOIN AllocationLD ON LabTasks.LDalloc_id = AllocationLD.LDalloc_id " +
               "JOIN LD ON LD.LD_id = AllocationLD.LD_id " +
               "JOIN FacultyAllocationLD ON FacultyAllocationLD.LDalloc_id = AllocationLD.LDalloc_id " +
               "WHERE FacultyAllocationLD.faculty_id IN (SELECT faculty_id FROM Faculty WHERE user_id = @user_id)";
                //SUBQUERY





                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@user_id", user_id);


                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showdatgrid(user_id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            assignTask(user_id);
            showdatgrid2(user_id);
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                int taskID = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["TaskID"].Value);

                deleteTask(taskID);
                showdatgrid2(user_id); // Refresh the grid after deleting the task
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

                    // Modify the query to delete the entire row based on task_id
                    string updateQuery = "DELETE FROM LabTasks WHERE ltask_id = @TaskID";

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
