using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text;
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

namespace TicketSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        static HttpClient httpClient = new HttpClient();
        public LoginWindow()
        {
            InitializeComponent();
        }
        private async Task Login()
        {
            var loginCont = LoginBox.Text;
            var passwordCont = PasswordBox.Text;


            var data = new
            {
                Login = loginCont,
                Password = passwordCont
            };
            // создаем JsonContent
            string jsonData = JsonSerializer.Serialize(data);
            // отправляем запрос


            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            // определяем данные запроса
            var response = await httpClient.PostAsync("https://localhost:7006/login", content);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var respData = JsonSerializer.Deserialize<Dictionary<string,string>>(await response.Content.ReadAsStringAsync(), options); ;
                
                foreach( var d in respData)
                {
                    Console.WriteLine(d.Key+" "+d.Value);
                }
                UserSession.Instance.Username = respData["username"];
                UserSession.Instance.Login = respData["login"];
                UserSession.Instance.Password = respData["password"];
                UserSession.Instance.JobtTitle = respData["jobTitle"];
                UserSession.Instance.Role = respData["role"];
                UserSession.Instance.AccessToken = respData["token"];
                UserSession.Instance.RefreshToken = respData["refreshToken"];
                MainWindow MW = new MainWindow();
                this.Close();
                MW.Show();
            }
            else
            {
                Console.WriteLine(response.StatusCode);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }
    }
}
