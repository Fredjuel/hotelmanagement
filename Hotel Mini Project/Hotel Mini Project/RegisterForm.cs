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

namespace Hotel_Mini_Project
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=hoteldb;Integrated Security=True");
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm lf = new LoginForm();
            this.Hide();
            lf.Show();
            lf.WindowState = FormWindowState.Maximized;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select username from login where username ='" + textBox1.Text + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("This username already exists.Kindly try any other username", "Username already used");
                    con.Close();
                }
                else
                {
                    con.Close();
                    SqlCommand cmd1 = new SqlCommand("insert into login values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "'," + textBox6.Text + ")", con);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd1;
                    DataSet ds = new DataSet();
                    da.Fill(ds, "login");
                    MessageBox.Show("Account created successfully....", "Registration");
                    LoginForm lf = new LoginForm();
                    this.Hide();
                    lf.Show();
                    lf.WindowState = FormWindowState.Maximized;
                }
                
            }
            else
            {
                MessageBox.Show("Password does not match..Try again","Password Mismatch");
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

        }
        int count = 0;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            count++;
            if(count % 2 != 0)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;

            }

        }
        int count1 = 0;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            count1++;
            if (count1 % 2 != 0)
            {
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;

            }

        }
    }
}
