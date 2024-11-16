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
    public partial class Views : Form
    {
        private int user_id;
        public Views(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
            showdatgrid(user_id);
            showdatgrid2(user_id);
            showdatgrid3(user_id);
            showdatgrid4(user_id);

        }

        private void Views_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void showdatgrid(int user_id)
        {

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                "Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                                        SELECT a.TA_id, max_allocation, max_courses
                                        FROM (
                                            SELECT TA_id, MAX(alloc_count) AS max_allocation
                                            FROM (
                                                SELECT TA_id, COUNT(*) AS alloc_count
                                                FROM AllocationTA
                                                GROUP BY TA_id
                                            ) AS TA_alloc_counts
                                            GROUP BY TA_id
                                        ) AS a
                                        JOIN (
                                            SELECT TA_id, COUNT(DISTINCT SC_id) AS max_courses
                                            FROM AllocationTA
                                            GROUP BY TA_id
                                        ) AS b
                                        ON a.TA_id = b.TA_id
                                        ORDER BY max_allocation DESC;
                                        ";


                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@user_id", user_id);


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
                string query = @"
                                SELECT LD_id, SUM(review_count) AS total_reviews
                                FROM (
                                    SELECT LD_id, COUNT(*) AS review_count
                                    FROM Feedback_LD
                                    GROUP BY LD_id
                                ) AS LD_feedback_counts
                                GROUP BY LD_id;
                            ";



                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@user_id", user_id);


                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;

            }
        }
        private void showdatgrid3(int user_id)
        {

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                "Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                                  SELECT TA_id, num_feedback
                                    FROM (
                                        SELECT TA_id, COUNT(*) AS num_feedback
                                        FROM Feedback_TA
                                        GROUP BY TA_id
                                    ) AS TA_feedback_counts
                                    WHERE num_feedback = (
                                        SELECT MAX(feedback_count)
                                        FROM (
                                            SELECT COUNT(*) AS feedback_count
                                            FROM Feedback_TA
                                            GROUP BY TA_id
                                        ) AS max_feedback_counts
                                    );";


                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@user_id", user_id);


                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView3.DataSource = dataTable;

            }
        }

        private void showdatgrid4(int user_id)
        {

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                "Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT TAalloc_id, num_completed_tasks
                                FROM (
                                    SELECT TAalloc_id, COUNT(*) AS num_completed_tasks
                                    FROM Tasks
                                    WHERE t_status = 'Complete'
                                    GROUP BY TAalloc_id
                                ) AS TA_completed_tasks
                                WHERE num_completed_tasks = (
                                    SELECT MAX(completed_task_count)
                                    FROM (
                                        SELECT TAalloc_id, COUNT(*) AS completed_task_count
                                        FROM Tasks
                                        WHERE t_status = 'Complete'
                                        GROUP BY TAalloc_id
                                    ) AS max_completed_task_counts
                                );";


                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@user_id", user_id);


                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView4.DataSource = dataTable;

            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_portal admin_portal = new admin_portal(user_id);
            admin_portal.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
