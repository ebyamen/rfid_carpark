using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Data.OleDb;

namespace otopark_sistemi
{
    public partial class Form1 : Form

    {
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Ace.OleDB.12.0;data source=veri.accdb");
        OleDbCommand kmt = new OleDbCommand();
        public static string portismi,banthizi;
        string[] ports = SerialPort.GetPortNames();
        public int kapasite=5;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kapasite = 5;
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
                
            }

            comboBox2.Items.Add("2400");
            comboBox2.Items.Add("4800");
            comboBox2.Items.Add("9600");

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
       
            timer1.Start();
            portismi = comboBox1.Text;
            banthizi = comboBox2.Text;

            try
            {
                serialPort1.PortName = portismi;
                serialPort1.BaudRate = Convert.ToInt16(banthizi);

                serialPort1.Open();
                label1.Text = "Bağlantı Sağlandı";
                label1.ForeColor = Color.Green;
            }
            catch
            {
                serialPort1.Close();
                serialPort1.Open();
                MessageBox.Show("Zaten Bağlı");
              
            }
     
        }


        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                label1.Text = "Bağlantı Kesildi";
                label1.ForeColor = Color.Red;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
               
            }
        }

  

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            string sonuc;
            sonuc = serialPort1.ReadExisting();
            
            
            if (sonuc != "")
            {
     
           
                label2.Text = sonuc;
                bag.Open();
                kmt.Connection = bag;
                kmt.CommandText = "SELECT * FROM tablo WHERE kid='" + sonuc + "'";

                OleDbDataReader oku = kmt.ExecuteReader();

                if (oku.Read())
                {
                    DateTime bugun = DateTime.Now;
                    pictureBox1.Image = Image.FromFile("foto\\" + oku["resimbilgisi"].ToString());
                    label8.Text = oku["plaka"].ToString();
                    label9.Text = oku["model"].ToString();
                    label10.Text = bugun.ToShortDateString();
                    label11.Text = bugun.ToLongTimeString();
                    bag.Close();
                    bag.Open();
                    kmt.CommandText = "INSERT INTO zaman (plaka,marka,tarih,saat)VALUES('" + label8.Text + "','" + label9.Text + "','" + label10.Text + "','" + label11.Text + "')";
                    kmt.ExecuteReader();
                    bag.Close();
                }
                else
                {
                    pictureBox1.Image = Image.FromFile("foto\\yasak.jpg");
                    label2.Text = "Kart Kayıtlı Değil";
                    label8.Text = "-------------";
                    label9.Text = "-------------";
                    label10.Text = "-------------";
                    label11.Text = "-------------";
                }




                bag.Close();
            }
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (portismi == null || banthizi == null)
            {
                MessageBox.Show("Bağlantını Kontrol Et");

            }
            else 
            {
                timer1.Stop();
                serialPort1.Close();
                label1.Text = "Bağlantı Kapalı";
                label1.ForeColor = Color.Red;

                arackayitformu kyt = new arackayitformu();
                kyt.ShowDialog();
            }
            
        }

        
     

       
    }
}
