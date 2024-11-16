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
    public partial class viewyourCourseTA : Form
    {
        private int user_id;
        public viewyourCourseTA(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
            showdatgrid1(user_id);
        }
        private void showdatgrid1(int user_id)
        {
            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                                      "Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT c.course_name, c.credit_hrs " +
               "FROM Course c " +
               "JOIN Courses_TA cta ON c.course_id = cta.Course_id " +
               "JOIN TA ON TA.TA_id = cta.TA_id AND TA.user_id = @user_id;";


                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@user_id", user_id);
                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TA_portal TA_portal = new TA_portal(user_id);
            TA_portal.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void viewyourCourseTA_Load(object sender, EventArgs e)
        {

        }
    }
}
