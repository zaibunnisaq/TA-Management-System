using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectDB
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) /////TA/LD REGISTERATION BUTTON
        {
            this.Hide();
            ta_ld_regis ta_ld_regis = new ta_ld_regis();
            ta_ld_regis.Show();
        }

        private void button1_Click(object sender, EventArgs e)/////////////ADMIN REGISTERATION BUTTON
        {
            this.Hide();
            admin_regis admin_regis = new admin_regis();
            admin_regis.Show();
        }

        private void button3_Click(object sender, EventArgs e) /////////////faculty regsiteration button
        {
            this.Hide();
            faculty_regis faculty_regis = new faculty_regis();
            faculty_regis.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
