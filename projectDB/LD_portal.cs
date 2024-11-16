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
    public partial class LD_portal : Form
    {
        private int user_id;
        public LD_portal(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
        }

        private void LD_portal_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT first_name FROM LD WHERE user_id = @user_id";
                SqlCommand commandreadfname = new SqlCommand(query, connection);
                commandreadfname.Parameters.AddWithValue("@user_id", user_id);

                using (SqlDataReader readerfname = commandreadfname.ExecuteReader())
                {
                    if (readerfname.Read())
                    {
                        label1.Text = readerfname.GetString(0);

                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }


        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Editprofile_LD Editprofile_LD = new Editprofile_LD(user_id);
            Editprofile_LD.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            viewyourCoursesLD viewyourCoursesLD = new viewyourCoursesLD(user_id);
            viewyourCoursesLD.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateTaskLD updateTaskLD = new UpdateTaskLD(user_id);
            updateTaskLD.Show();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            viewfeedbackLD viewfeedbackLD = new viewfeedbackLD(user_id);    
            viewfeedbackLD.Show();
        }
    }
}
