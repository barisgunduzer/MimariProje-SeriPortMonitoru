using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mimari_seriport_haberlesme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void COMPortlariListele()
        {
            string[] myPort;

            myPort = System.IO.Ports.SerialPort.GetPortNames();
            comboBox1.Items.AddRange(myPort);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            COMPortlariListele();

        }


        delegate void GelenVerileriGuncelleCallback(string veri);

        private void GelenVerileriGuncelle(string veri)
        {
            if (this.textBox1.InvokeRequired)
            {
                GelenVerileriGuncelleCallback d = new GelenVerileriGuncelleCallback(GelenVerileriGuncelle);
                this.Invoke(d, new object[] { veri });
            }
            else
            {
                this.textBox1.AppendText(veri);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("COM port bulunamadi");
                return;
            }

            if (comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Bağlantı hızı seçiniz");
                return;
            }

            try
            {
                if (serialPort1.IsOpen == false)
                {
                    serialPort1.PortName = comboBox1.SelectedItem.ToString();
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.Open();
                    button1.Text = "BAĞLANTI KES";

                    timer1.Enabled = true;
                }
                else
                {
                    timer1.Enabled = false;
                    serialPort1.Close();
                    button1.Text = "BAĞLANTI AÇ";
                }
            }
            catch
            {
                MessageBox.Show("Bağlantı açılamadı!");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.BytesToRead > 0)
            {
                GelenVerileriGuncelle(serialPort1.ReadExisting());

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
