using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        private async Task SendTom()
        {
            var cont = LoginBox.Text;
            StringContent content = new StringContent(cont);
            // определяем данные запроса
            using var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7006/data");
            // установка отправляемого содержимого
            request.Content = content;
            // отправляем запрос
            using var response = await httpClient.SendAsync(request);
            // получаем ответ
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendTom();
            //MainWindow MW = new MainWindow();
            //MW.Show();

        }
    }
}
