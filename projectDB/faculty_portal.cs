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
    public partial class faculty_portal : Form
    {
        private int user_id;
        public faculty_portal(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
        }


        private void faculty_portal_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT first_name FROM Faculty WHERE user_id = @user_id";
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

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Editprofile_faculty Editprofile_faculty = new Editprofile_faculty(user_id);
            Editprofile_faculty.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }


        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Hide();
            AssigntaskTA AssigntaskTA = new AssigntaskTA(user_id);
            AssigntaskTA.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            AssignTaskLD AssignTaskLD = new AssignTaskLD(user_id);
            AssignTaskLD.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            GiveFeedbackTA GiveFeedbackTA = new GiveFeedbackTA(user_id);
            GiveFeedbackTA.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            GiveFeedbackLD GiveFeedbackLD = new GiveFeedbackLD(user_id);
            GiveFeedbackLD.Show();
        }

    }
}

