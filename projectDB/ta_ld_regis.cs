using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectDB
{
    public partial class ta_ld_regis : Form
    {

        public ta_ld_regis()
        {
            InitializeComponent();
            populate_comboBox();
        }

        public void regsiterLD()
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
                string batch = textBox7.Text;
                string GPA = textBox8.Text;

                string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";


                if (password.Length == 8)
                {
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


                string queryLD = "INSERT INTO LD (first_name, last_name, contact_no, dob, addr, email,GPA, batch_id,user_id) VALUES (@first_name, @last_name, @contact_no, @dob, @addr, @email,@GPA, @batch_id,@user_id)";
                SqlCommand commandLD = new SqlCommand(queryLD, connection);
                commandLD.Parameters.AddWithValue("@first_name", firstname);
                commandLD.Parameters.AddWithValue("@last_name", lastname);
                commandLD.Parameters.AddWithValue("@contact_no", phone);
                commandLD.Parameters.AddWithValue("@dob", dob);
                commandLD.Parameters.AddWithValue("@addr", address);
                commandLD.Parameters.AddWithValue("@email", email);
                commandLD.Parameters.AddWithValue("@GPA", GPA);
                commandLD.Parameters.AddWithValue("@batch_id", batch);
                commandLD.Parameters.AddWithValue("@user_id", count); // Assuming i is the user_id from Users table
                int rowsAffectedAdmin = commandLD.ExecuteNonQuery();


                if (rowsaffected > 0 && rowsAffectedAdmin > 0)
                {
                    MessageBox.Show("Regsitered Sucessfully");
                    string query22 = "Select * from LD where first_name = '" + firstname + "' AND last_name = '" + lastname + "' AND contact_no = '" + phone + "'";
                    SqlCommand command2 = new SqlCommand(query22, connection);

                    using (SqlDataReader reader2 = command2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            MessageBox.Show("LogIn Successful");
                            this.Hide();
                            LD_courses LD_Courses = new LD_courses(count);
                            LD_Courses.Show();
                        }
                        // Close the reader when done
                        reader2.Close();
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

        public void regsiterTALD()
        {
            string username = textBox1.Text;
            string password = textBox5.Text;
            string firstname = textBox2.Text;
            string lastname = textBox3.Text;
            string email = textBox4.Text;
            string phone = maskedTextBox2.Text;
            string dob = maskedTextBox1.Text;
            string address = textBox6.Text;
            string batch = textBox7.Text;
            string GPA = textBox8.Text;

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

            //int user_id = count;
            string queryTA = "INSERT INTO TA (first_name, last_name, contact_no, dob, addr, email,GPA,  batch_id,user_id) VALUES (@first_name, @last_name, @contact_no, @dob, @addr, @email,@GPA,@batch_id,@user_id)";
            SqlCommand commandTA = new SqlCommand(queryTA, connection);
            commandTA.Parameters.AddWithValue("@first_name", firstname);
            commandTA.Parameters.AddWithValue("@last_name", lastname);
            commandTA.Parameters.AddWithValue("@contact_no", phone);
            commandTA.Parameters.AddWithValue("@dob", dob);
            commandTA.Parameters.AddWithValue("@addr", address);
            commandTA.Parameters.AddWithValue("@email", email);
            commandTA.Parameters.AddWithValue("@GPA", GPA);
            commandTA.Parameters.AddWithValue("@batch_id", batch);
            commandTA.Parameters.AddWithValue("@user_id", count); // Assuming i is the user_id from Users table
            int rowsAffectedTA = commandTA.ExecuteNonQuery();


            string queryLD = "INSERT INTO LD (first_name, last_name, contact_no, dob, addr, email,GPA, batch_id,user_id) VALUES (@first_name, @last_name, @contact_no, @dob, @addr, @email,@GPA, @batch_id,@user_id)";
            SqlCommand commandLD = new SqlCommand(queryLD, connection);
            commandLD.Parameters.AddWithValue("@first_name", firstname);
            commandLD.Parameters.AddWithValue("@last_name", lastname);
            commandLD.Parameters.AddWithValue("@contact_no", phone);
            commandLD.Parameters.AddWithValue("@dob", dob);
            commandLD.Parameters.AddWithValue("@addr", address);
            commandLD.Parameters.AddWithValue("@email", email);
            commandLD.Parameters.AddWithValue("@GPA", GPA);
            commandLD.Parameters.AddWithValue("@batch_id", batch);
            commandLD.Parameters.AddWithValue("@user_id", count); // Assuming i is the user_id from Users table
            int rowsAffectedLD = commandLD.ExecuteNonQuery();



            if (rowsaffected > 0 && rowsAffectedTA > 0 && rowsAffectedLD > 0)
            {
                string query21 = "Select * from TA where first_name = '" + firstname + "' AND last_name = '" + lastname + "' AND contact_no = '" + phone + "'";
                SqlCommand command21 = new SqlCommand(query21, connection);



                string query22 = "Select * from LD where first_name = '" + firstname + "' AND last_name = '" + lastname + "' AND contact_no = '" + phone + "'";
                SqlCommand command2 = new SqlCommand(query22, connection);

                SqlDataReader reader22 = command2.ExecuteReader();
                //SqlDataReader reader21 = command21.ExecuteReader();

                if (reader22.Read())
                {
                    MessageBox.Show("Regsitered Sucessfully");
                    // Login to a new form to choose options for data and LD portal
                    //this.Hide();
                    //choice choice = new choice(count);
                    //choice.Show();
                }

                reader22.Close();
               // reader21.Close();

            }


            else
            {
                MessageBox.Show("Regsiteration failed");
            }
        }


     // MessageBox.Show("An error occurred: " + ex.Message);
           

        

        public void regsiterTA()
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
                string batch = textBox7.Text;
                string GPA = textBox8.Text;

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


                string queryTA = "INSERT INTO TA (first_name, last_name, contact_no, dob, addr, email,GPA,  batch_id,user_id) VALUES (@first_name, @last_name, @contact_no, @dob, @addr, @email,@GPA,@batch_id,@user_id)";
                SqlCommand commandTA = new SqlCommand(queryTA, connection);
                commandTA.Parameters.AddWithValue("@first_name", firstname);
                commandTA.Parameters.AddWithValue("@last_name", lastname);
                commandTA.Parameters.AddWithValue("@contact_no", phone);
                commandTA.Parameters.AddWithValue("@dob", dob);
                commandTA.Parameters.AddWithValue("@addr", address);
                commandTA.Parameters.AddWithValue("@email", email);
                commandTA.Parameters.AddWithValue("@GPA", GPA);
                commandTA.Parameters.AddWithValue("@batch_id", batch);
                commandTA.Parameters.AddWithValue("@user_id", count); // Assuming i is the user_id from Users table
                int rowsAffectedTA = commandTA.ExecuteNonQuery();


                if (rowsaffected > 0 && rowsAffectedTA > 0)
                {
                    MessageBox.Show("Regsitered Sucessfully");
                    string query22 = "Select * from TA where first_name = '" + firstname + "' AND last_name = '" + lastname + "' AND contact_no = '" + phone + "'";
                    SqlCommand command2 = new SqlCommand(query22, connection);

                    using (SqlDataReader reader2 = command2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            MessageBox.Show("LogIn Successful");
                            this.Hide();
                            TA_courses TA_Courses = new TA_courses(count);
                            TA_Courses.Show();
                        }
                        reader2.Close();
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



        private void ta_ld_regis_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)/////username
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)////////////firstname
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)///////////lastname
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)//////////email
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)///////////address
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)////////////password
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)//////dob
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)///////contactno
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)/////////////experience
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        private void populate_comboBox()
        {
            comboBox1.Items.Add("TA");
            comboBox1.Items.Add("LD");
            //comboBox1.Items.Add("Both");
            comboBox1.SelectedIndex = 0;
        }
     

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string selectedRole = comboBox1.SelectedItem.ToString();

            
            if (selectedRole == "TA")
            {
                
                regsiterTA();
      
            }
            else if (selectedRole == "LD")
            {
                regsiterLD();
                
            }
            else if (selectedRole == "Both")
            {
                regsiterTALD();


            }
            else
            {
                // Handle an unexpected selection (optional)
                MessageBox.Show("Please select a valid role for registration.");
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
