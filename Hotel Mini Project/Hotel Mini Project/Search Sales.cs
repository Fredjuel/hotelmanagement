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
    public partial class Search_Sales : Form
    {
        public Search_Sales()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            StartingForm mf = new StartingForm();
            this.Close();
            mf.Show();
            mf.WindowState = FormWindowState.Maximized;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=hoteldb;Integrated Security=True");

        private void Search_Sales_Load(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select b.billno,f.fname,b.billdate,b.price,b.quantity,b.amount from tblbilling b " +
                "inner join tblFood f on b.fid = f.fid " +
                "where f.fname='"+comboBox1.Text+"' and b.billdate between '"+dateTimePicker1.Value.ToString("MM-dd-yyyy")+"' and '"+ dateTimePicker2.Value.ToString("MM-dd-yyyy") + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "history");
            dataGridView1.DataMember = "history";
            dataGridView1.DataSource = ds;
            con.Open();
            SqlCommand cmd2 = new SqlCommand("select sum(quantity) as tot,sum(amount) as amt from tblBilling where billdate between '" + dateTimePicker1.Value.ToString("MM-dd-yyyy") + "' and '" + dateTimePicker2.Value.ToString("MM-dd-yyyy") + "' and fid = (select fid from tblFood where fname = '" + comboBox1.Text + "' )", con);
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr["tot"].ToString();
                textBox5.Text = dr["amt"].ToString();
            }
            con.Close();
        }
    }
}
