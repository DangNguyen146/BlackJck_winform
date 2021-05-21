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

namespace BlackJack
{
    public partial class DataClient : Form
    {
        public DataClient()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=BLACKSONIA\\SQLEXPRESS;Initial Catalog=Client;Integrated Security=True");

        private void DataClient_Load(object sender, EventArgs e)
        {
            conn.Open();
            var dap = new SqlDataAdapter("SELECT * FROM CLIENT", conn);
            var table = new DataTable();
            dap.Fill(table);
            datatClient.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dap = new SqlDataAdapter(textBox1.Text.Trim(), conn);
            var table = new DataTable();
            dap.Fill(table);
            datatClient.DataSource = table;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dap = new SqlDataAdapter("SELECT * FROM CLIENT", conn);
            var table = new DataTable();
            dap.Fill(table);
            datatClient.DataSource = table;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dap = new SqlDataAdapter(textBox2.Text, conn);
            var table = new DataTable();
            dap.Fill(table);

            textBox3.Text = table.Rows[0][0].ToString();
        }
    }
}
