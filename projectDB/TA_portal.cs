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
    public partial class TA_portal : Form
    {
        private int user_id;
        public TA_portal(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
        }

        private void TA_portal_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT first_name FROM TA WHERE user_id = @user_id";
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


        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Editprofile_TA Editprofile_TA = new Editprofile_TA(user_id);
            Editprofile_TA.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            viewyourCourseTA viewyourCourseTA = new viewyourCourseTA(user_id);  
            viewyourCourseTA.Show();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            viewfeedbackTA viewfeedbackTA = new viewfeedbackTA(user_id);
            viewfeedbackTA.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            updateTaskTA updateTaskTA = new updateTaskTA(user_id);
            updateTaskTA.Show();
        }
    }
}
