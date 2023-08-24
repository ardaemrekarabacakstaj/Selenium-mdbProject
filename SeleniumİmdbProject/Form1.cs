using MaterialSkin;
using MaterialSkin.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = OpenQA.Selenium.Keys;

namespace SeleniumİmdbProject
{
    public partial class Form1 : MaterialForm
    {
        private Dictionary<string, object> filmVerileri;

        public Form1()
        {
            InitializeComponent();

            // Create a material theme manager and add the form to manage (this)
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            string value = materialSingleLineTextField1.Text.ToString();
            IWebDriver driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://www.google.com/");

            var textarea = driver.FindElement(By.XPath("//*[@id='APjFqb']"));
            textarea.Click();
            textarea.SendKeys("imdb");
            textarea.SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            var clickElement = driver.FindElement(By.XPath("//*[@id=\"rso\"]/div[1]/div/div/div/div/div/div/div/div[1]/div/a"));
            clickElement.Click();
            Thread.Sleep(5000);

            var searchBar = driver.FindElement(By.XPath("//*[@id=\"suggestion-search\"]"));
            searchBar.Click();
            searchBar.SendKeys(value);
            searchBar.SendKeys(Keys.Enter);

            var listValues = driver.FindElement(By.XPath("//*[@id=\"__next\"]/main/div[2]/div[3]/section/div/div[1]/section[2]/div[2]/ul/li[1]"));
            listValues.Click();

            var filmName = driver.FindElement(By.XPath("//*[@id=\"__next\"]/main/div/section[1]/section/div[3]/section/section/div[2]/div[1]/h1"));
            var filmDate = driver.FindElement(By.XPath("//*[@id=\"__next\"]/main/div/section[1]/section/div[3]/section/section/div[2]/div[1]/ul/li[1]"));
            var yasKisitlamasi = driver.FindElement(By.XPath("//*[@id=\"__next\"]/main/div/section[1]/section/div[3]/section/section/div[2]/div[1]/ul/li[2]"));
            var filmSuresi = driver.FindElement(By.XPath("//*[@id=\"__next\"]/main/div/section[1]/section/div[3]/section/section/div[2]/div[1]/ul/li[3]"));
            var filmRating = driver.FindElement(By.XPath("//*[@id=\"__next\"]/main/div/section[1]/section/div[3]/section/section/div[3]/div[2]/div[2]/div[1]/div/div[1]/a/span/div/div[2]"));
            var filmImage = driver.FindElement(By.XPath("//*[@id=\"__next\"]/main/div/section[1]/section/div[3]/section/section/div[3]/div[1]/div[1]/div/a/div"));

            var filmTrailer = driver.FindElement(By.XPath("//*[@id=\"__next\"]/main/div/section[1]/section/div[3]/section/section/div[3]/div[2]/div[1]/section/p"));


            int width = 400;
            int height = 400;
            int x = filmImage.Location.X;
            int y = filmImage.Location.Y;

         
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            var base64 = screenshot.AsBase64EncodedString;
            var bytes = Convert.FromBase64String(base64);

            using (var image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(bytes)))
            {
                var croppedImage = new System.Drawing.Bitmap(width, height);
                using (var graphics = System.Drawing.Graphics.FromImage(croppedImage))
                {
                    graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(x, y, width, height), System.Drawing.GraphicsUnit.Pixel);
                }
                
                Image filmImageObj = (Image)croppedImage.Clone();

              
                string screenshotFilePath = $"{filmName.Text.ToString()}_image_screenshot.png";
                croppedImage.Save(screenshotFilePath);

                
                Dictionary<string, string> filmVerileri = new Dictionary<string, string>
                {
                    {"filmName", filmName.Text},
                    {"filmSuresi", filmSuresi.Text},
                    {"yasKisitlamasi", yasKisitlamasi.Text},
                    {"filmDate", filmDate.Text},
                    {"filmTrailer", filmTrailer.Text},
                    {"filmRating", filmRating.Text},
                    {"filmImage", screenshotFilePath}, 
                };


                filmGoster filmGosterForm = new filmGoster(filmVerileri);
                filmGosterForm.Show();
                this.Hide();
                driver.Quit();
                
            }
        }
    }
}
