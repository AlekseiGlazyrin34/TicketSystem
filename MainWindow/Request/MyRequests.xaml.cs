using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
    /// Логика взаимодействия для MyRequests.xaml
    /// </summary>
    public partial class MyRequests : Page
    {
        HttpClient httpClient = new HttpClient();
        public MyRequests()
        {
            InitializeComponent();
            LoadData();
        }

        private async Task LoadData()
        {
            var response = await UserSession.Instance.SendAuthorizedRequest(() => new HttpRequestMessage(HttpMethod.Get, "https://localhost:7006/load-data"));
            var reloadData = JsonSerializer.Deserialize<List<MyReq>>(await response.Content.ReadAsStringAsync());
            MyListView.ItemsSource =reloadData;
            
            
        }
        
    }
    public class MyReq
    {
        public int requestId { get; set; }
        public string problemName { get; set; }
        public string statusName { get; set; }
        public DateTime reqtime { get; set; }
    }
}
