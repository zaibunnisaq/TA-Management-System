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
    public partial class viewfeedbackTA : Form
    {
        private int user_id;
        public viewfeedbackTA(int user_id)
        {
            InitializeComponent();
            this.user_id= user_id;
            showdatgrid1(user_id);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void showdatgrid1(int user_id)
        {
            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                                      "Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
               
                string query = "SELECT f.first_name, f.last_name, Feedback_TA.TAf_descp " +
               "FROM Feedback_TA " +
               "JOIN Faculty f ON f.faculty_id = Feedback_TA.faculty_id " +
               "JOIN TA ON TA.TA_id = Feedback_TA.TA_id " +
               "JOIN Users ON Users.user_id = TA.user_id " +
               "WHERE Users.user_id = @user_id;";



                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@user_id", user_id);
                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void viewfeedbackTA_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TA_portal TA_portal = new TA_portal(user_id);
            TA_portal.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
