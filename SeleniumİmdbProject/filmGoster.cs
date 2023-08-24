using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumİmdbProject
{
    public partial class filmGoster : MaterialForm
    {
        private Dictionary<string, string> filmVerileri;

        public filmGoster(Dictionary<string, string> filmVerileri)
        {
            InitializeComponent();
            filmVerileri = filmVerileri;

            if (filmVerileri.ContainsKey("filmName"))
            {
                materialSingleLineTextField1.Text = "Film Adı : " + filmVerileri["filmName"];
            }
            if (filmVerileri.ContainsKey("filmDate"))
            {
                materialSingleLineTextField3.Text = "Yayınlanma Tarihi : " + filmVerileri["filmDate"];
            }
            if (filmVerileri.ContainsKey("filmSuresi"))
            {
                materialSingleLineTextField2.Text = "Film Süresi : " + filmVerileri["filmSuresi"];
            }
            if (filmVerileri.ContainsKey("filmRating"))
            {
                materialSingleLineTextField4.Text =  "Film Rating : "+ filmVerileri["filmRating"];
            }
            if (filmVerileri.ContainsKey("filmTrailer"))
            {
                richTextBox1.Text =  "Film Trailer : "+ filmVerileri["filmTrailer"];
            }
            if (filmVerileri.ContainsKey("yasKisitlamasi"))
            {
                materialLabel1.Text = "Yaş Uygunluğu : " + filmVerileri["yasKisitlamasi"];
            }
            if (filmVerileri.ContainsKey("filmImage"))
            {
                string imagePath = filmVerileri["filmImage"];
                Image image = Image.FromFile(imagePath);
                pictureBox1.Image = image;
            }
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;


            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
        }
    }
}
