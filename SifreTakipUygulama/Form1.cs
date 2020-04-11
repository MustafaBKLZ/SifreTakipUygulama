using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SifreTakipUygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        int[] zorluk;
        private void btn_olustur_Click(object sender, EventArgs e)
        {
            btn_kaydet.Enabled = true;
            txt_olusan_sfire.Text = "";
            int sifre;
            string karakter = "";

            //zorluk = new int[] { 33, 47 };// ASCII kod tablosundaki özel karakter kodları #1
            //zorluk = new int[] { 48, 57 };// ASCII kod tablosundaki rakam karakter kodları
            //zorluk = new int[] { 58, 64 };// ASCII kod tablosundaki özel karakter kodları #2
            //zorluk = new int[] { 65, 90 };// ASCII kod tablosundaki Büyük harf karakter kodları
            //zorluk = new int[] { 91, 96 };// ASCII kod tablosundaki özel karakter kodları #3
            //zorluk = new int[] { 97, 122 };// ASCII kod tablosundaki Küçük harf karakter kodları


            Random rastgele = new Random();
            if (cmb_zorluk.SelectedIndex >= 0)
            {
                switch (cmb_zorluk.SelectedIndex)
                {
                    //Sadece Büyük Harf
                    case 0: zorluk = new int[] { 65, 90 }; break;
                    //Sadece Küçük Harf
                    case 1: zorluk = new int[] { 97, 122 }; break;
                    //Sadece Rakam
                    case 2: zorluk = new int[] { 48, 57 }; break;
                    //Rakam ve Özel Karakterler
                    case 3: zorluk = new int[] { 33, 64 }; break;
                    //Harfler Özel Karakterler
                    case 4: zorluk = new int[] { 58, 122 }; break;
                    //Tamamı Karışık
                    case 5: zorluk = new int[] { 33, 122 }; break;
                }

                for (int i = 0; i < nud_sifre_karakter_adet.Value; i++)
                {
                    sifre = rastgele.Next(zorluk[0], zorluk[1]); // ASCII tablosundan rastgele bir karakter seçiyoruz.
                    karakter += Convert.ToChar(sifre).ToString(); // Rastgele seçilmiş olan sayıyı harf ve özel karakterlere çeviriyoruz.
                }
                if (chk_sayi_harf_donusumleri.CheckState == CheckState.Checked)
                    txt_olusan_sfire.Text = Sayi_Harf_Donusumleri(karakter);
                else
                    txt_olusan_sfire.Text = karakter;
            }
            else
            {
                MessageBox.Show("Zorluk seçmelisiniz.");
            }
        }

        private void btn_olustur_ozel_Click(object sender, EventArgs e)
        {
            btn_kaydet.Enabled = true;
            string cumle = txt_cumle.Text;
            string[] parcalar = cumle.Split(' ');
            string sifre = "", karakter = "";
            int random_Secim = 0;
            Random rastgele = new Random();
            zorluk = new int[] { 33, 47 };// ASCII kod tablosundaki özel karakter kodları #1

            for (int i = 0; i < parcalar.Count(); i++)
            {
                random_Secim = rastgele.Next(zorluk[0], zorluk[1]);
                karakter = Convert.ToChar(random_Secim).ToString();

                if (SayiMi(parcalar[i]))
                {
                    if (parcalar[i].Length > 3)
                        sifre += parcalar[i].Substring(0, 2) + karakter + parcalar[i].Substring(2, 2);
                }
                else
                {
                    if (rastgele.Next(0, 2) == 0)
                    {
                        sifre += parcalar[i].Substring(0, 1).ToLower();
                        if (rastgele.Next(0, 2) == 1)
                            sifre += karakter;
                    }
                    else
                    {
                        sifre += parcalar[i].Substring(0, 1).ToUpper();
                        if (rastgele.Next(0, 2) == 0)
                            sifre += karakter;
                    }
                }
            }
            sifre += karakter;

            if (chk_sayi_harf_donusumleri.CheckState == CheckState.Checked)
                txt_olusan_sfire.Text = Sayi_Harf_Donusumleri(sifre);
            else
                txt_olusan_sfire.Text = sifre;
        }

        string Sayi_Harf_Donusumleri(string sifre)
        {
            return sifre.Replace("a", "@").Replace("S", "5").Replace("G", "6").Replace("g", "9").Replace("l", "1").Replace("Z", "2")
                .Replace("B", "8").Replace("O", "0").Replace("T", "7");
        }

        bool SayiMi(string text)
        {
            foreach (char chr in text)
            {
                if (!Char.IsNumber(chr)) return false;
            }
            return true;
        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = System.Windows.Forms.Application.StartupPath + "//Sifrelerim.txt";
                string writeText = txt_nerenin_sifresi.Text + " = " + txt_olusan_sfire.Text;
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Close();

                File.AppendAllText(fileName, Environment.NewLine + writeText);
                MessageBox.Show("Kaydedildi");
                btn_kaydet.Enabled = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Kaydedilemedi" + Environment.NewLine + ex.Message);
            }
        }
    }
}
