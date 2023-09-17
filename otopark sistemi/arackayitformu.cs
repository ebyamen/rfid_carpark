using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace otopark_sistemi
{
    public partial class arackayitformu : Form
    {
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Ace.OleDB.12.0;data source=veri.accdb");
        OleDbCommand kmt = new OleDbCommand();
        public arackayitformu()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string sonuc;
            sonuc = serialPort1.ReadExisting();


            if (sonuc != "")
            {
                label6.Text = sonuc;
            }
          
        }

        private void arackayitformu_Load(object sender, EventArgs e)
        {
            serialPort1.PortName = Form1.portismi;
            serialPort1.BaudRate = Convert.ToInt16(Form1.banthizi);

            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.Open();
                    label5.Text = "Bağlantı Sağlandı";
                    label5.ForeColor = Color.Green;
                }
                catch
                {
                    label5.Text = "Bağlantı Sağlanamadı";

                }
            }
            else
            {
                label5.Text = "Bağlantı Sağlanamadı";
                label5.ForeColor = Color.Red;

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
            textBox1.Text = "";
            comboBox1.Text = "Seçiniz...";
            textBox3.Text = "";
            label6.Text = "__________";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyaları (jpg) |*jpg|Tüm Dosyalar |*.*";
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\foto";
            dosya.RestoreDirectory = true;

            if (dosya.ShowDialog() == DialogResult.OK)
            {
                string di = dosya.SafeFileName;
                textBox3.Text = di;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label6.Text == "__________" || textBox1.Text == "" || comboBox1.Text == "Seçiniz..." || textBox3.Text == "")
            {
                label7.Text = "Bilgileri Eksiksiz Giriniz";
                label7.ForeColor = Color.Red;
            }
            else
            {
                try
                {
                    bag.Open();

                    kmt.Connection = bag;
                    kmt.CommandText = "INSERT INTO tablo (kid,plaka,model,resimbilgisi)VALUES('" + label6.Text + "','" + textBox1.Text + "','" + comboBox1.Text + "','" + textBox3.Text + "')";
                    kmt.ExecuteNonQuery();
                    label7.Text = "Kayıt Yapıldı";
                    label7.ForeColor = Color.Green;

                    bag.Close();
                }
                catch 
                {
                    bag.Close();
                    MessageBox.Show("Bu Kart Zaten Kayıtlı");
                }
            }
        }

        private void arackayitformu_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            serialPort1.Close();

        }
    }
}
