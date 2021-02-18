using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EOP_Down
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public bool DownloadImageAndSaveFile(string url, string path)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetWebClient(string url)
        {
            try
            {
                string strHTML = "";
                WebClient myWebClient = new WebClient();
                Stream myStream = myWebClient.OpenRead(url);
                StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
                strHTML = sr.ReadToEnd();
                myStream.Close();
                return strHTML;
            }
            catch
            {
                return "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Url.Text != "")
            {
                string str = GetWebClient(TextBox_Url.Text);
                string[] strings = Regex.Split(str, "<img src=\"/pianomusic/", RegexOptions.IgnoreCase);
                int i = 1;
                foreach (string _s in strings)
                {
                    if (!_s.Contains("class=\"img-responsive DownMusicPNG\"")) continue;
                    string s = Strings.Left(_s, _s.IndexOf(".png") + 4);
                    DownloadImageAndSaveFile("https://www.everyonepiano.cn/pianomusic/" + s, i++.ToString("000") + ".png");
                }
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Url.Text = "";
        }
    }
}
