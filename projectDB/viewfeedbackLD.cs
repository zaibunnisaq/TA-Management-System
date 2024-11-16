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
    public partial class viewfeedbackLD : Form
    {
        private int user_id;
        public viewfeedbackLD(int user_id)
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

                string query = "SELECT f.first_name, f.last_name, Feedback_LD.LDf_descp " +
               "FROM Feedback_LD " +
               "JOIN Faculty f ON f.faculty_id = Feedback_LD.faculty_id " +
               "JOIN LD ON LD.LD_id = Feedback_LD.LD_id " +
               "JOIN Users ON Users.user_id = LD.user_id " +
               "WHERE Users.user_id = @user_id;";



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
            LD_portal LD_portal = new LD_portal(user_id);
            LD_portal.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void viewfeedbackLD_Load(object sender, EventArgs e)
        {

        }
    }
}
