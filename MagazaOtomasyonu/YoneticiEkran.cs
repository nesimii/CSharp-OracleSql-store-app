using Oracle.ManagedDataAccess.Client;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace MagazaOtomasyonu
{
    public partial class YoneticiEkran : Form
    {
        public YoneticiEkran()
        {
            InitializeComponent();
        }
        public string id, ad, soyad;// giriş yapan yöneticinin bilgileri saklanır.
        Baglanti baglanti = new Baglanti();
        OracleCommand komut = new OracleCommand();
        OracleDataReader oku;
        private void YoneticiEkran_Load(object sender, EventArgs e)
        {
            /*int genislik = Screen.PrimaryScreen.WorkingArea.Width;
            int yukseklik = Screen.PrimaryScreen.Bounds.Height;
            this.Size = new Size((int)(genislik * 0.8), (int)(yukseklik * 0.8));
            this.Location = new Point((int)(genislik - genislik * 0.8) / 2, (int)(yukseklik - yukseklik * 0.8) / 2);*/

            label1.Text = ad + " " + soyad;
            dataGridView1.DataSource = baglanti.tablodoldurma("SELECT * FROM PERSONEL");
            dataGridView2.DataSource = baglanti.tablodoldurma("SELECT * FROM MUSTERI");
            dataGridView3.DataSource = baglanti.tablodoldurma("SELECT * FROM KARKAZANCTABLOSU");
            dataGridView4.DataSource = baglanti.tablodoldurma("select * from personel");
            dataGridView5.DataSource = baglanti.tablodoldurma("SELECT BARKODNO,KATEGORIADI,URUNADI,URUNGELISFIYATI,URUNSATISFIYATI,RENK,BEDEN,STOKSAYISI FROM URUNLER u JOIN KATEGORI k ON u.kategorııd=k.kategorııd");
            dataGridView6.DataSource = baglanti.tablodoldurma("SELECT * FROM ISLEMLERVIEW");
            kategoriekle();

        }
        public void kategoriekle()
        {
            ukategoritbox.Items.Clear();
            ukategori1tbox.Items.Clear();
            this.oku = baglanti.okuyucu("select KATEGORIADI FROM KATEGORI");
            while (this.oku.Read() == true)
            {
                ukategoritbox.Items.Add(this.oku.GetString(0));
                ukategori1tbox.Items.Add(this.oku.GetString(0));
            }
            this.oku.Close();
            baglanti.baglantikapat();

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Idtboxp.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            adtboxp.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            soyadtboxp.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            teltboxp.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            adrestboxp.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            maastboxp.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            kulidtboxp.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            sifretboxp.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void eklebtnp_Click(object sender, EventArgs e)
        {
            if (Idtboxp.Text == "" || adtboxp.Text == "" || soyadtboxp.Text == "" || teltboxp.Text == "" || adrestboxp.Text == "" || maastboxp.Text == "" || kulidtboxp.Text == "" || sifretboxp.Text == "")
            {
                MessageBox.Show("lütfen tüm alanları doldurunuz");
            }
            else
            {
                string cevap;
                string id = "1";
                cevap = baglanti.prosedurPersonelIslem("kayit", id, adtboxp.Text, soyadtboxp.Text, teltboxp.Text, adrestboxp.Text, maastboxp.Text, kulidtboxp.Text, sifretboxp.Text);
                MessageBox.Show(cevap);
                if (cevap == "Kayıt Yapıldı")
                {
                    tabControl1_SelectedIndexChanged(null, null);
                }
            }


        }

        private void guncellebtnp_Click(object sender, EventArgs e)
        {
            string cevap;
            if (Idtboxp.Text == "" || adtboxp.Text == "" || soyadtboxp.Text == "" || teltboxp.Text == "" || adrestboxp.Text == "" || maastboxp.Text == "" || kulidtboxp.Text == "" || sifretboxp.Text == "")
            {
                MessageBox.Show("tablodan personel seçiniz");
            }
            else
            {

                cevap = baglanti.prosedurPersonelIslem("guncelle", Idtboxp.Text, adtboxp.Text, soyadtboxp.Text, teltboxp.Text, adrestboxp.Text, maastboxp.Text, kulidtboxp.Text, sifretboxp.Text);
                MessageBox.Show(cevap);

                if (cevap == "Kayıt Güncellendi")
                {
                    tabControl1_SelectedIndexChanged(null, null);
                }
            }



        }

        private void silbtnp_Click(object sender, EventArgs e)
        {
            if (Idtboxp.Text == "" || adtboxp.Text == "" || soyadtboxp.Text == "" || teltboxp.Text == "" || adrestboxp.Text == "" || maastboxp.Text == "" || kulidtboxp.Text == "" || sifretboxp.Text == "")
            {
                MessageBox.Show("tablodan personel seçiniz");
            }
            else
            {
                string cevap;
                cevap = baglanti.prosedurPersonelIslem("sil", Idtboxp.Text, adtboxp.Text, soyadtboxp.Text, teltboxp.Text, adrestboxp.Text, maastboxp.Text, kulidtboxp.Text, sifretboxp.Text);
                MessageBox.Show(cevap);
                if (cevap == "Kayıt Silindi")
                {
                    tabControl1_SelectedIndexChanged(null, null);
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Idtboxm.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            adtboxm.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            soyadtboxm.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            teltboxm.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            adrestboxm.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
        }

        private void eklebtnm_Click(object sender, EventArgs e)
        {
            if (adtboxm.Text == "" || soyadtboxm.Text == "" || teltboxm.Text == "" || adrestboxm.Text == "")
            {
                MessageBox.Show("tüm alanları doldurunuz");
            }
            else
            {
                string cevap;
                cevap = baglanti.prosedurMusteriIslem("ekle", Idtboxm.Text, adtboxm.Text, soyadtboxm.Text, teltboxm.Text, adrestboxm.Text);
                MessageBox.Show(cevap);
                if (cevap == "Kayıt Yapıldı")
                {
                    tabControl1_SelectedIndexChanged(null, null);
                }
            }


        }

        private void guncellebtnm_Click(object sender, EventArgs e)
        {
            if (Idtboxm.Text == "")
            {
                MessageBox.Show("tablodan müşteri seçiniz");
            }
            else
            {
                string cevap;
                cevap = baglanti.prosedurMusteriIslem("guncelle", Idtboxm.Text, adtboxm.Text, soyadtboxm.Text, teltboxm.Text, adrestboxm.Text);
                MessageBox.Show(cevap);

                if (cevap == "Kayıt Güncellendi")
                {
                    tabControl1_SelectedIndexChanged(null, null);
                }
            }
        }

        private void silbtnm_Click(object sender, EventArgs e)
        {
            if (Idtboxm.Text == "")
            {
                MessageBox.Show("tablodan müşteri seçiniz");
            }
            else
            {
                string cevap;
                cevap = baglanti.prosedurMusteriIslem("sil", Idtboxm.Text, adtboxm.Text, soyadtboxm.Text, teltboxm.Text, adrestboxm.Text);
                MessageBox.Show(cevap);
                if (cevap == "Kayıt Silindi")
                {
                    tabControl1_SelectedIndexChanged(null, null);
                }

            }
        }
        int buyultmemod = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (buyultmemod % 2 == 0)
            {
                this.WindowState = FormWindowState.Maximized;
                buyultmemod = 1;
            }
            else if (buyultmemod % 2 == 1)
            {
                this.WindowState = FormWindowState.Normal;
                buyultmemod = 2;
            }

        }

        string personelid;
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            spersonellabel.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString() + " " + dataGridView4.CurrentRow.Cells[2].Value.ToString();
            personelid = dataGridView4.CurrentRow.Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hepsiradio.Checked == true)
            {
                if (tarihcheck.Checked == true)
                {
                    dataGridView3.DataSource = baglanti.tablodoldurma("select * from karkazanctablosu where TARIH BETWEEN TO_DATE('" + ilkdateTimePicker1.Value.Date.ToString("dd / MM / yyyy") + "', 'dd/mm/yyyy') and TO_DATE('" + sondateTimePicker1.Value.ToString("dd / MM / yyyy") + "','dd/mm/yyyy')");

                    this.oku = baglanti.okuyucu("select sum(NETKAR) as KAR from karkazanctablosu where TARIH BETWEEN TO_DATE('" + ilkdateTimePicker1.Value.Date.ToString("dd / MM / yyyy") + "', 'dd/mm/yyyy') and TO_DATE('" + sondateTimePicker1.Value.ToString("dd / MM / yyyy") + "','dd/mm/yyyy')");
                    this.oku.Read();

                    if (this.oku.IsDBNull(this.oku.GetOrdinal("KAR")))
                    {
                        MessageBox.Show("satış yok");
                    }
                    else
                    {
                        toplamkazanclabel.Text = "Toplam Kazanç: " + this.oku.GetString(0);
                    }
                    this.oku.Close();
                    baglanti.baglantikapat();

                }
                else
                {
                    dataGridView3.DataSource = baglanti.tablodoldurma("SELECT * FROM KARKAZANCTABLOSU");
                    this.oku = baglanti.okuyucu("select sum(NETKAR) as KAR from karkazanctablosu");
                    this.oku.Read();
                    if (this.oku.IsDBNull(this.oku.GetOrdinal("KAR")))
                    {
                        MessageBox.Show("Personel hiç satış yapmamıştır");
                    }
                    else
                    {
                        toplamkazanclabel.Text = "Toplam Kazanç: " + this.oku.GetString(0);
                    }
                    this.oku.Close();
                    baglanti.baglantikapat();

                }

            }
            else if (secilipersonelradio.Checked == true)
            {
                if (tarihcheck.Checked == true)
                {
                    dataGridView3.DataSource = baglanti.tablodoldurma("select * from karkazanctablosu where PID=" + dataGridView4.CurrentRow.Cells[0].Value.ToString() + " and TARIH BETWEEN TO_DATE('" + ilkdateTimePicker1.Value.Date.ToString("dd / MM / yyyy") + "', 'dd/mm/yyyy') and TO_DATE('" + sondateTimePicker1.Value.ToString("dd / MM / yyyy") + "','dd/mm/yyyy')");
                    this.oku = baglanti.okuyucu("select sum(NETKAR) as KAR from karkazanctablosu where PID=" + dataGridView4.CurrentRow.Cells[0].Value.ToString() + " and TARIH BETWEEN TO_DATE('" + ilkdateTimePicker1.Value.Date.ToString("dd / MM / yyyy") + "', 'dd/mm/yyyy') and TO_DATE('" + sondateTimePicker1.Value.ToString("dd / MM / yyyy") + "','dd/mm/yyyy')");
                    this.oku.Read();

                    if (this.oku.IsDBNull(this.oku.GetOrdinal("KAR")))
                    {
                        MessageBox.Show("Seçilen tarih aralığında personel satış yapmamıştır");
                    }
                    else
                    {
                        toplamkazanclabel.Text = "Toplam Kazanç: " + this.oku.GetString(0);
                    }
                    this.oku.Close();
                    baglanti.baglantikapat();

                }
                else
                {
                    dataGridView3.DataSource = baglanti.tablodoldurma("select * from karkazanctablosu where PID=" + dataGridView4.CurrentRow.Cells[0].Value.ToString());
                    this.oku = baglanti.okuyucu("select sum(NETKAR) as KAR from karkazanctablosu where PID=" + dataGridView4.CurrentRow.Cells[0].Value.ToString());
                    this.oku.Read();

                    if (this.oku.IsDBNull(this.oku.GetOrdinal("KAR")))
                    {
                        MessageBox.Show("personel satış yapmamıştır");
                    }
                    else
                    {
                        toplamkazanclabel.Text = "Toplam Kazanç: " + this.oku.GetString(0);
                    }
                    this.oku.Close();
                    baglanti.baglantikapat();
                }
            }
            else if (personeltoplamsatisradio.Checked == true)
            {
                if (tarihcheck.Checked == true)
                {
                    dataGridView3.DataSource = baglanti.tablodoldurma("SELECT p.adı,p.soyadı,COUNT(personelID) as SATISSAYISI from SATIS s, PERSONEL p where p.ıd=s.personelıd and TARIH BETWEEN TO_DATE('" + ilkdateTimePicker1.Value.Date.ToString("dd / MM / yyyy") + "', 'dd/mm/yyyy') and TO_DATE('" + sondateTimePicker1.Value.ToString("dd / MM / yyyy") + "', 'dd/mm/yyyy') GROUP BY p.adı, p.soyadı");
                }
                else
                {
                    dataGridView3.DataSource = baglanti.tablodoldurma("SELECT p.adı,p.soyadı,COUNT(personelID) as SatışSayısı from SATIS s, PERSONEL p where p.ıd=s.personelıd GROUP BY p.adı, p.soyadı");
                }
            }
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ubarkodnotbox.Text = dataGridView5.CurrentRow.Cells[0].Value.ToString();
            ukategoritbox.Text = dataGridView5.CurrentRow.Cells[1].Value.ToString();
            uadtbox.Text = dataGridView5.CurrentRow.Cells[2].Value.ToString();
            ugfiyattbox.Text = dataGridView5.CurrentRow.Cells[3].Value.ToString();
            usfiyattbox.Text = dataGridView5.CurrentRow.Cells[4].Value.ToString();
            urenktbox.Text = dataGridView5.CurrentRow.Cells[5].Value.ToString();
            ubedentbox.Text = dataGridView5.CurrentRow.Cells[6].Value.ToString();
            ustoktbox.Text = dataGridView5.CurrentRow.Cells[7].Value.ToString();
        }

        private void kategorieklebtn_Click(object sender, EventArgs e)
        {
            if (kategoriekletbox.Text == "")
            {
                MessageBox.Show("kategori alanını doldurunuz");
            }
            else
            {
                MessageBox.Show(baglanti.prosedurKategoriIslem("ekle", kategoriekletbox.Text));
                kategoriekle();
            }


        }

        private void kategorisilbtn_Click(object sender, EventArgs e)
        {
            if (ukategori1tbox.Text == "")
            {
                MessageBox.Show("kategori seçiniz");
            }
            else
            {
                MessageBox.Show(baglanti.prosedurKategoriIslem("sil", ukategori1tbox.Text));
                kategoriekle();
            }

        }

        private void uruneklebtn_Click(object sender, EventArgs e)
        {
            if (ubarkodnotbox.Text == "" || ukategoritbox.Text == "" || uadtbox.Text == "" || ugfiyattbox.Text == "" || usfiyattbox.Text == "" || urenktbox.Text == "" || urenktbox.Text == "" || ustoktbox.Text == "")
            {
                MessageBox.Show("tüm kutucukları doldurunuz");
            }
            else
            {
                MessageBox.Show(baglanti.prosedurUrunIslem("ekle", ubarkodnotbox.Text, ukategoritbox.Text, uadtbox.Text, ugfiyattbox.Text, usfiyattbox.Text, urenktbox.Text, ubedentbox.Text, ustoktbox.Text));
                tabControl1_SelectedIndexChanged(null, null);
            }

        }

        private void urunguncellebtn_Click(object sender, EventArgs e)
        {
            if (ubarkodnotbox.Text == "" || ukategoritbox.Text == "" || uadtbox.Text == "" || ugfiyattbox.Text == "" || usfiyattbox.Text == "" || urenktbox.Text == "" || urenktbox.Text == "" || ustoktbox.Text == "")
            {
                MessageBox.Show("tüm kutucukları doldurunuz");
            }
            else
            {
                MessageBox.Show(baglanti.prosedurUrunIslem("guncelle", ubarkodnotbox.Text, ukategoritbox.Text, uadtbox.Text, ugfiyattbox.Text, usfiyattbox.Text, urenktbox.Text, ubedentbox.Text, ustoktbox.Text));
                tabControl1_SelectedIndexChanged(null, null);
            }

        }

        private void urunsilbtn_Click(object sender, EventArgs e)
        {
            if (ubarkodnotbox.Text == "" || ukategoritbox.Text == "" || uadtbox.Text == "" || ugfiyattbox.Text == "" || usfiyattbox.Text == "" || urenktbox.Text == "" || urenktbox.Text == "" || ustoktbox.Text == "")
            {
                MessageBox.Show("tüm kutucukları doldurunuz");
            }
            else
            {
                MessageBox.Show(baglanti.prosedurUrunIslem("sil", ubarkodnotbox.Text, ukategoritbox.Text, uadtbox.Text, ugfiyattbox.Text, usfiyattbox.Text, urenktbox.Text, ubedentbox.Text, ustoktbox.Text));
                tabControl1_SelectedIndexChanged(null, null);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //personel ekranı sıfırlama
            dataGridView1.DataSource = baglanti.tablodoldurma("SELECT * FROM PERSONEL");
            Idtboxp.Text = "";
            adtboxp.Text = "";
            soyadtboxp.Text = "";
            teltboxp.Text = "";
            adrestboxp.Text = "";
            maastboxp.Text = "";
            kulidtboxp.Text = "";
            sifretboxp.Text = "";
            label27.Text = "-";
            label28.Text = "-";
            dataGridView1.ClearSelection();

            //musteri ekrani sıfırlama
            dataGridView2.DataSource = baglanti.tablodoldurma("SELECT * FROM MUSTERI");
            Idtboxm.Text = "";
            adtboxm.Text = "";
            soyadtboxm.Text = "";
            teltboxm.Text = "";
            adrestboxm.Text = "";
            dataGridView2.ClearSelection();

            dataGridView3.DataSource = baglanti.tablodoldurma("SELECT * FROM KARKAZANCTABLOSU");
            dataGridView3.ClearSelection();
            dataGridView4.DataSource = baglanti.tablodoldurma("select * from personel");
            dataGridView4.ClearSelection();

            //urun islem ekrani sifirlama
            dataGridView5.DataSource = baglanti.tablodoldurma("SELECT BARKODNO,KATEGORIADI,URUNADI,URUNGELISFIYATI,URUNSATISFIYATI,RENK,BEDEN,STOKSAYISI FROM URUNLER u JOIN KATEGORI k ON u.kategorııd=k.kategorııd");
            ubarkodnotbox.Text = "";
            uadtbox.Text = "";
            ugfiyattbox.Text = "";
            usfiyattbox.Text = "";
            urenktbox.Text = "";
            ustoktbox.Text = "";
            kategoriekletbox.Text = "";
            dataGridView5.ClearSelection();
        }

        private void kulidtboxp_TextChanged(object sender, EventArgs e)
        {
            if (Idtboxp.Text == "")
            {
                this.oku = baglanti.okuyucu("SELECT KULLANICIADI FROM PERSONEL WHERE KULLANICIADI='" + kulidtboxp.Text + "'");
                if (this.oku.Read())
                {
                    label27.Text = "kullanıcı adı kullanılıyor";
                    label27.ForeColor = Color.Red;
                    eklebtnp.Enabled = false;
                    guncellebtnp.Enabled = false;
                    silbtnp.Enabled = false;
                }
                else
                {
                    label27.Text = "kullanıcı adı kullanılabilir";
                    label27.ForeColor = Color.Green;
                    eklebtnp.Enabled = true;
                    guncellebtnp.Enabled = true;
                    silbtnp.Enabled = true;
                }
                baglanti.baglantikapat();
            }
            else if (Idtboxp.Text != "")
            {
                this.oku = baglanti.okuyucu("SELECT ID,KULLANICIADI FROM PERSONEL WHERE ID!=" + Idtboxp.Text + " and KULLANICIADI='" + kulidtboxp.Text + "'");
                if (this.oku.Read())
                {
                    label27.Text = "kullanıcı adı kullanılıyor";
                    label27.ForeColor = Color.Red;
                    eklebtnp.Enabled = false;
                    guncellebtnp.Enabled = false;
                    silbtnp.Enabled = false;
                }
                else
                {
                    label27.Text = "kullanıcı adı kullanılabilir";
                    label27.ForeColor = Color.Green;
                    eklebtnp.Enabled = true;
                    guncellebtnp.Enabled = true;
                    silbtnp.Enabled = true;
                }
                baglanti.baglantikapat();
            }
        }

        private void teltboxp_TextChanged(object sender, EventArgs e)
        {
            if (Idtboxp.Text == "")
            {
                this.oku = baglanti.okuyucu("SELECT KULLANICIADI FROM PERSONEL WHERE TELEFON='" + teltboxp.Text + "'");
                if (this.oku.Read())
                {
                    label28.Text = "numara kayıtlı";
                    label28.ForeColor = Color.Red;
                    eklebtnp.Enabled = false;
                    guncellebtnp.Enabled = false;
                    silbtnp.Enabled = false;
                }
                else
                {
                    label28.Text = "numara kullanılabilir";
                    label28.ForeColor = Color.Green;
                    eklebtnp.Enabled = true;
                    guncellebtnp.Enabled = true;
                    silbtnp.Enabled = true;
                }
                baglanti.baglantikapat();
            }
            else if (Idtboxp.Text != "")
            {
                this.oku = baglanti.okuyucu("SELECT ID,KULLANICIADI FROM PERSONEL WHERE ID!=" + Idtboxp.Text + " and TELEFON='" + teltboxp.Text + "'");
                if (this.oku.Read())
                {
                    label28.Text = "numara kayıtlı";
                    label28.ForeColor = Color.Red;
                    eklebtnp.Enabled = false;
                    guncellebtnp.Enabled = false;
                    silbtnp.Enabled = false;
                }
                else
                {
                    label28.Text = "numara kullanılabilir";
                    label28.ForeColor = Color.Green;
                    eklebtnp.Enabled = true;
                    guncellebtnp.Enabled = true;
                    silbtnp.Enabled = true;
                }
                baglanti.baglantikapat();
            }
        }

        private void teltboxm_TextChanged(object sender, EventArgs e)
        {
            if (Idtboxm.Text == "")
            {
                this.oku = baglanti.okuyucu("SELECT TELEFON FROM MUSTERI WHERE TELEFON='" + teltboxm.Text + "'");
                if (this.oku.Read())
                {
                    label29.Text = "numara kayıtlı";
                    label29.ForeColor = Color.Red;
                    eklebtnm.Enabled = false;
                    guncellebtnm.Enabled = false;
                    silbtnm.Enabled = false;
                }
                else
                {
                    label28.Text = "numara kullanılabilir";
                    label28.ForeColor = Color.Green;
                    eklebtnm.Enabled = true;
                    guncellebtnm.Enabled = true;
                    silbtnm.Enabled = true;
                }
                baglanti.baglantikapat();
            }
            else if (Idtboxm.Text != "")
            {
                this.oku = baglanti.okuyucu("SELECT ID,TELEFON FROM MUSTERI WHERE ID!=" + Idtboxm.Text + " and TELEFON='" + teltboxm.Text + "'");
                if (this.oku.Read())
                {
                    label29.Text = "numara kayıtlı";
                    label29.ForeColor = Color.Red;
                    eklebtnm.Enabled = false;
                    guncellebtnm.Enabled = false;
                    silbtnm.Enabled = false;
                }
                else
                {
                    label29.Text = "numara kullanılabilir";
                    label29.ForeColor = Color.Green;
                    eklebtnm.Enabled = true;
                    guncellebtnm.Enabled = true;
                    silbtnm.Enabled = true;
                }
                baglanti.baglantikapat();
            }
        }

        private void teltboxm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void teltboxp_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Itarihcbox.Checked == true)
            {
                dataGridView6.DataSource = baglanti.tablodoldurma("select * from ISLEMLERVIEW where \"İŞLEM TARİHİ\" BETWEEN TO_DATE('" + Itarihpicker1.Value.Date.ToString("dd / MM / yyyy") + "', 'dd/mm/yyyy') and TO_DATE('" + Itarihpicker2.Value.ToString("dd / MM / yyyy") + "','dd/mm/yyyy')");
            }
            else
            {
                dataGridView6.DataSource = baglanti.tablodoldurma("select * from ISLEMLERVIEW");
            }
        }

        private void ugfiyattbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ustoktbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ubarkodnotbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void usfiyattbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void uadtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void urenktbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void kategoriekletbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void adtboxm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void soyadtboxm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void adtboxp_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void soyadtboxp_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void maastboxp_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void indirbtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void kapatbtn_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Show();
            this.Close();
        }


    }
}
