using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MagazaOtomasyonu
{
    class Baglanti
    {
        OracleConnection baglanti = new OracleConnection("Data Source=localhost:1521/XEPDB1; User Id=SYSTEM;password=admin;");
        OracleCommand komut = new OracleCommand();
        OracleDataAdapter adaptor = new OracleDataAdapter();
        OracleDataReader oku;
        public string yoneticiid, yoneticiisim, yoneticisoyisim;
        public void baglantikapat()
        {
            baglanti.Close();
        }
        public DataTable tablodoldurma(string komut)
        {

            DataTable tablo = new DataTable();
            this.baglanti.Open();
            this.komut.CommandType = CommandType.Text;
            this.komut.CommandText = komut;
            this.komut.Connection = baglanti;
            adaptor.SelectCommand = this.komut;
            adaptor.Fill(tablo);
            this.baglanti.Close();
            return tablo;
        }
        public OracleDataReader okuyucu(string komut)
        {
            this.baglanti.Open();
            this.komut.Connection = baglanti;
            this.komut.CommandType = CommandType.Text;
            this.komut.CommandText = komut;
            this.oku = this.komut.ExecuteReader();
            return this.oku;

        }
        public void komutCalıstır(string komut)
        {
            this.baglanti.Open();
            this.komut.CommandType = CommandType.Text;
            this.komut.Connection = baglanti;
            this.komut.CommandText = komut;
            this.komut.ExecuteNonQuery();
            this.baglanti.Close();
        }



        public string prosedurYoneticiGiris(string kullaniciadi, string pass)
        {
            string cevap;

            this.komut.CommandText = "YONETICIGIRIS";//stored procedure nin adı yazılır.
            this.komut.Connection = baglanti;
            this.komut.CommandType = CommandType.StoredProcedure;//komut tipi belirtilir.

            this.komut.Parameters.Clear();// her işlem yapıldığında kayıtlı parametreleri temizler.

            this.komut.Parameters.Add("KULLANICIADI", OracleDbType.Varchar2, 2000).Value = kullaniciadi;//parametre eklemesi yapılır
            this.komut.Parameters["KULLANICIADI"].Direction = ParameterDirection.Input;//parametre giriş-çıkış olduğu belirlenir.

            this.komut.Parameters.Add("SIFRE", OracleDbType.Varchar2, 2000).Value = pass;//parametre eklemesi yapılır
            this.komut.Parameters["SIFRE"].Direction = ParameterDirection.Input;//parametre giriş-çıkış olduğu belirlenir.

            this.komut.Parameters.Add("id", OracleDbType.Decimal, 20).Direction = ParameterDirection.Output;//geri dönecek değer
            this.komut.Parameters.Add("isim", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;//geri dönecek değer
            this.komut.Parameters.Add("soyisim", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;//geri dönecek değer
            this.komut.Parameters.Add("cevap", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;//geri dönecek değer

            baglanti.Open();
            this.komut.ExecuteNonQuery();
            yoneticiid = this.komut.Parameters["id"].Value.ToString();
            yoneticiisim = this.komut.Parameters["isim"].Value.ToString();
            yoneticisoyisim = this.komut.Parameters["soyisim"].Value.ToString();
            cevap = this.komut.Parameters["cevap"].Value.ToString();
            baglanti.Close();
            return cevap;
        }

        public string prosedurPersonelIslem(string islem, string id, string ad, string soyad, string tel, string adres, string maas, string kulid, string sifre)
        {
            string cevap;

            this.komut.CommandText = "PERSONELISLEM";
            this.komut.Connection = baglanti;
            this.komut.CommandType = CommandType.StoredProcedure;//komut tipi belirtilir.
            this.komut.Parameters.Clear();

            this.komut.Parameters.Add("islem", OracleDbType.Varchar2, 2000).Value = islem;//parametre eklemesi yapılır
            this.komut.Parameters["islem"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("pID", OracleDbType.Decimal, 2000).Value = id;//parametre eklemesi yapılır
            this.komut.Parameters["pID"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("name", OracleDbType.Varchar2, 2000).Value = ad;//parametre eklemesi yapılır
            this.komut.Parameters["name"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("surname", OracleDbType.Varchar2, 2000).Value = soyad;//parametre eklemesi yapılır
            this.komut.Parameters["surname"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("tel", OracleDbType.Decimal, 2000).Value = tel;//parametre eklemesi yapılır
            this.komut.Parameters["tel"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("adress", OracleDbType.Varchar2, 2000).Value = adres;//parametre eklemesi yapılır
            this.komut.Parameters["adress"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("salary", OracleDbType.Decimal, 2000).Value = maas;//parametre eklemesi yapılır
            this.komut.Parameters["salary"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("nickname", OracleDbType.Varchar2, 2000).Value = kulid;//parametre eklemesi yapılır
            this.komut.Parameters["nickname"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("pass", OracleDbType.Varchar2, 2000).Value = sifre;//parametre eklemesi yapılır
            this.komut.Parameters["pass"].Direction = ParameterDirection.Input;

            this.komut.Parameters.Add("cevap", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;//Geri dönecek değer

            baglanti.Open();
            this.komut.ExecuteNonQuery();
            cevap = this.komut.Parameters["cevap"].Value.ToString();
            baglanti.Close();
            return cevap;
        }
        public string prosedurMusteriIslem(string islem, string id, string ad, string soyad, string tel, string adres)
        {
            string cevap;

            this.komut.CommandText = "MUSTERIISLEM";
            this.komut.Connection = baglanti;
            this.komut.CommandType = CommandType.StoredProcedure;//komut tipi belirtilir.
            this.komut.Parameters.Clear();

            this.komut.Parameters.Add("islem", OracleDbType.Varchar2, 2000).Value = islem;//parametre eklemesi yapılır
            this.komut.Parameters["islem"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("mID", OracleDbType.Decimal, 2000).Value = id;//parametre eklemesi yapılır
            this.komut.Parameters["mID"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("name", OracleDbType.Varchar2, 2000).Value = ad;//parametre eklemesi yapılır
            this.komut.Parameters["name"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("surname", OracleDbType.Varchar2, 2000).Value = soyad;//parametre eklemesi yapılır
            this.komut.Parameters["surname"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("tel", OracleDbType.Decimal, 2000).Value = tel;//parametre eklemesi yapılır
            this.komut.Parameters["tel"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("adress", OracleDbType.Varchar2, 2000).Value = adres;//parametre eklemesi yapılır
            this.komut.Parameters["adress"].Direction = ParameterDirection.Input;

            this.komut.Parameters.Add("cevap", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;//Geri dönecek değer

            baglanti.Open();
            this.komut.ExecuteNonQuery();
            cevap = this.komut.Parameters["cevap"].Value.ToString();
            baglanti.Close();
            return cevap;
        }
        public string prosedurKategoriIslem(string islem, string kategoriname)
        {
            string cevap;
            this.komut.CommandText = "KATEGORIISLEM";
            this.komut.Connection = baglanti;
            this.komut.CommandType = CommandType.StoredProcedure;//komut tipi belirtilir.
            this.komut.Parameters.Clear();

            this.komut.Parameters.Add("islem", OracleDbType.Varchar2, 2000).Value = islem;//parametre eklemesi yapılır
            this.komut.Parameters["islem"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("kategoriname", OracleDbType.Varchar2, 2000).Value = kategoriname;//parametre eklemesi yapılır
            this.komut.Parameters["kategoriname"].Direction = ParameterDirection.Input;

            this.komut.Parameters.Add("cevap", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;//Geri dönecek değer
            baglanti.Open();
            this.komut.ExecuteNonQuery();
            cevap = this.komut.Parameters["cevap"].Value.ToString();
            baglanti.Close();
            return cevap;
        }
        public string prosedurUrunIslem(string islem, string barkod, string kategoriadi, string urunadi, string gelisfiyat, string satisfiyat, string renk, string beden, string stoksayi)
        {
            string cevap;
            this.komut.CommandText = "URUNISLEM";
            this.komut.Connection = baglanti;
            this.komut.CommandType = CommandType.StoredProcedure;//komut tipi belirtilir.
            this.komut.Parameters.Clear();

            this.komut.Parameters.Add("islem", OracleDbType.Varchar2, 2000).Value = islem;//parametre eklemesi yapılır
            this.komut.Parameters["islem"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("barkod", OracleDbType.Decimal, 2000).Value = barkod;//parametre eklemesi yapılır
            this.komut.Parameters["barkod"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("kategoriname", OracleDbType.Varchar2, 2000).Value = kategoriadi;//parametre eklemesi yapılır
            this.komut.Parameters["kategoriname"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("urunad", OracleDbType.Varchar2, 2000).Value = urunadi;//parametre eklemesi yapılır
            this.komut.Parameters["urunad"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("gelisfiyat", OracleDbType.Decimal, 2000).Value = gelisfiyat;//parametre eklemesi yapılır
            this.komut.Parameters["gelisfiyat"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("satisfiyat", OracleDbType.Decimal, 2000).Value = satisfiyat;//parametre eklemesi yapılır
            this.komut.Parameters["satisfiyat"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("renkk", OracleDbType.Varchar2, 2000).Value = renk;//parametre eklemesi yapılır
            this.komut.Parameters["renkk"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("bedenn", OracleDbType.Varchar2, 2000).Value = beden;//parametre eklemesi yapılır
            this.komut.Parameters["bedenn"].Direction = ParameterDirection.Input;
            this.komut.Parameters.Add("stoksayi", OracleDbType.Decimal, 2000).Value = stoksayi;//parametre eklemesi yapılır
            this.komut.Parameters["stoksayi"].Direction = ParameterDirection.Input;

            this.komut.Parameters.Add("cevap", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;//Geri dönecek değer
            baglanti.Open();
            this.komut.ExecuteNonQuery();
            cevap = this.komut.Parameters["cevap"].Value.ToString();
            baglanti.Close();
            return cevap;
        }

    }
}
