using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace projectDB
{
    public partial class Form1 : Form
    {
/*        private int user_id;
*/        public Form1()
        {
            InitializeComponent();
            /*TA_courses TA_courses = new TA_courses(user_id);
            TA_courses.Show();*/
           
        }


        public void checklogin()
        {
            string uname = textBox1.Text;
            string pass = textBox2.Text;

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "Select user_id from Users where username = '" + uname + "' AND password = '" + pass + "'";
                SqlCommand command = new SqlCommand(query, connection);
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    int user_id = Convert.ToInt32(result);

                    string adminQuery = "SELECT * FROM Admin WHERE user_id = @user_id";
                    SqlCommand adminCommand = new SqlCommand(adminQuery, connection);
                    adminCommand.Parameters.AddWithValue("@user_id", user_id);

                    using (SqlDataReader reader = adminCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MessageBox.Show("Login Successful as Admin");
                            this.Hide();
                            admin_portal admin_portal = new admin_portal(user_id);
                            admin_portal.Show();

                            
                        }
                    }

                    string facultyQuery = "SELECT * FROM Faculty WHERE user_id = @user_id";
                    SqlCommand facultyCommand = new SqlCommand(facultyQuery, connection);
                    facultyCommand.Parameters.AddWithValue("@user_id", user_id);

                    using (SqlDataReader readerfaculty = facultyCommand.ExecuteReader())
                    {
                        if (readerfaculty.Read())
                        {
                            MessageBox.Show("Login Successful as Faculty");
                            this.Hide();
                            faculty_portal faculty_portal = new faculty_portal(user_id);
                            faculty_portal.Show();
                        }
                    }

                    string TAQuery = "SELECT * FROM TA WHERE user_id = @user_id";
                    SqlCommand TACommand = new SqlCommand(TAQuery, connection);
                    TACommand.Parameters.AddWithValue("@user_id", user_id);

                    using (SqlDataReader readerTA = TACommand.ExecuteReader())
                    {
                        if (readerTA.Read())
                        {
                            MessageBox.Show("Login Successful as TA");
                            this.Hide();
                            TA_portal TA_portal = new TA_portal(user_id);
                            TA_portal.Show();
                            return;

                        }
                    }

                    string LDQuery = "SELECT * FROM LD WHERE user_id = @user_id";
                    SqlCommand LDCommand = new SqlCommand(LDQuery, connection);
                    LDCommand.Parameters.AddWithValue("@user_id", user_id);

                    using (SqlDataReader readerld = LDCommand.ExecuteReader())
                    {
                        if (readerld.Read())
                        {
                            MessageBox.Show("Login Successful as LD");
                            this.Hide();
                            LD_portal LD_portal = new LD_portal(user_id);
                            LD_portal.Show();
                            return;
                        }
                    }

                //    using (SqlDataReader reader5 = LDCommand.ExecuteReader())
                //    using (SqlDataReader reader6 = TACommand.ExecuteReader())
                //    {
                //        List<string> LD = new List<string>();
                //        List<string> TA = new List<string>();
                //        while (reader5.Read())
                //        {
                //            LD.Add(reader5.GetString(0));
                //        }
                //        while (reader6.Read())
                //        {
                //            TA.Add(reader6.GetString(0));
                //        }

                //        if (LD.SequenceEqual(TA))
                //        {
                //            choice choice = new choice(user_id);
                //            choice.Show();
                //        }
                //        else
                //        {
                //            reader5.Close();
                //            reader6.Close();
                //            if (LD.Count > 0)
                //            {
                //                MessageBox.Show("Login Successful as Faculty");
                //                LD_portal LD_portal = new LD_portal(user_id);
                //                LD_portal.Show();
                //            }

                //            if (TA.Count > 0)
                //            {
                //                MessageBox.Show("Login Successful as Faculty");
                //                TA_portal TA_portal = new TA_portal(user_id);
                //                TA_portal.Show();
                //            }
                //        }
                //    }
                }
                else
                {
                    MessageBox.Show("User does not exist");
                }
            }
        }





        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) ///////////USERAME TEXTBOX
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)///////////////////PASSWORD TEXTBOX

        {

        }

        private void button1_Click(object sender, EventArgs e)////////////////LOGIN BUTTON

        {
            checklogin();
            //Form2 form2 = new Form2();
            //form2.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) ////////////REGISTERATION LINK 

        {
            this.Hide();
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
