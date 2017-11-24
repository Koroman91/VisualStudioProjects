using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Program1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
try
{
    string myConnection = "datasource=localhost;port=3306;username=root; password=megatrend91";
    MySqlConnection myConn = new MySqlConnection(myConnection);
    MySqlDataAdapter myDataAdapter = new MySqlDataAdapter();
    myDataAdapter.SelectCommand = new MySqlCommand(" select * from imena ;", myConn);
    MySqlCommandBuilder cb = new MySqlCommandBuilder(myDataAdapter);
    myConn.Open();
    //DataSet ds = new DataSet();
    MessageBox.Show("Connected");
    myConn.Close();
}
catch (Exception ex)
{
    MessageBox.Show(ex.Message);
}
        }
    }
}
