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
using System.Collections;
using static System.Windows.Forms.AxHost;
using System.Net;
using System.Reflection.Emit;

namespace Hotel_Mini_Project
{
    public partial class Sales_Billing : Form
    {
        public Sales_Billing()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=hoteldb;Integrated Security=True");

        public int random()
        {
            Random rn = new Random();
            return rn.Next(1000, 9999);

        }
        private void Sales_Billing_Load(object sender, EventArgs e)
        {
            textBox2.Text = random().ToString();

            SqlCommand cmd = new SqlCommand("select fid,fname from tblfood order by fname", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow dr = dt.NewRow();
            dr[1] = "Select Food";
            dt.Rows.InsertAt(dr, 0);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "fname";
            comboBox1.ValueMember = "fid";
        }
        static int i = 1;
        static int total = 0;
        public static DataTable dt = new DataTable() { Columns = { new DataColumn(@"S.No"), new DataColumn(@"Food"), new DataColumn(@"Price"), new DataColumn(@"Quantity"), new DataColumn(@"Amount") } };
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into tblbilling values("+ textBox2.Text + ",getdate(),"+comboBox1.SelectedValue+"," + textBox1.Text + "," + textBox3.Text + "," + textBox4.Text + ")", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "bill");
            DataRow dr = dt.NewRow();
            dr["S.No"] = i;
            dr["Food"] = comboBox1.Text;
            dr["Price"] = double.Parse(textBox1.Text);
            dr["Quantity"] = int.Parse(textBox3.Text);
            dr["Amount"] = int.Parse(textBox4.Text);
            dt.Rows.Add(dr);
            dataGridView1.DataSource = dt;
            total = total + int.Parse(textBox4.Text);
            textBox5.Text = total.ToString();
            i++;
            textBox4.Clear();





        }
        public void view()
        {
            
            SqlCommand cmd = new SqlCommand("select ROW_NUMBER() Over(Order by CustomerID) as S.No,fid,fname as Food from tblfood right join tblbilling on fid=fid", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "bill");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "bill";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label8.Text = comboBox1.Text;
            

                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblfood where fname = '" + label8.Text+"'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox1.Text = dr["fprice"].ToString();
                }
                con.Close();
            

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if(textBox3.Text != "")
            {
                if (int.Parse(textBox3.Text) > 0)
                {
                    double cost = double.Parse(textBox1.Text);
                    double qty = double.Parse(textBox3.Text);
                    textBox4.Text = (cost * qty).ToString();
                }
            }
                
           
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
