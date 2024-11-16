using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectDB
{
   
    public partial class admin_portal : Form
    {
        private int user_id;
        public admin_portal(int user_id)
        {
            InitializeComponent();
            this.user_id=user_id;
           
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Editprofile_admin Editprofile_admin = new Editprofile_admin(user_id);
            Editprofile_admin.Show();
        }

        private void admin_portal_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT first_name FROM Admin WHERE user_id = @user_id";
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


        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            updateallocations updateallocations = new updateallocations(user_id);
            updateallocations.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            manageTA manageTA = new manageTA(user_id);
            manageTA.Show();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            manageLD manageLD = new manageLD(user_id);
            manageLD.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Views Views = new Views(user_id);
            Views.Show();
        }
    }
}
