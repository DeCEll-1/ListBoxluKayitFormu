using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace DataBaseliPersonelKimlik
{
    public partial class Form1 : Form
    {
        public string GeciciAd { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        #region checkboxes

        private void cb_Kadin_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cb_Kadin.Checked)
            {
                cb_Erkek.Checked = false;
            }
        }

        private void cb_Erkek_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Erkek.Checked)
            {
                cb_Kadin.Checked = false;
            }
        }
        #endregion

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_Text.SelectedItem != null)
            {
                DirectoryInfo di = new DirectoryInfo(@"C:\Personeller");
                FileInfo[] files = di.GetFiles();
                foreach (FileInfo item in files)
                {
                    if (lb_Text.Text == item.Name)
                    {
                        StreamReader sr = new StreamReader(@"C:\Personeller/" + item.Name);
                        tb_Adi.Text = sr.ReadLine();
                        tb_Soyadi.Text = sr.ReadLine();
                        mtb_Telefon.Text = sr.ReadLine();

                        if (sr.ReadLine() == "Erkek")
                        {
                            cb_Erkek.Checked = true;
                        }
                        else
                        {
                            cb_Kadin.Checked = true;
                        }

                        mtb_TC.Text = sr.ReadLine();
                        tb_Adres.Text = sr.ReadLine();
                        tb_Sehir.Text = sr.ReadLine();
                        tb_Departman.Text = sr.ReadLine();
                        btn_Guncelle.Enabled = true;
                        btn_Sil.Enabled = true;
                        GeciciAd = tb_Adi.Text;
                    }
                }
            }
            else
            {
                tb_Adi.Text = null;
                tb_Soyadi.Text = null;
                mtb_Telefon.Text = null;
                cb_Erkek.Checked = false;
                cb_Kadin.Checked = true;
                mtb_TC.Text = null;
                tb_Adres.Text = null;
                tb_Sehir.Text = null;
                tb_Departman.Text = null;

                btn_Guncelle.Enabled = false;
                btn_Sil.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void Guncelle()
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Personeller");
            FileInfo fi = new FileInfo(@"C:\Personeller/" + GeciciAd + ".per");
            fi.Delete();
            FileInfo fi2 = new FileInfo(@"C:\Personeller/" + tb_Adi.Text + ".per");
            fi2.Create();
            di.Create();
            StreamWriter sw = new StreamWriter(@"C:\Personeller/" + tb_Adi.Text + ".per");
            lbl_Varmi.Visible = true;
            sw.WriteLine(tb_Adi.Text);
            sw.WriteLine(tb_Soyadi.Text);
            sw.WriteLine(mtb_Telefon.Text);
            if (cb_Erkek.Checked)
            {
                sw.WriteLine("Erkek");
            }
            else
            {
                sw.WriteLine("Kadın");
            }
            sw.WriteLine(mtb_TC.Text);
            sw.WriteLine(tb_Adres.Text);
            sw.WriteLine(tb_Sehir.Text);
            sw.WriteLine(tb_Departman.Text);
            sw.Close();
            lbl_Varmi.Text = "Kişi Güncellendi";
            lbl_Varmi.Visible = true;
            int w = Form.ActiveForm.Size.Width;
            int h = Form.ActiveForm.Size.Height;
            lbl_Varmi.Size = new Size(((h / 2) - lbl_Varmi.Size.Height), ((w / 2) - lbl_Varmi.Size.Width));

        }

        private void Submit()
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Personeller");
            FileInfo fi = new FileInfo(@"C:\Personeller/" + tb_Adi.Text + ".per");
            if (!fi.Exists)
            {
                di.Create();
                StreamWriter sw = new StreamWriter(@"C:\Personeller/" + tb_Adi.Text + ".per");
                lbl_Varmi.Visible = true;
                sw.WriteLine(tb_Adi.Text);
                sw.WriteLine(tb_Soyadi.Text);
                sw.WriteLine(mtb_Telefon.Text);
                if (cb_Erkek.Checked)
                {
                    sw.WriteLine("Erkek");
                }
                else
                {
                    sw.WriteLine("Kadın");
                }
                sw.WriteLine(mtb_TC.Text);
                sw.WriteLine(tb_Adres.Text);
                sw.WriteLine(tb_Sehir.Text);
                sw.WriteLine(tb_Departman.Text);
                sw.Close();
                lbl_Varmi.Text = "Kişi Oluşturuldu";
                lbl_Varmi.Visible = true;
                int w = Form.ActiveForm.Size.Width;
                int h = Form.ActiveForm.Size.Height;
                lbl_Varmi.Size = new Size(((h / 2) - lbl_Varmi.Size.Height), ((w / 2) - lbl_Varmi.Size.Width));
            }
            else
            {
                lbl_Varmi.Text = "Kişi Zaten Var";
                int w = Form.ActiveForm.Size.Width;
                int h = Form.ActiveForm.Size.Height;
                lbl_Varmi.Size = new Size(((h / 2) - lbl_Varmi.Size.Height), ((w / 2) - lbl_Varmi.Size.Width));
                lbl_Varmi.Visible = true;
            }
        }

        private void btn_Guncelle_Click(object sender, EventArgs e)
        {
            Guncelle();
        }

        private void btn_Submit_Click_1(object sender, EventArgs e)
        {
            Submit();
        }

        private void LoadForm()
        {
            lb_Text.Items.Clear();
            cb_Erkek.Checked = true;
            DirectoryInfo di = new DirectoryInfo(@"C:\Personeller");
            if (!di.Exists) { di.Create(); }
            FileInfo fi = new FileInfo(@"C:\Personeller/null.per");
            if (!fi.Exists) { fi.Create(); }

            lbl_FileTrue.Text = Convert.ToString(di.Exists);
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo item in files)
            {
                lb_Text.Items.Add(item.Name);
            }
        }

        private void btn_Yenile_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void btn_Sil_Click(object sender, EventArgs e)
        {
            Sil();
        }

        private void Sil()
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Personeller");
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo item in files)
            {
                if (lb_Text.Text == item.Name)
                {
                    lb_Text.SelectedIndex = 0;
                    GeciciAd = item.Name;
                    break;
                }
            }
            

        }
    }
}