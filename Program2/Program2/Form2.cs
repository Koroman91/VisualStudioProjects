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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Fillcombo();
            Fillcombo2();
            FilllistBox();
            load_table();
            timer1.Start();
        }

        void FilllistBox()
        {
          string constring = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  string Query= "select * from proba1.Konekcija;";
                  MySqlConnection conDataBase = new MySqlConnection(constring);

                  MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                  MySqlDataReader myReader;
            try{
                  conDataBase.Open();
                  myReader = cmdDataBase.ExecuteReader();
                  
                  while (myReader.Read())
                  {
                      string sName=myReader.GetString("name");
                      listBox1.Items.Add(sName);
                  }
                  
               }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
        }


        void Fillcombo()
        {
            string constring = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  string Query= "select * from proba1.Konekcija;";
                  MySqlConnection conDataBase = new MySqlConnection(constring);

                  MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                  MySqlDataReader myReader;
            try{
                  conDataBase.Open();
                  myReader = cmdDataBase.ExecuteReader();
                  
                  while (myReader.Read())
                  {
                      string sName=myReader.GetString("name");
                      comboBox1.Items.Add(sName);
                  }
                  
               }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
        }


        void Fillcombo2()
        {
            string constring = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  string Query= "select * from proba1.Konekcija;";
                  MySqlConnection conDataBase = new MySqlConnection(constring);

                  MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                  MySqlDataReader myReader;
            try{
                  conDataBase.Open();
                  myReader = cmdDataBase.ExecuteReader();
                  
                  while (myReader.Read())
                  {
                      string sName=myReader.GetString("name");
                      comboBox2.Items.Add(sName);
                  }
                  
               }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
        }

        void load_table(){
                  string constring = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  MySqlConnection conDataBase = new MySqlConnection(constring);

                  MySqlCommand cmdDataBase = new MySqlCommand("select * from proba1.Konekcija;", conDataBase);
                  
            try{
                  MySqlDataAdapter sda = new MySqlDataAdapter();
                  sda.SelectCommand = cmdDataBase;
                  DataTable dbdataset = new DataTable();
                  sda.Fill(dbdataset);
                  BindingSource bSource = new BindingSource();

                  bSource.DataSource = dbdataset;
                  dataGridView1.DataSource = bSource;
                  sda.Update(dbdataset);
         }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }


         }


        private void button1_Click(object sender, EventArgs e)
        {

                  string constring = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  string Query= "insert into proba1.Konekcija (Eid, name, surname, age) values ('"+this.Eid_txt.Text+"','"+this.Name_txt.Text+"','"+this.Surname_txt.Text+"','"+this.Age_txt.Text+"');";
                  MySqlConnection conDataBase = new MySqlConnection(constring);

                  MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                  MySqlDataReader myReader;
            try{
                  conDataBase.Open();
                  myReader = cmdDataBase.ExecuteReader();
                  MessageBox.Show("Saved");
                  while (myReader.Read())
                  {
                   
                  }
                  
               }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
        }

        private void button2_Click(object sender, EventArgs e)
        {
                  string constring = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  string Query= "update proba1.Konekcija set Eid='"+this.Eid_txt.Text+"',name='"+this.Name_txt.Text+"',surname='"+this.Surname_txt.Text+"',age='"+this.Age_txt.Text+"'where Eid='"+this.Eid_txt.Text+"';";
                  MySqlConnection conDataBase = new MySqlConnection(constring);

                  MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                  MySqlDataReader myReader;
            try{
                  conDataBase.Open();
                  myReader = cmdDataBase.ExecuteReader();
                  MessageBox.Show("Updated");
                  while (myReader.Read())
                  {
                   
                  }
                  
               }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
        }

        private void button3_Click(object sender, EventArgs e)
        {
string constring = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  string Query= "insert into proba1.Konekcija (Eid, name, surname, age) values ('"+this.Eid_txt.Text+"','"+this.Name_txt.Text+"','"+this.Surname_txt.Text+"','"+this.Age_txt.Text+"');";
                  MySqlConnection conDataBase = new MySqlConnection(constring);

                  MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                  MySqlDataReader myReader;
            try{
                  conDataBase.Open();
                  myReader = cmdDataBase.ExecuteReader();
                  MessageBox.Show("Saved");
                  while (myReader.Read())
                  {
                   
                  }
                  
               }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
              string constring = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  string Query= "delete from proba1.Konekcija where Eid='"+ this.Eid_txt.Text + "' ;";
                  MySqlConnection conDataBase = new MySqlConnection(constring);

                  MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                  MySqlDataReader myReader;
            try{
                  conDataBase.Open();
                  myReader = cmdDataBase.ExecuteReader();
                  MessageBox.Show("Deleted");
                  while (myReader.Read())
                  {
                   
                  }
                  
               }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string namestr = Name_txt.Text;
            comboBox1.Items.Add(namestr);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Eid_txt.Text= comboBox1.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
             string constring = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  string Query= "select * from proba1.Konekcija where name = '" + comboBox2.Text +"' ;";
                  MySqlConnection conDataBase = new MySqlConnection(constring);

                  MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                  MySqlDataReader myReader;
            try{
                  conDataBase.Open();
                  myReader = cmdDataBase.ExecuteReader();
                  
                  while (myReader.Read())
                  {
                      string sEid=myReader.GetInt32("Eid").ToString();
                      string sName=myReader.GetString("name");
                      string sSurname=myReader.GetString("surname");
                      string sAge=myReader.GetInt32("age").ToString();
                      Eid_txt.Text=sEid;
                      Name_txt.Text=sName;
                      Surname_txt.Text=sSurname;
                      Age_txt.Text=sAge;
                  }
                  
               }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
        }

        private void button5_Click(object sender, EventArgs e)
        {
              string namestr = Name_txt.Text;
              comboBox2.Items.Add(namestr);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string constring = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  string Query= "select * from proba1.Konekcija where name = '" + listBox1.Text +"' ;";
                  MySqlConnection conDataBase = new MySqlConnection(constring);

                  MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                  MySqlDataReader myReader;
            try{
                  conDataBase.Open();
                  myReader = cmdDataBase.ExecuteReader();
                  
                  while (myReader.Read())
                  {
                      string sEid=myReader.GetInt32("Eid").ToString();
                      string sName=myReader.GetString("name");
                      string sSurname=myReader.GetString("surname");
                      string sAge=myReader.GetInt32("age").ToString();
                      Eid_txt.Text=sEid;
                      Name_txt.Text=sName;
                      Surname_txt.Text=sSurname;
                      Age_txt.Text=sAge;
                  }
                  
               }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
        }

        private void button6_Click(object sender, EventArgs e)
        {
                  string constring = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  MySqlConnection conDataBase = new MySqlConnection(constring);

                  MySqlCommand cmdDataBase = new MySqlCommand("select * from proba1.Konekcija;", conDataBase);
                  
            try{
                  MySqlDataAdapter sda = new MySqlDataAdapter();
                  sda.SelectCommand = cmdDataBase;
                  DataTable dbdataset = new DataTable();
                  sda.Fill(dbdataset);
                  BindingSource bSource = new BindingSource();

                  bSource.DataSource = dbdataset;
                  dataGridView1.DataSource = bSource;
                  sda.Update(dbdataset);
         }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            /*this.chart1.Series["Age"].Points.AddXY("Max",33);
            this.chart1.Series["Score"].Points.AddXY("Max",90);

            this.chart1.Series["Age"].Points.AddXY("Carl",20);
            this.chart1.Series["Score"].Points.AddXY("Carl",70);

            this.chart1.Series["Age"].Points.AddXY("Mark",50);
            this.chart1.Series["Score"].Points.AddXY("Mark",56);

            this.chart1.Series["Age"].Points.AddXY("Alli",40);
            this.chart1.Series["Score"].Points.AddXY("Alli",30);
            */

          string constring = "datasource=localhost; port=3306; username=root; password=megatrend91";
                  MySqlConnection conDataBase = new MySqlConnection(constring);

                  MySqlCommand cmdDataBase = new MySqlCommand("select * from proba1.Konekcija;", conDataBase);
                  MySqlDataReader myReader;
                  
            try{
                  conDataBase.Open();
                  myReader=cmdDataBase.ExecuteReader();

                 while (myReader.Read())
                 {
                     this.chart1.Series["Age"].Points.AddXY(myReader.GetString("name"), myReader.GetInt32("Age"));
                     
                 }




         }
            catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
         
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime=DateTime.Now;
            this.time_lbl.Text=dateTime.ToString();
        }

        private void time_lbl_Click(object sender, EventArgs e)
        {

        }
        }

   
    }

