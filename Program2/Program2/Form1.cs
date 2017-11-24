using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Program2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            password_txt.PasswordChar='*';
            password_txt.MaxLength=10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try{
                  string myConnection = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  MySqlConnection myConn = new MySqlConnection(myConnection);

                  MySqlCommand SelectCommand = new MySqlCommand("select * from proba1.imena where username ='" +this.username_txt.Text + "' and password='" +this.password_txt.Text + "' ;", myConn);
                  MySqlDataReader myReader;
                  myConn.Open();
                  myReader = SelectCommand.ExecuteReader();
                  int count = 0;
                  while (myReader.Read())
                  {
                   count = count + 1;
                  }
                  if (count == 1)
                     {
                       MessageBox.Show("Username and Password is correct");
                       this.Hide();
                       Form2 f2 = new Form2();
                       f2.ShowDialog();
                     }
                  else if (count > 1)
                     {
                       MessageBox.Show("Duplicate Username and password...Access denied");
                     }
                  else 
                      MessageBox.Show("Username and password is Not correct...Please try again");
                      myConn.Close();
               }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
        }


    }
}
