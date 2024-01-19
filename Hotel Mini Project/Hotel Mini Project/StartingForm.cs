using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Mini_Project
{
    public partial class StartingForm : Form
    {
        public StartingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            NewEntryForm nf = new NewEntryForm();
            nf.Show();
            nf.WindowState = FormWindowState.Maximized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sales_Billing nf = new Sales_Billing();
            nf.Show();
            nf.WindowState = FormWindowState.Maximized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Search_Sales nf = new Search_Sales();
            nf.Show();
            nf.WindowState = FormWindowState.Maximized;
        }
    }
}
