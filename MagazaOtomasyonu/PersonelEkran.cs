using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MagazaOtomasyonu
{
    public partial class PersonelEkran : Form
    {
        public PersonelEkran()
        {
            InitializeComponent();
        }
        public string pid, pad, psoyad;
        Baglanti baglanti = new Baglanti();
        OracleDataReader oku;

        DataTable tabloyedek = new DataTable();


        private void PersonelEkran_Load(object sender, EventArgs e)
        {

            tabloyedek.Columns.Add("Barkod No", typeof(int));
            tabloyedek.Columns.Add("Ürün Adı", typeof(string));
            tabloyedek.Columns.Add("Beden", typeof(string));
            tabloyedek.Columns.Add("Adet", typeof(int));
            tabloyedek.Columns.Add("Fiyat", typeof(double));
            dataGridView1.DataSource = tabloyedek;

            label19.Text = pad + " " + psoyad;
            dataGridView1.DataSource = baglanti.tablodoldurma("select * from urunlerviewp");
            dataGridView2.DataSource = baglanti.tablodoldurma("select * from musteri");
            dataGridView3.DataSource = baglanti.tablodoldurma("select * from urunlerviewp where \"STOK MİKTARI\">0");
            dataGridView4.DataSource = baglanti.tablodoldurma("select * from satısview");
            dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatısView");
            dataGridView1.ClearSelection();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {


            //Stok Sorgulama resetleme
            textBox1.Text = "";
            dataGridView1.DataSource = baglanti.tablodoldurma("select * from urunlerviewp");
            dataGridView1.ClearSelection();

            //Satış işlemleri resetleme
            smusteriidlbl.Text = "-";
            sadlbl.Text = "-";
            ssoyadlbl.Text = "-";
            stelefonlbl.Text = "-";
            sadreslbl.Text = "-";
            label16.Text = "0";
            label17.Text = "0";
            textBox4.Text = "";
            maskedTextBox1.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            dataGridView2.DataSource = baglanti.tablodoldurma("select * from musteri");
            dataGridView3.DataSource = baglanti.tablodoldurma("select * from urunlerviewp where \"STOK MİKTARI\">0");
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
            while (dataGridView6.Rows.Count > 0)
            {
                dataGridView6.Rows.RemoveAt(0);
            }
            uadetcombobox.Items.Clear();

            //Değişim işlemleri resetleme
            adlabel.Text = "-";
            soyadlabel.Text = "-";
            satisidlabel.Text = "-";
            barkodnolabel.Text = "-";
            gunlabel.Text = "-";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            maskedTextBox2.Text = "";
            dataGridView4.DataSource = baglanti.tablodoldurma("select * from satısview");
            dataGridView4.ClearSelection();
            dadetcombobox.Items.Clear();

            //iade işlemleri resetleme
            label32.Text = "-";
            label33.Text = "-";
            label35.Text = "-";
            label36.Text = "-";
            label37.Text = "-";
            textBox12.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            maskedTextBox3.Text = "";
            radioiade.Checked = false;
            radiokusurlu.Checked = false;
            dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatısView");
            dataGridView5.ClearSelection();
            iadetcombobox.Items.Clear();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                dataGridView1.DataSource = baglanti.tablodoldurma("select * from urunlerviewp WHERE " + "\"" + "BARKOD NUMARASI" + "\"" + " LIKE '" + Convert.ToInt64(textBox1.Text) + "%'");
                baglanti.baglantikapat();
            }
            else if (textBox1.Text == "")
            {
                dataGridView1.DataSource = baglanti.tablodoldurma("select * from urunlerviewp");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label14.Visible = true;
            label15.Visible = true;
            label17.Visible = true;
            textBox6.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label14.Visible = false;
            label15.Visible = false;
            label17.Visible = false;
            textBox6.Visible = false;
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.oku = baglanti.okuyucu("SELECT TELEFON FROM MUSTERI WHERE TELEFON='" + maskedTextBox1.Text + "'");
            if (this.oku.Read())
            {
                label48.Text = "numara kayıtlı";
                label48.ForeColor = Color.Red;
                musterikayitbtn.Enabled = false;
            }
            else
            {
                label48.Text = "numara kullanılabilir";
                label48.ForeColor = Color.Green;
                musterikayitbtn.Enabled = true;
            }
            baglanti.baglantikapat();

            if (maskedTextBox1.Text != "")
            {
                dataGridView2.DataSource = baglanti.tablodoldurma("select * from musteri WHERE telefon LIKE '" + Convert.ToInt64(maskedTextBox1.Text) + "%'");
            }
            else if (maskedTextBox1.Text == "")
            {
                dataGridView2.DataSource = baglanti.tablodoldurma("select * from musteri");
                label48.Text = "-";
            }


        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                dataGridView3.DataSource = baglanti.tablodoldurma("select * from urunlervıewp WHERE \"STOK MİKTARI\">0 and  \"BARKOD NUMARASI\" LIKE '" + Convert.ToInt64(textBox4.Text) + "%'");
                baglanti.baglantikapat();
                oku = baglanti.okuyucu("select URUNSATISFIYATI from urunler WHERE BARKODNO =" + Convert.ToInt64(textBox4.Text));
                if (oku.Read() == true)
                {
                    label16.Text = oku.GetString(0);
                    baglanti.baglantikapat();
                }
                else
                {
                    baglanti.baglantikapat();
                }
            }
            else if (textBox4.Text == "")
            {
                dataGridView3.DataSource = baglanti.tablodoldurma("select * from urunlerviewp WHERE \"STOK MİKTARI\">0");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            smusteriidlbl.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            sadlbl.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            ssoyadlbl.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            stelefonlbl.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            sadreslbl.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                double x = Convert.ToDouble(textBox6.Text);
                double y = Convert.ToDouble(label16.Text);
                label17.Text = (x - y).ToString();
            }
            else if (textBox6.Text == "")
            {
                textBox6.Text = "0";
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (textBox12.Text != "")
            {
                dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatisView where barkodno LIKE '" + Convert.ToInt64(textBox12.Text) + "%'");
            }
            else
            {
                dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatisView");
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void maskedTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void maskedTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox3.Text != "")
            {
                dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatisView where telefon LIKE '" + Convert.ToInt64(maskedTextBox3.Text) + "%'");
            }
            else
            {
                dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatisView");
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (textBox11.Text != "")
            {
                dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatisView where adı LIKE '" + textBox11.Text.ToString() + "%'");
            }
            else
            {
                dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatisView");
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text != "")
            {
                dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatisView where soyadı LIKE '" + textBox10.Text.ToString() + "%'");
            }
            else
            {
                dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatisView");
            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        string gunfarki;
        private void button1_Click(object sender, EventArgs e)
        {
            int kusurluurun;

            if (label35.Text == "-")
            {
                MessageBox.Show("tablodan ürün seçiniz!");
            }
            else if (Convert.ToInt32(gunfarki) > 15)
            {
                MessageBox.Show("satın alma işlemi 15 günü geçtiği için işlem yapılamaz");
            }
            else if (iadetcombobox.Text.ToString() == "")
            {
                MessageBox.Show("adet seçimi yapınız");
            }
            else
            {
                if (radiokusurlu.Checked == true)
                {
                    this.oku = baglanti.okuyucu("select kusurluurunsayısı from kusurluurunler where barkodno=" + Convert.ToInt64(label36.Text));
                    if (this.oku.Read() == true)
                    {
                        kusurluurun = Convert.ToInt32(oku.GetString(0)) + Convert.ToInt32(iadetcombobox.SelectedItem.ToString());

                        baglanti.baglantikapat();
                        baglanti.komutCalıstır("UPDATE kusurluurunler set kusurluurunsayısı=" + kusurluurun + " where barkodno=" + Convert.ToInt64(label36.Text));
                        if (Convert.ToInt32(dataGridView5.CurrentRow.Cells[6].Value.ToString()) == Convert.ToInt32(iadetcombobox.SelectedItem.ToString()))
                        {
                            baglanti.komutCalıstır("delete from satis where satısıd=" + Convert.ToInt64(label35.Text));
                        }
                        else
                        {
                            baglanti.komutCalıstır("UPDATE SATIS SET ADET=" + (Convert.ToInt32(dataGridView5.CurrentRow.Cells[6].Value.ToString()) - Convert.ToInt32(iadetcombobox.SelectedItem.ToString())) + " WHERE SATISID=" + label35.Text);
                        }
                        dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatısView");
                        dataGridView5.ClearSelection();
                        iadetcombobox.Items.Clear();
                        tabControl1_SelectedIndexChanged(null, null);
                    }
                    else if (this.oku.Read() == false)
                    {
                        baglanti.baglantikapat();
                        baglanti.komutCalıstır("INSERT INTO kusurluurunler (barkodno,urunadı,urungelısfıyatı,urunsatısfıyatı,kategorııd,renk,beden,kusurluurunsayısı) select barkodno, urunadı, urungelısfıyatı, urunsatısfıyatı, kategorııd, renk, beden," + iadetcombobox.Text + " from urunler where barkodno = " + Convert.ToInt64(label36.Text));
                        if (Convert.ToInt32(dataGridView5.CurrentRow.Cells[6].Value.ToString()) == Convert.ToInt32(iadetcombobox.SelectedItem.ToString()))
                        {
                            baglanti.komutCalıstır("delete from satis where satısıd=" + Convert.ToInt64(label35.Text));
                        }
                        else
                        {
                            baglanti.komutCalıstır("UPDATE SATIS SET ADET=" + (Convert.ToInt32(dataGridView5.CurrentRow.Cells[6].Value.ToString()) - Convert.ToInt32(iadetcombobox.SelectedItem.ToString())) + " WHERE SATISID=" + label35.Text);
                        }
                        dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatısView");
                        dataGridView5.ClearSelection();
                        iadetcombobox.Items.Clear();
                        tabControl1_SelectedIndexChanged(null, null);
                    }

                }
                else if (radioiade.Checked == true)
                {

                    this.oku = baglanti.okuyucu("select stoksayısı from urunler where barkodno=" + Convert.ToInt64(label36.Text));
                    oku.Read();
                    long stoksayisi = Convert.ToInt64(oku.GetString(0)) + Convert.ToInt32(iadetcombobox.SelectedItem.ToString());
                    baglanti.baglantikapat();
                    baglanti.komutCalıstır("update urunler set stoksayısı=" + stoksayisi + "where barkodno=" + Convert.ToInt64(label36.Text));
                    if (Convert.ToInt32(dataGridView5.CurrentRow.Cells[6].Value.ToString()) == Convert.ToInt32(iadetcombobox.SelectedItem.ToString()))
                    {
                        baglanti.komutCalıstır("delete from satis where satısıd=" + Convert.ToInt64(label35.Text));
                    }
                    else
                    {
                        baglanti.komutCalıstır("UPDATE SATIS SET ADET=" + (Convert.ToInt32(dataGridView5.CurrentRow.Cells[6].Value.ToString()) - Convert.ToInt32(iadetcombobox.SelectedItem.ToString())) + " WHERE SATISID=" + label35.Text);
                    }
                    dataGridView5.DataSource = baglanti.tablodoldurma("select * from SatısView");
                    dataGridView5.ClearSelection();
                    iadetcombobox.Items.Clear();
                    tabControl1_SelectedIndexChanged(null, null);
                }
                else
                {
                    MessageBox.Show("kusurlu veya normal iade seçimi yapınız.");
                }


            }

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            iadetcombobox.Items.Clear();
            for (int i = 1; i <= Convert.ToInt32(dataGridView5.CurrentRow.Cells[6].Value.ToString()); i++)
            {
                iadetcombobox.Items.Add(i);
            }
            label32.Text = dataGridView5.CurrentRow.Cells[1].Value.ToString();
            label33.Text = dataGridView5.CurrentRow.Cells[2].Value.ToString();
            label35.Text = dataGridView5.CurrentRow.Cells[0].Value.ToString();
            label36.Text = dataGridView5.CurrentRow.Cells[4].Value.ToString();
            DateTime bugunkutarih = DateTime.Today;
            DateTime tablodakitarih = Convert.ToDateTime(dataGridView5.CurrentRow.Cells[8].Value.ToString());
            TimeSpan Sonuc = bugunkutarih - tablodakitarih;
            gunfarki = Sonuc.TotalDays.ToString();
            label37.Text = "Ürün " + Sonuc.TotalDays.ToString() + " gün önce satın alınmış";

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                dataGridView4.DataSource = baglanti.tablodoldurma("select * from satısview where  barkodno LIKE'" + textBox7.Text + "%'");
            }
            else
            {
                dataGridView4.DataSource = baglanti.tablodoldurma("select * from SatisView");
            }
        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox2.Text != "")
            {
                dataGridView4.DataSource = baglanti.tablodoldurma("select * from SatisView where telefon LIKE '" + Convert.ToInt64(maskedTextBox2.Text) + "%'");
            }
            else
            {
                dataGridView4.DataSource = baglanti.tablodoldurma("select * from SatisView");
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text != "")
            {
                dataGridView4.DataSource = baglanti.tablodoldurma("select * from SatisView where ad LIKE '" + textBox9.Text.ToString() + "%'");
            }
            else
            {
                dataGridView4.DataSource = baglanti.tablodoldurma("select * from SatisView");
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                dataGridView4.DataSource = baglanti.tablodoldurma("select * from SatisView where soyad LIKE '" + textBox8.Text.ToString() + "%'");
            }
            else
            {
                dataGridView4.DataSource = baglanti.tablodoldurma("select * from SatisView");
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void maskedTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        public int minimumadetbul(int satilan, int urunstok)
        {
            if (satilan > urunstok)
            {
                return urunstok;
            }
            else if (satilan < urunstok)
            {
                return satilan;
            }
            else
            {
                return satilan;
            }

        }
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.oku = baglanti.okuyucu("select STOKSAYISI FROM URUNLER WHERE BARKODNO=" + dataGridView4.CurrentRow.Cells[4].Value.ToString());
            this.oku.Read();
            int adet = minimumadetbul(Convert.ToInt32(dataGridView4.CurrentRow.Cells[6].Value.ToString()), Convert.ToInt32(this.oku.GetString(0)));
            baglanti.baglantikapat();
            dadetcombobox.Items.Clear();
            for (int i = 1; i <= adet; i++)
            {
                dadetcombobox.Items.Add(i);
            }
            adlabel.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();
            soyadlabel.Text = dataGridView4.CurrentRow.Cells[2].Value.ToString();
            satisidlabel.Text = dataGridView4.CurrentRow.Cells[0].Value.ToString();
            barkodnolabel.Text = dataGridView4.CurrentRow.Cells[4].Value.ToString();
            DateTime bugunkutarih = DateTime.Today;
            DateTime tablodakitarih = Convert.ToDateTime(dataGridView4.CurrentRow.Cells[8].Value.ToString());
            TimeSpan Sonuc = bugunkutarih - tablodakitarih;
            gunfarki = Sonuc.TotalDays.ToString();
            gunlabel.Text = "Ürün " + Sonuc.TotalDays.ToString() + " gün önce satın alınmış";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (satisidlabel.Text == "-")
            {
                MessageBox.Show("tablodan ürün seçiniz!");
            }
            else if (dadetcombobox.Text.ToString() == "")
            {
                MessageBox.Show("adet seçimi yapınız");
            }
            else if (Convert.ToInt32(gunfarki) > 15)
            {
                MessageBox.Show("satın alma işlemi 15 günü geçtiği için işlem yapılamaz");
            }
            else
            {

                this.oku = baglanti.okuyucu("select stoksayısı from urunler where barkodno=" + barkodnolabel.Text);
                this.oku.Read();
                long stok = Convert.ToInt64(oku.GetString(0));
                baglanti.baglantikapat();
                baglanti.komutCalıstır("update urunler set stoksayısı=" + (stok - Convert.ToInt32(dadetcombobox.SelectedItem.ToString())) + " WHERE barkodno=" + barkodnolabel.Text);
                this.oku = baglanti.okuyucu("select barkodno,KUSURLUURUNSAYISI from kusurluurunler where barkodno=" + barkodnolabel.Text);
                if (this.oku.Read())
                {
                    int kusurluurunsayi = Convert.ToInt32(this.oku.GetString(1)) + Convert.ToInt32(dadetcombobox.SelectedItem.ToString());

                    baglanti.baglantikapat();
                    baglanti.komutCalıstır("UPDATE KUSURLUURUNLER SET KUSURLUURUNSAYISI=" + kusurluurunsayi + "where barkodno=" + barkodnolabel.Text);

                }
                else
                {
                    baglanti.baglantikapat();
                    baglanti.komutCalıstır("INSERT INTO KUSURLUURUNLER SELECT BARKODNO,URUNADI,URUNGELISFIYATI,URUNSATISFIYATI,KATEGORIID,RENK,BEDEN," + dadetcombobox.Text + " FROM URUNLER WHERE BARKODNO=" + barkodnolabel.Text);
                }

                dataGridView4.DataSource = baglanti.tablodoldurma("select * from satısview");
                MessageBox.Show("Değişim İşlemi başarılı, Yeni ürün verilmiştir ve kusurlu ürün iadeye gönderildi.");
                this.oku = baglanti.okuyucu("select STOKSAYISI FROM URUNLER WHERE BARKODNO=" + barkodnolabel.Text);
                this.oku.Read();
                int a1 = Convert.ToInt32(this.oku.GetString(0));
                baglanti.baglantikapat();
                tabControl1_SelectedIndexChanged(null, null);
                int adet = minimumadetbul(Convert.ToInt32(dataGridView4.CurrentRow.Cells[6].Value.ToString()), a1);

                dadetcombobox.Items.Clear();
                for (int i = 1; i <= adet; i++)
                {
                    dadetcombobox.Items.Add(i);
                }


            }
        }

        private void musterikayitbtn_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Tüm Bilgileri Doldurunuz!");
            }
            else
            {
                MessageBox.Show(baglanti.prosedurMusteriIslem("kayit", "1", "" + textBox2.Text + "", "" + textBox3.Text + "", "" + maskedTextBox1.Text + "", "" + textBox5.Text + ""));
                dataGridView2.DataSource = baglanti.tablodoldurma("select * from musteri");
            }
        }

        private void ueklebtn_Click(object sender, EventArgs e)
        {

            double fiyat;
            if (uadetcombobox.SelectedItem == null)//ürün adet seçimi kontrolü
            {
                MessageBox.Show("adet seçimi yapınız");
            }
            else
            {
                fiyat = Convert.ToDouble(uadetcombobox.SelectedItem.ToString()) * Convert.ToDouble(dataGridView3.CurrentRow.Cells[5].Value.ToString());//combobox ürün*adet sayısı toplam fiyatı

                int yerbul = -1;//satış tablosundan ürün önceden eklendiyse bulmak için
                for (int i = 0; i < dataGridView6.Rows.Count; i++)
                {
                    if (Convert.ToInt64(dataGridView3.CurrentRow.Cells[0].Value.ToString()) == Convert.ToInt64(dataGridView6.Rows[i].Cells[0].Value.ToString()))
                    {
                        yerbul = i;
                    }
                }
                if (yerbul != -1)//ürün bulunduysa adet sayısı ve fiyat güncellemesi yapılır.
                {
                    int adetarttir = Convert.ToInt32(dataGridView6.Rows[yerbul].Cells[3].Value.ToString()) + Convert.ToInt32(uadetcombobox.SelectedItem.ToString());
                    dataGridView6.Rows[yerbul].Cells[3].Value = adetarttir.ToString();
                    double fiyatarttir = Convert.ToDouble(dataGridView6.Rows[yerbul].Cells[4].Value.ToString()) + fiyat;
                    dataGridView6.Rows[yerbul].Cells[4].Value = fiyatarttir.ToString();
                    ucikarbtn.Enabled = true;
                }
                else//ürün ilk defa ekleniyorsa
                {
                    tabloyedek.Rows.Add(dataGridView3.CurrentRow.Cells[0].Value.ToString(), dataGridView3.CurrentRow.Cells[2].Value.ToString(), dataGridView3.CurrentRow.Cells[4].Value.ToString(), uadetcombobox.SelectedItem.ToString(), fiyat.ToString());
                    dataGridView6.DataSource = tabloyedek;
                    ucikarbtn.Enabled = true;
                }
            }

            //aşağıdaki kısım combobox sayısını güncellemek içindir.
            this.oku = baglanti.okuyucu("select STOKSAYISI,barkodno from urunler WHERE barkodno=" + dataGridView3.CurrentRow.Cells[0].Value.ToString());
            this.oku.Read();
            int stoksayi = Convert.ToInt32(oku.GetString(0));
            long barkod = Convert.ToInt64(oku.GetString(1));
            baglanti.baglantikapat();

            for (int i = 0; i < dataGridView6.Rows.Count; i++)
            {
                if (barkod == Convert.ToInt64(dataGridView6.Rows[i].Cells[0].Value.ToString()))
                {
                    stoksayi -= Convert.ToInt32(dataGridView6.Rows[i].Cells[3].Value.ToString());
                }
            }
            uadetcombobox.Items.Clear();

            if (stoksayi > 10)
            {
                stoksayi = 10;
            }

            for (int i = 1; i <= stoksayi; i++)
            {
                uadetcombobox.Items.Add(i);
            }
            int toplam = 0;
            for (int i = 0; i < dataGridView6.Rows.Count; i++)
            {
                toplam += Convert.ToInt32(dataGridView6.Rows[i].Cells[4].Value.ToString());
            }
            label16.Text = toplam.ToString();

        }

        private void ucikarbtn_Click(object sender, EventArgs e)
        {
            if (dataGridView6.CurrentRow.Cells[0].Value.ToString() != "")
            {
                dataGridView6.Rows[0].Selected = true;

                label16.Text = (Convert.ToInt32(label16.Text) - Convert.ToInt32(dataGridView6.CurrentRow.Cells[4].Value.ToString())).ToString();
                dataGridView6.Rows.RemoveAt(dataGridView6.SelectedRows[0].Index);

                //aşağıdaki kısım combobox sayısını güncellemek içindir.
                this.oku = baglanti.okuyucu("select STOKSAYISI,barkodno from urunler WHERE barkodno=" + dataGridView3.CurrentRow.Cells[0].Value.ToString());
                this.oku.Read();
                int stoksayi = Convert.ToInt32(oku.GetString(0));
                long barkod = Convert.ToInt64(oku.GetString(1));
                baglanti.baglantikapat();

                uadetcombobox.Items.Clear();

                if (stoksayi > 10)
                {
                    stoksayi = 10;
                }

                for (int i = 1; i <= stoksayi; i++)
                {
                    uadetcombobox.Items.Add(i);
                }
                if (dataGridView6.Rows.Count == 0)
                {
                    ucikarbtn.Enabled = false;
                }
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.oku = baglanti.okuyucu("select STOKSAYISI,barkodno from urunler WHERE barkodno=" + dataGridView3.CurrentRow.Cells[0].Value.ToString());//tablodan seçilen ürünün stoksayısı alınır
            this.oku.Read();
            int stoksayi = Convert.ToInt32(oku.GetString(0));
            long barkod = Convert.ToInt64(oku.GetString(1));
            baglanti.baglantikapat();

            for (int i = 0; i < dataGridView6.Rows.Count; i++)//aynı üründen tekrar eklemek istenilirse combobox içerisine kalan stok sayısı kadar ürün sayısı eklenir.
            {
                if (barkod == Convert.ToInt64(dataGridView6.Rows[i].Cells[0].Value.ToString()))//
                {
                    stoksayi -= Convert.ToInt32(dataGridView6.Rows[i].Cells[3].Value.ToString());//
                }
            }
            uadetcombobox.Items.Clear();//

            if (stoksayi > 10)// combobox uzun olmaması için
            {
                stoksayi = 10;
            }

            for (int i = 1; i <= stoksayi; i++)
            {
                uadetcombobox.Items.Add(i);
            }

        }

        private void satis_Click(object sender, EventArgs e)
        {
            int parafark = Convert.ToInt32(textBox6.Text) - Convert.ToInt32(label16.Text);
            if (dataGridView6.Rows.Count == 0)
            {
                MessageBox.Show("Satış tablosunda ürün yok");
            }
            else if (smusteriidlbl.Text == "-")
            {
                MessageBox.Show("Müşteri Seçiniz");
            }
            else if (parafark < 0)
            {
                MessageBox.Show("ödenen ücret yetersiz");
            }
            else
            {
                for (int i = 0; i < dataGridView6.Rows.Count; i++)
                {
                    this.oku = baglanti.okuyucu("SELECT (URUNSATISFIYATI-URUNGELISFIYATI) FROM URUNLER WHERE BARKODNO=" + dataGridView6.Rows[i].Cells[0].Value.ToString());
                    this.oku.Read();
                    int adet = Convert.ToInt32(dataGridView6.Rows[i].Cells[3].Value.ToString());
                    double kar = Convert.ToDouble(this.oku.GetString(0)) * adet;
                    baglanti.baglantikapat();
                    baglanti.komutCalıstır("INSERT INTO SATIS (MUSTERIID,BARKODNO,PERSONELID,TARIH,NETKAR,ADET) VALUES (" + smusteriidlbl.Text + "," + dataGridView6.Rows[i].Cells[0].Value.ToString() + "," + pid + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "'," + kar + "," + adet + ")");

                    this.oku = baglanti.okuyucu("SELECT  STOKSAYISI FROM URUNLER WHERE BARKODNO=" + dataGridView6.Rows[i].Cells[0].Value.ToString());
                    this.oku.Read();
                    int stokazalt = Convert.ToInt32(this.oku.GetString(0)) - adet;
                    baglanti.baglantikapat();

                    baglanti.komutCalıstır("UPDATE URUNLER SET STOKSAYISI=" + stokazalt + " WHERE BARKODNO=" + dataGridView6.Rows[i].Cells[0].Value.ToString());
                }
                MessageBox.Show("satış yapıldı");
                tabControl1_SelectedIndexChanged(null, null);

            }

        }
    }
}

