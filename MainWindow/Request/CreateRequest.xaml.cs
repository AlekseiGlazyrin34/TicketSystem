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
    /// Логика взаимодействия для CreateRequest.xaml
    /// </summary>
    public partial class CreateRequest : Page
    {
        static HttpClient httpClient = new HttpClient();
        public CreateRequest()
        {
            InitializeComponent();
        }

        private async Task CrReq()
        {
            

            var response = await UserSession.Instance.SendAuthorizedRequest(() => new HttpRequestMessage(HttpMethod.Get, "https://localhost:7006/data"));
            Console.WriteLine(response.StatusCode+"\n"+UserSession.Instance.AccessToken);
            //await httpClient.GetAsync("https://localhost:7006/data");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CrReq();
        }
    }
}
