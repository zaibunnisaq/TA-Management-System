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
    public partial class GiveFeedbackTA : Form
    {
        private int user_id;
        public GiveFeedbackTA(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
        }

        private void Givefeedback(int user_id)
        {
            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";
            string firstname = textBox1.Text;
            string lastname = textBox2.Text;
            string feeback = textBox3.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Feedback_TA (TAf_descp, TA_id, faculty_id) " +
                               "VALUES (@feeback, (SELECT TA_id FROM TA WHERE first_name = @firstname AND last_name = @lastname), (SELECT faculty_id FROM Faculty WHERE user_id = @user_id));";
                SqlCommand commandreadfname = new SqlCommand(query, connection);
                commandreadfname.Parameters.AddWithValue("@user_id", user_id);
                commandreadfname.Parameters.AddWithValue("@firstname", firstname);
                commandreadfname.Parameters.AddWithValue("@lastname", lastname);
                commandreadfname.Parameters.AddWithValue("@feeback", feeback);

                int rowsAffected = commandreadfname.ExecuteNonQuery();
                //using (SqlDataReader reader = commandreadfname.ExecuteReader())
                //{
                   

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Feedback Added!");
                    }
                    else
                    {
                        MessageBox.Show("Feeback is not added. TA does not exist.");
                    }
               // }
            }
        }

        private void GiveFeedbackTA_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
 
       
        private void button2_Click(object sender, EventArgs e)
        {
            Givefeedback(user_id);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

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
                    "FROM TA";

                SqlCommand commandgrid = new SqlCommand(query, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            showdatgrid();
        }

        private void label1_Click(object sender, EventArgs e)
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
