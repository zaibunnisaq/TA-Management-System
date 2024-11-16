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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace projectDB
{
  
    public partial class updateallocations : Form
    {
        private int user_id;
        public updateallocations(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(deleteButtonColumn);
        }

        private void deleteTask(int AllocationID)
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                                          "Database=TA Management system;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = "DELETE FROM AllocationTA WHERE TAalloc_id = @AllocationID";

                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@AllocationID", AllocationID);
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Allocation deleted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the Allocation.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Allocation: {ex.Message}");
            }
        }

        private void updateallocations_Load(object sender, EventArgs e)
        {

        }

        //private void showdatgrid2(int user_id)
        //{
        //    string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
        //                              "Database=TA Management system;Integrated Security=True;";

        //    string selectedcourse = comboBox1.SelectedItem.ToString();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        string query = "select s.sec_name " +
        //    "from Section s " +
        //    "join SectionCourse sc on sc.section_id = s.section_id " +
        //    "join Course c on c.course_id = sc.course_id " +
        //    "where c.course_name = @selectedcourse";


        //        SqlCommand commandgrid = new SqlCommand(query, connection);
        //        commandgrid.Parameters.AddWithValue("@selectedcourse", selectedcourse);


        //        SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
        //        DataTable dataTable = new DataTable();
        //        adapter.Fill(dataTable);
        //        dataGridView2.DataSource = dataTable;
        //    }
        //}

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void showdatgrid(int user_id)
        {

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
            "Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT TAalloc_id as AllocationID, TA.TA_id, TA.first_name as firstname, TA.last_name as lastname, " +
                "Section.sec_name as Section, Course.course_name as Course " +
                "FROM AllocationTA " +
                "JOIN TA ON TA.TA_id = AllocationTA.TA_id " +
                "JOIN SectionCourse ON SectionCourse.SC_id = AllocationTA.SC_id " +
                "JOIN Section ON Section.section_id = SectionCourse.section_id " +
                "JOIN Course ON Course.course_id = SectionCourse.course_id;";



                SqlCommand commandgrid = new SqlCommand(query, connection);
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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT LDalloc_id as AllocationID, LD.LD_id, LD.first_name as firstname, LD.last_name as lastname, " +
                "Section.sec_name as Section, Course.course_name as Course " +
                "FROM AllocationLD " +
                "JOIN LD ON LD.LD_id = AllocationLD.LD_id " +
                "JOIN SectionCourse ON SectionCourse.SC_id = AllocationLD.SC_id " +
                "JOIN Section ON Section.section_id = SectionCourse.section_id " +
                "JOIN Course ON Course.course_id = SectionCourse.course_id;";



                SqlCommand commandgrid = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;

            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                int AllocationID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["AllocationID"].Value);

                deleteTask(AllocationID);
                showdatgrid(user_id);
            }
        }

        private void updateallocationTA(int user_id)
        {
            
            string allocationid = textBox2.Text;
            string TA_id = textBox1.Text;

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "UPDATE AllocationTA set TA_id=@TA_id where TAalloc_id=@allocationid;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@allocationid", allocationid);
            command.Parameters.AddWithValue("@TA_id", TA_id);
            int rowsaffected = command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (rowsaffected > 0)
                {
                    MessageBox.Show("Updated !");
                }
                else
                {
                    MessageBox.Show("Invalid Entry");
                }
            }

        }

        private void updateallocationLD(int user_id)
        {

            string allocationid = textBox3.Text;
            string LD_id = textBox4.Text;

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                "Database=TA Management system;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "UPDATE AllocationLD set LD_id=@LD_id where LDalloc_id=@allocationid;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@allocationid", allocationid);
            command.Parameters.AddWithValue("@LD_id", LD_id);
            int rowsaffected = command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (rowsaffected > 0)
                {
                    MessageBox.Show("Updated !");
                }
                else
                {
                    MessageBox.Show("Invalid Entry");
                }
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            showdatgrid(user_id);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            showdatgrid2(user_id);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            updateallocationTA(user_id);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            updateallocationLD(user_id);
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_portal admin_portal = new admin_portal(user_id);
            admin_portal.Show();
        }
    }
}
