using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAnimatedGif;

namespace TicketSystem
{
    /// <summary>
    /// Логика взаимодействия для CreateRequest.xaml
    /// </summary>
    public partial class CreateRequest : Page
    {
        static HttpClient httpClient = new HttpClient();
        public CreateRequest()
        {
            InitializeComponent();

            var gifPath = "/images/waiting.gif";
            var imageSource = new Uri(gifPath, UriKind.RelativeOrAbsolute);
            ImageBehavior.SetAnimatedSource(GifImage, new System.Windows.Media.Imaging.BitmapImage(imageSource));
            GifImage.Visibility = Visibility.Collapsed;
        }

        private async Task CrReq()
        {

            if (ProblemName.Text=="" || Room.Text=="" || Priority.Text == "")
            {
                TextError.Visibility= Visibility.Visible;
                return;
            }
            ReqButton.IsEnabled = false;
            GifImage.Visibility = Visibility.Visible;
            
            var response = await UserSession.Instance.SendAuthorizedRequest(() => {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7006/send-request");
                var data = new
                {
                    ProblemName = ProblemName.Text,
                    Room = Room.Text,
                    Priority = Priority.Text,
                    Description = Additional.Text
                };
                
                request.Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                return request;
            });

            if (response.IsSuccessStatusCode)
            {
                
                GifImage.Visibility = Visibility.Collapsed;
                ReqButton.IsEnabled = true;
                ProblemName.Text = "";
                Room.Text = "";
                Priority.Text = "";
                Additional.Text= "";
                MessageBox.Show("Запрос успешно отправлен", "Запрос", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                GifImage.Visibility = Visibility.Collapsed;
                ReqButton.IsEnabled = true;
            }
        }

        private async void ReqButton_Click(object sender, RoutedEventArgs e)
        {
            await CrReq();
        }
    }
}
