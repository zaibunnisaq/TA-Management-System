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
    public partial class faculty_regis : Form
    {
        public faculty_regis()
        {
            InitializeComponent();
        }


        public void regsiteruser()
        {
            try
            {
                string username = textBox1.Text;
                string password = textBox5.Text;
                string firstname = textBox2.Text;
                string lastname = textBox3.Text;
                string email = textBox4.Text;
                string phone = maskedTextBox2.Text;
                string dob = maskedTextBox1.Text;
                string address = textBox6.Text;
                string experience = textBox7.Text;

                if (password.Length == 8)
                {
                    string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string query = "INSERT INTO Users  (username,password) VALUES (@username, @password)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                int rowsaffected = command.ExecuteNonQuery();


                int count = 0;
                string queryc = "Select count(*) as c from users";
                SqlCommand commandc = new SqlCommand(queryc, connection);
                using (SqlDataReader readerc = commandc.ExecuteReader())
                {
                    if (readerc.HasRows)
                    {
                        while (readerc.Read())
                        {
                            // Access data using reader["columnName"]
                            count = readerc.GetInt32(readerc.GetOrdinal("c"));

                            // Do something with the retrieved data
                            Console.WriteLine($"User ID: {count}");
                        }
                    }

                }


                string queryAdmin = "INSERT INTO Faculty (first_name, last_name, contact_no, dob, addr, email, experience, user_id) VALUES (@first_name, @last_name, @contact_no, @dob, @addr, @email, @experience, @user_id)";
                SqlCommand commandAdmin = new SqlCommand(queryAdmin, connection);
                commandAdmin.Parameters.AddWithValue("@first_name", firstname);
                commandAdmin.Parameters.AddWithValue("@last_name", lastname);
                commandAdmin.Parameters.AddWithValue("@contact_no", phone);
                commandAdmin.Parameters.AddWithValue("@dob", dob);
                commandAdmin.Parameters.AddWithValue("@addr", address);
                commandAdmin.Parameters.AddWithValue("@email", email);
                commandAdmin.Parameters.AddWithValue("@experience", experience);
                commandAdmin.Parameters.AddWithValue("@user_id", count); // Assuming i is the user_id from Users table
                int rowsAffectedAdmin = commandAdmin.ExecuteNonQuery();


                if (rowsaffected > 0 && rowsAffectedAdmin > 0)
                {
                    MessageBox.Show("Regsitered Sucessfully");
                    string query22 = "Select * from Faculty where first_name = '" + firstname + "' AND last_name = '" + lastname + "' AND contact_no = '" + phone + "'";
                    SqlCommand command2 = new SqlCommand(query22, connection);

                    using (SqlDataReader reader2 = command2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            MessageBox.Show("LogIn Successful");
                            this.Hide();
                            faculty_portal faculty_portal = new faculty_portal(count);
                            faculty_portal.Show();
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Regsiteration failed");
                }
            }

                else
                {
                    MessageBox.Show("Password must be exactly 8 characters long.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        private void textBox1_TextChanged(object sender, EventArgs e)//////USERNAME TEXTBOX

        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)///FIRSTNAME
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)//////////////LASTNAME

        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)////EMAIL
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)//////////ADDRESS
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)////////////PASSWORD
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)////////dob
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)////////////CONTACT NO
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)////////////////EXPERIENCE 
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            regsiteruser();
        }

        private void faculty_regis_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
