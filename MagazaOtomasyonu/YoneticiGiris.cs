using System;
using System.Windows.Forms;

namespace MagazaOtomasyonu
{
    public partial class YoneticiGiris : Form
    {
        public YoneticiGiris()
        {
            InitializeComponent();
        }
        Baglanti baglanti = new Baglanti();


        private void button3_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Kullanıcı Adı";
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

            if (textBox1.Text == "Kullanıcı Adı")
            {
                textBox1.Text = "";
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Şifre")
            {
                textBox2.Text = "";
            }
            textBox2.UseSystemPasswordChar = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Şifre";
                textBox2.UseSystemPasswordChar = false;
            }

        }

        private void girisbtn_Click(object sender, EventArgs e)
        {
            String cevap;
            cevap = baglanti.prosedurYoneticiGiris("" + textBox1.Text + "", "" + textBox2.Text + "");

            if (cevap == "Bilgiler Doğru")
            {
                YoneticiEkran y1 = new YoneticiEkran();
                y1.id = baglanti.yoneticiid;
                y1.ad = baglanti.yoneticiisim;
                y1.soyad = baglanti.yoneticisoyisim;
                y1.Show();
                this.Close();
            }
            else if (cevap == "Bilgiler Yanlış")
            {
                label1.Text = cevap;
            }
        }
    }
}
