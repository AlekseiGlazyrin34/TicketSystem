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
        private async void MyListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MyListView.SelectedItem is MyReq selectedRequest)
            {
                
                var reqId = selectedRequest.requestId;
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7006/loadadd-data?reqid={selectedRequest.requestId}");
                
                var response = await UserSession.Instance.SendAuthorizedRequest(() => { return request; });
                 Console.WriteLine(await response.Content.ReadAsStringAsync());
                var myreq = JsonSerializer.Deserialize<List<MyReqAdd>>(await response.Content.ReadAsStringAsync());
                
                ReqIdTB.Text = "ID: "+ myreq[0].requestId;
                ProblemNameTB.Text ="Название проблемы: "+ myreq[0].problemName;
                DateTimeTB.Text ="Дата/время: "+ myreq[0].reqtime;
                StatusTB.Text = "Статус: " + myreq[0].statusName;
                PriorityTB.Text = "Приоритет: " + myreq[0].priorityName;
                RoomTB.Text ="Помещение: " + myreq[0].room;
                DescriptionTB.Text =  myreq[0].description;
                if (myreq[0].username != null) ResponseTB.Text = $"От {myreq[0].username}\n" + myreq[0].responseContent;
                else ResponseTB.Text = "";

            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
    public class MyReq
    {
        public int requestId { get; set; }
        public string problemName { get; set; }
        public string statusName { get; set; }
        public DateTime reqtime { get; set; }
    }
    public class MyReqAdd
    {
        public int requestId { get; set; }
        public string problemName { get; set; }
        public string statusName { get; set; }
        public string priorityName { get; set; }
        public string description { get; set; }
        public DateTime reqtime { get; set; }
        public string room { get; set; }
        public string? responseContent { get; set; }
        public string? username { get; set; }
    }
}
