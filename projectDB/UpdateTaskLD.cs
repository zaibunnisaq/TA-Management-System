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
    public partial class UpdateTaskLD : Form
    {
        private int user_id;
        public UpdateTaskLD(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
            showdatgrid1(user_id);
        }

        private void UpdateTaskLD_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            LD_portal LD_portal = new LD_portal(user_id);
            LD_portal.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["CheckboxColumn"].Index && e.RowIndex >= 0)
            {
                DataGridViewCheckBoxCell checkBoxCell = dataGridView1[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;

                if (checkBoxCell != null && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                {
                    int taskID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["TaskID"].Value);

                    UpdateTaskStatus(taskID);
                }
            }
        }
        private void UpdateTaskStatus(int taskID)
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                                          "Database=TA Management system;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = "UPDATE LabTasks SET lt_status = 'Complete' WHERE ltask_id = @TaskID";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@TaskID", taskID);
                    updateCommand.ExecuteNonQuery();
                }

                // MessageBox.Show("Task status updated to Complete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating task status: {ex.Message}");
            }
        }
        private void showdatgrid1(int user_id)
        {
            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                                      "Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query =
                    @"SELECT t.ltask_id AS 'TaskID',t.lt_status AS 'Status', t.lt_name AS 'Task Name', t.lt_descp AS 'Description', t.lt_deadline AS 'Deadline' , s.sec_name AS 'Section', c.course_name AS 'Courses'
                    FROM LabTasks t
                    JOIN AllocationLD ata ON t.LDalloc_id = ata.LDalloc_id
                    JOIN LD ta ON ta.LD_id = ata.LD_id
                    JOIN SectionCourse sc ON sc.SC_id = ata.SC_id
                    JOIN Section s ON s.section_id = sc.section_id
                    JOIN Course c ON c.course_id = sc.course_id
                    WHERE ta.user_id = @user_id;";

                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@user_id", user_id);
                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["CheckboxColumn"].Value != null && (bool)row.Cells["CheckboxColumn"].Value)
                {
                    int taskID = Convert.ToInt32(row.Cells["TaskID"].Value);
                    UpdateTaskStatus(taskID);
                }
            }

            showdatgrid1(user_id);

            MessageBox.Show("All selected tasks updated to Complete.");
        }
    }
}
