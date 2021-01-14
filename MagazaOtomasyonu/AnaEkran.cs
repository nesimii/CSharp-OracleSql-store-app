using System;
using System.Drawing;
using System.Windows.Forms;

namespace MagazaOtomasyonu
{
    public partial class AnaEkran : Form
    {
        public AnaEkran()
        {

            InitializeComponent();

        }

        private void AnaEkran_Load(object sender, EventArgs e)
        {
            int wit = Screen.PrimaryScreen.WorkingArea.Width;
            int heid = Screen.PrimaryScreen.Bounds.Height;
            this.Size = new Size((int)(wit * 0.8), (int)(heid * 0.8));
            this.Location = new Point((int)(wit - wit * 0.8) / 2, (int)(heid - heid * 0.8) / 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PersonelGiris pgiris1 = new PersonelGiris();
            pgiris1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YoneticiGiris ygiris1 = new YoneticiGiris();
            ygiris1.Show();
            this.Hide();
        }
    }
}
