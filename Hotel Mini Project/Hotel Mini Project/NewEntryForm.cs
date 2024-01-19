using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Mini_Project
{
    public partial class NewEntryForm : Form
    {
        public NewEntryForm()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=hoteldb;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(textBox1.Text == string.Empty)
            {
                MessageBox.Show("Kindly enter the food name...", "Missing Input");
                return;
            }
            else if(textBox2.Text == string.Empty)
            {
                MessageBox.Show("Kindly enter the price of the food...", "Missing Input");
                return;
            }
            else if(comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Kindly select Food Availability...", "Missing Input");
                return;
            }
            else if (radioButton1.Checked is false && radioButton2.Checked is false)
            {
                MessageBox.Show("Kindly select the food type...", "Missing Input");
                return;
            }
            else
            {
                if (radioButton1.Checked)
                {
                    label6.Text = "V";
                }
                if (radioButton2.Checked)
                {
                    label6.Text = "N";
                }
            }
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into tblFood values('" + textBox1.Text + "', '" + label6.Text + "'," + textBox2.Text + ",'" + comboBox1.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                textBox1.Clear();
                textBox2.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                comboBox1.Text = "";
                view();
            }
            
            catch(System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Kindly enter amount in numbers....","Wrong input");
            }

        }

        private void NewEntryForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if(label7.Text == "label7")
            {
                MessageBox.Show("Kindly select the food item from the table");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("delete from tblfood where fid = " + label7.Text, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "hotel");
                view();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var item = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            label7.Text = item.ToString();
        }
        
       

        private void button5_Click(object sender, EventArgs e)
        {
            view();
        }
        public void view()
        {
            SqlCommand cmd = new SqlCommand("select * from tblFood", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "hotel");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "hotel";
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblFood where fid=" + label7.Text, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox1.Text = dr["fname"].ToString();
                    textBox2.Text = dr["fprice"].ToString();
                    if (dr["ftype"].ToString() == "V")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                    comboBox1.Text = dr["favailable"].ToString();
                }
                con.Close();
            }
            
            catch(System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Kindly select the food item");
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Kindly enter the food name...", "Missing Input");
                return;
            }
            else if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("Kindly enter the price of the food...", "Missing Input");
                return;
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Kindly select Food Availability...", "Missing Input");
                return;
            }
            else if (radioButton1.Checked is false && radioButton2.Checked is false)
            {
                MessageBox.Show("Kindly select the food type...", "Missing Input");
                return;
            }
            else
            {
                if (radioButton1.Checked)
                {
                    label6.Text = "V";
                }
                if (radioButton2.Checked)
                {
                    label6.Text = "N";
                }
            }
            try
            {
                SqlCommand cmd = new SqlCommand("update tblFood set fname = '" + textBox1.Text + "'," +"ftype='" + label6.Text + "',fprice=" + textBox2.Text + ",favailable='" + comboBox1.Text + "' where fid=" + label7.Text, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "hotel");
                textBox1.Clear();
                textBox2.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                comboBox1.Text = "";
                view();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Kindly enter amount in numbers....", "Wrong input");
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            StartingForm mf = new StartingForm();
            this.Close();
            mf.Show();
            mf.WindowState = FormWindowState.Maximized;
        }
    }
}
