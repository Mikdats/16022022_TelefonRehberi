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
using System.Configuration;

namespace _16022022_TelefonRehberi
{
    public partial class RehberForm : Form
    {
        public RehberForm()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ConnectionString);
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut =new SqlCommand("RehberEkle", baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@Isim", txtisim.Text);
            komut.Parameters.AddWithValue("@Soyisim", txtsoyisim.Text);
            komut.Parameters.AddWithValue("@Telefon1", txttel1.Text);
            komut.Parameters.AddWithValue("@Telefon2", txttel2.Text);
            komut.Parameters.AddWithValue("@Email", txtemail.Text);
            komut.Parameters.AddWithValue("@Webadres", txtweb.Text);
            komut.Parameters.AddWithValue("@Adres", txtadres.Text);
            komut.Parameters.AddWithValue("@Aciklama", txtaciklama.Text);

            baglanti.Open();
            int sonuc=komut.ExecuteNonQuery();
            baglanti.Close();

            if (sonuc > 0)
            {
                MessageBox.Show("Kayıt başarılı bir şekilde eklendi.");
                RehberListele();
            }
            else
                MessageBox.Show("Kayıt eklenirken hata oluştu.");
        }

        private void RehberForm_Load(object sender, EventArgs e)
        {
            RehberListele();

        }

        private void RehberListele()
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "Select * from Rehber";
            komut.Connection = baglanti;
            baglanti.Open();
            SqlDataReader rdr = komut.ExecuteReader();

            List<Rehber> rehberlistesi = new List<Rehber>();

            // Rehber r = new Rehber() {Isim = "elif",Soyisim ="cengiz"};
            //rehberlistesi.Add(r);

            while (rdr.Read())
            {
                rehberlistesi.Add(new Rehber()
                {
                    ID = rdr.GetInt32(0),
                    Isim = rdr.GetString(1),
                    Soyisim = rdr.GetString(2),
                    Telefon1 = rdr.GetString(3),
                    Telefon2 = rdr.GetString(4),
                    Email = rdr.GetString(5),
                    Webadres = rdr.GetString(6),
                    Adres = rdr.GetString(7),
                    Aciklama = rdr.GetString(8)
                });
            }
            baglanti.Close();
            lst_Rehber.DataSource = rehberlistesi;
        }

        private void lst_Rehber_Click(object sender, EventArgs e)
        {
            Rehber r = (Rehber)lst_Rehber.SelectedItem;

            txt_secisim.Text = r.Isim;
            txt_secsoyisim.Text = r.Soyisim;
            txt_sectel1.Text = r.Telefon1;
            txt_sectel2.Text = r.Telefon2;
            txt_secAdres.Text = r.Adres;
            txt_secemail.Text = r.Email;
            txt_secweb.Text = r.Webadres;
            txt_secaciklama.Text = r.Aciklama;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int id = ((Rehber)lst_Rehber.SelectedItem).ID;
            SqlCommand komut = new SqlCommand("RehberGuncelle", baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@ID", id);
            komut.Parameters.AddWithValue("@Isim", txt_secisim.Text);
            komut.Parameters.AddWithValue("@Soyisim", txt_secsoyisim.Text);
            komut.Parameters.AddWithValue("@Telefon1", txt_sectel1.Text);
            komut.Parameters.AddWithValue("@Telefon2", txt_sectel2.Text);
            komut.Parameters.AddWithValue("@Email", txt_secemail.Text);
            komut.Parameters.AddWithValue("@Webadres", txt_secweb.Text);
            komut.Parameters.AddWithValue("@Adres", txt_secAdres.Text);
            komut.Parameters.AddWithValue("@Aciklama", txt_secaciklama.Text);
            int sonuc=0;

            try
            {
                baglanti.Open();
                sonuc = komut.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata:" + ex);
            }
            finally
            {
                baglanti.Close();
            }
        
            if (sonuc>0)
            {
                MessageBox.Show("Kayıt Güncellendi");
                RehberListele();

            }
            else
            {
                MessageBox.Show("Kayıt Güncellenemedi!!");
            }


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int id = ((Rehber)lst_Rehber.SelectedItem).ID;
            SqlCommand komut = new SqlCommand("RehberSil", baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@ID", id);
            baglanti.Open();
            int sonuc = komut.ExecuteNonQuery();
            baglanti.Close();

            if (sonuc>0)
            {
                MessageBox.Show("Kayıt Silindi");
                txt_secisim.Text = "";
                txt_secsoyisim.Text = "";
                txt_secAdres.Text = "";
                txt_sectel1.Text = "";
                txt_sectel2.Text = "";
                txt_secweb.Text = "";
                txt_secemail.Text = "";
                txt_secaciklama.Text = "";
                RehberListele();
            }
            else
            {
                MessageBox.Show("Kayıt silinirken hata oluştu");
            }

        }
    }
}
