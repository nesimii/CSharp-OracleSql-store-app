using Oracle.ManagedDataAccess.Client;
using System;
using System.Windows.Forms;

namespace MagazaOtomasyonu
{
    public partial class PersonelGiris : Form
    {
        public PersonelGiris()
        {
            InitializeComponent();
        }
        Baglanti baglanti = new Baglanti();
        OracleDataReader oku;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.oku = baglanti.okuyucu("select id,adı,soyadı from personel where KULLANICIADI= '" + textBox1.Text + "' and SIFRE = '" + textBox2.Text + "'");

            if (this.oku.Read() == true)
            {

                PersonelEkran pekran1 = new PersonelEkran();
                pekran1.pid = oku.GetString(0);
                pekran1.pad = oku.GetString(1);
                pekran1.psoyad = oku.GetString(2);
                baglanti.baglantikapat();
                pekran1.Show();
                this.Close();
            }
            else if (this.oku.Read() == false)
            {
                label1.Text = "Kullanıcı adı veya Şifre Hatalı";
                baglanti.baglantikapat();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Show();
            this.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
