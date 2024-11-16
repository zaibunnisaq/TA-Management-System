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
    public partial class viewyourCoursesLD : Form
    {
        private int user_id;
        public viewyourCoursesLD(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
            showdatgrid1(user_id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LD_portal LD_portal = new LD_portal(user_id);
            LD_portal.Show();

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
               "JOIN Courses_LD cld ON c.course_id = cld.Course_id " +
               "JOIN LD ON LD.LD_id = cld.LD_id " +
               "WHERE LD.user_id = @user_id;";

                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@user_id", user_id);
                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void viewyourCoursesLD_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
