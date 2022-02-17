using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16022022_TelefonRehberi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("server=localhost;database=TelefonRehberi;user=sa;password=12345");
        
        private void btnGiriş_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select count(*) from Kullanici where KullaniciAdi = @ad and Sifre = @sifre", baglanti);
            komut.Parameters.AddWithValue("@ad", txtKulAd.Text);
            komut.Parameters.AddWithValue("@sifre", txtSifre.Text);
            baglanti.Open();
            int sayi=(int)komut.ExecuteScalar();
            baglanti.Close();

            if (sayi > 0)
            {
                RehberForm r = new RehberForm();
                r.Show();             
            }
            else
                MessageBox.Show("Hatalı kullanıcı adı veya şifre", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }
    }
}
