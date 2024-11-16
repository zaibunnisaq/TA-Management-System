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
    public partial class GiveFeedbackLD : Form
    {
        private int user_id;
        public GiveFeedbackLD(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
        }

        private void Givefeedback(int user_id)
        {
            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";
            string firstname = textBox1.Text;
            string lastname = textBox2.Text;
            string feedback = textBox3.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Feedback_LD (LDf_descp, LD_id, faculty_id) " +
                               "VALUES (@feedback, (SELECT LD_id FROM LD WHERE first_name = @firstname AND last_name = @lastname), " +
                               "(SELECT faculty_id FROM Faculty WHERE user_id = @user_id));";

                SqlCommand commandreadfname = new SqlCommand(query, connection);
                commandreadfname.Parameters.AddWithValue("@user_id", user_id);
                commandreadfname.Parameters.AddWithValue("@firstname", firstname);
                commandreadfname.Parameters.AddWithValue("@lastname", lastname);
                commandreadfname.Parameters.AddWithValue("@feedback", feedback); 

                int rowsAffected = commandreadfname.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Feedback Added!");
                }
                else
                {
                    MessageBox.Show("Feedback is not added. LD does not exist.");
                }
            }
        }


        private void showdatgrid()
        {
            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                "Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query =
                    "SELECT first_name as FirstName," +
                    "last_name as LastName " +
                    "FROM LD";

                SqlCommand commandgrid = new SqlCommand(query, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            showdatgrid();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Givefeedback(user_id);
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void GiveFeedbackLD_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            faculty_portal faculty_portal = new faculty_portal(user_id);
            faculty_portal.Show();
        }
    }
}
