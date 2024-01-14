using System.Net;
using System.Windows;

namespace SomeS_mt_lab2
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetBook(uri.Text);
        }
        void GetBook(string uri)
        {
            WebClient wc = new WebClient();

            try
            {
                wc.DownloadStringCompleted += (s, eArgs) =>
                {
                    try
                    {
                        string theBook = eArgs.Result;
                        Dispatcher?.Invoke(() => text.Text = theBook);
                    }
                    catch (Exception e)
                    {
                        Dispatcher?.Invoke(() => text.Text = e.InnerException?.Message);
                    }
                };
                wc.DownloadStringAsync(new Uri(uri));
            }
            catch (Exception e)
            {
                text.Text = e.Message;
            }
        }

        private void BaseLevel(object sender, RoutedEventArgs e)
        {
            text.Text = text.Text.Insert(0, "«") + "»";
        }
        private void NormalLevel(object sender, RoutedEventArgs e)
        {
            char[] arr = text.Text.ToCharArray();
            Array.Sort(arr);
            text.Text = string.Concat(arr);
        }
    }
}