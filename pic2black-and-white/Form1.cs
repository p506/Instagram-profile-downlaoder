using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MaterialSkin.Animations;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;
using System.Net;

namespace Instagram_profile_downoader
{
    public partial class Form1 : MaterialForm
    {

        string api = "https://mirhamedrooy.ir/api/v1/?username=";
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

        public string profile_hd_photo(string usrname)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 " +
                                                "(KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                string url2 = api + usrname;
                string result = wc.DownloadString(url2);

                dynamic end = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                string profile_hd_photo = end.rouigram.profile_hd_photo;
                return profile_hd_photo;

            }


        }
        public string follower_count(string usrname)
        {

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 " +
                                                "(KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                string url2 = api + usrname;
                string result = wc.DownloadString(url2);

                dynamic end = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                string follower_count = end.rouigram.follower_count;
                return follower_count;

            }
        }

        public string following_count(string usrname)
        {

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 " +
                                                "(KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                string url2 = api + usrname;
                string result = wc.DownloadString(url2);

                dynamic end = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                string following_count = end.rouigram.following_count;
                return following_count;

            }
        }


        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {

            pictureBox1.Load(profile_hd_photo(user.Text));
            f.Text = follower_count(user.Text);
            fo.Text = following_count(user.Text);


        }

        private void materialLabel6_Click(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Png;
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(save.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;

                }
                pictureBox1.Image.Save(save.FileName, format);
            }

        }
        public Bitmap make_bw(Bitmap original)
        {

            Bitmap output = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {

                for (int j = 0; j < original.Height; j++)
                {

                    Color c = original.GetPixel(i, j);

                    int average = ((c.R + c.B + c.G) / 3);

                    if (average < 200)
                        output.SetPixel(i, j, Color.Black);

                    else
                        output.SetPixel(i, j, Color.White);

                }
            }

            return output;

        }




    
    }
}
