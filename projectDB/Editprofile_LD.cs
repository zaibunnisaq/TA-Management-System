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
    public partial class Editprofile_LD : Form
    {
        private int user_id;

        public Editprofile_LD(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
        }

        private void Editprofile_LD_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                "Database=TA Management system;" +
                "Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT LD.first_name, LD.last_name, LD.contact_no, LD.addr, LD.email " +
               "FROM LD " +
               "JOIN Users ON LD.user_id = Users.user_id " +
               "WHERE LD.user_id = @user_id;";

                SqlCommand commandreadfname = new SqlCommand(query, connection);
                commandreadfname.Parameters.AddWithValue("@user_id", user_id);

                using (SqlDataReader readerfname = commandreadfname.ExecuteReader())
                {
                    if (readerfname.Read())
                    {
                        label2.Text = readerfname.GetString(0);
                        label7.Text = readerfname.GetString(1);
                        label10.Text = readerfname.GetString(2);
                        label13.Text = readerfname.GetString(3);
                        label16.Text = readerfname.GetString(4);
                    }
                }
            }
        }

        private void Updatefirstname(int user_id)
        {
            string newfirstname = textBox1.Text;
            if (string.IsNullOrWhiteSpace(newfirstname))
            {
                MessageBox.Show("First name cannot be empty. Please enter a valid value.");
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                    "Database=TA Management system;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update query
                    string query = "UPDATE LD SET first_name = @newfirstname WHERE user_id = @userId";
                    SqlCommand commandupdatefname = new SqlCommand(query, connection);
                    commandupdatefname.Parameters.AddWithValue("@newfirstname", newfirstname);
                    commandupdatefname.Parameters.AddWithValue("@userId", user_id);

                    int rowsAffected = commandupdatefname.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                    }
                    else
                    {
                        MessageBox.Show("No rows updated. User may not exist or data unchanged.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void Updatelastname(int user_id)
        {
            string newlastname = textBox2.Text;
            if (string.IsNullOrWhiteSpace(newlastname))
            {
                MessageBox.Show("Last name cannot be empty. Please enter a valid value.");
                return;
            }
            try
            {
                string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                    "Database=TA Management system;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update query
                    string query = "UPDATE LD SET last_name = @newlastname WHERE user_id = @userId";
                    SqlCommand commandupdatelname = new SqlCommand(query, connection);
                    commandupdatelname.Parameters.AddWithValue("@newlastname", newlastname);
                    commandupdatelname.Parameters.AddWithValue("@userId", user_id);

                    int rowsAffected = commandupdatelname.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                    }
                    else
                    {
                        MessageBox.Show("No rows updated. User may not exist or data unchanged.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void Updatecontact(int user_id)
        {
            string newcontact = maskedTextBox1.Text;
            if (string.IsNullOrWhiteSpace(newcontact))
            {
                MessageBox.Show("Contact cannot be empty. Please enter a valid value.");
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                    "Database=TA Management system;" +
                    "Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update query
                    string query = "UPDATE LD SET contact_no = @newcontact WHERE user_id = @userId";
                    SqlCommand commandupdatecontact = new SqlCommand(query, connection);
                    commandupdatecontact.Parameters.AddWithValue("@newcontact", newcontact);
                    commandupdatecontact.Parameters.AddWithValue("@userId", user_id);

                    int rowsAffected = commandupdatecontact.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                    }
                    else
                    {
                        MessageBox.Show("No rows updated. User may not exist or data unchanged.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Updateaddress(int user_id)
        {
            string newaddress = textBox4.Text;
            if (string.IsNullOrWhiteSpace(newaddress))
            {
                MessageBox.Show("Address cannot be empty. Please enter a valid value.");
                return;
            }
            try
            {
                string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                    "Database=TA Management system;" +
                    "Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update query
                    string query = "UPDATE LD SET addr = @newaddress WHERE user_id = @userId";
                    SqlCommand commandupdateaddress = new SqlCommand(query, connection);
                    commandupdateaddress.Parameters.AddWithValue("@newaddress", newaddress);
                    commandupdateaddress.Parameters.AddWithValue("@userId", user_id);

                    int rowsAffected = commandupdateaddress.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                    }
                    else
                    {
                        MessageBox.Show("No rows updated. User may not exist or data unchanged.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void Updatemail(int user_id)
        {
            string newemail = textBox5.Text;
            if (string.IsNullOrWhiteSpace(newemail))
            {
                MessageBox.Show("Email cannot be empty. Please enter a valid value.");
                return;
            }
            try
            {
                string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;Database=TA Management system;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update query
                    string query = "UPDATE LD SET email = @newemail WHERE user_id = @userId";
                    SqlCommand commandupdatelname = new SqlCommand(query, connection);
                    commandupdatelname.Parameters.AddWithValue("@newemail", newemail);
                    commandupdatelname.Parameters.AddWithValue("@userId", user_id);

                    int rowsAffected = commandupdatelname.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                    }
                    else
                    {
                        MessageBox.Show("No rows updated. User may not exist or data unchanged.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void showdatgrid(int user_id)
        {

            string connectionString = "Data Source=DESKTOP-TROH6LH\\SQLEXPRESS;" +
                "Database=TA Management system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT LD_id as LDid," +
                    "first_name as FirstName," +
                    "last_name as LastName," +
                    "contact_no," +
                    "dob as DOB, " +
                    "addr as Address," +
                    " email FROM LD WHERE user_id = @user_id";
                SqlCommand commandgrid = new SqlCommand(query, connection);
                commandgrid.Parameters.AddWithValue("@user_id", user_id);


                SqlDataAdapter adapter = new SqlDataAdapter(commandgrid);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Updatefirstname(user_id);
            showdatgrid(user_id);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Updatelastname(user_id);
            showdatgrid(user_id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Updatecontact(user_id);
            showdatgrid(user_id);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Updateaddress(user_id);
            showdatgrid(user_id);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Updatemail(user_id);
            showdatgrid(user_id);
        }


        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            LD_portal LD_portal = new LD_portal(user_id);
            LD_portal.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}