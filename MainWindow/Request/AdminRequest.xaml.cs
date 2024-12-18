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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class AdminRequest : Page
    {
        private List<MyReqAdd> myreq;
        public AdminRequest()
        {
            InitializeComponent();
            LoadData();
        }
        private async Task LoadData()
        {

            var response = await UserSession.Instance.SendAuthorizedRequest(() => new HttpRequestMessage(HttpMethod.Get, "https://localhost:7006/load-alldata"));
            var reloadData = JsonSerializer.Deserialize<List<MyReq>>(await response.Content.ReadAsStringAsync());
            MyListView.ItemsSource = reloadData;

        }
        private async void MyListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MyListView.SelectedItem is MyReq selectedRequest)
            {

                var reqId = selectedRequest.requestId;
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7006/loadadd-data?reqid={selectedRequest.requestId}");

                var response = await UserSession.Instance.SendAuthorizedRequest(() => { return request; });
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                myreq = JsonSerializer.Deserialize<List<MyReqAdd>>(await response.Content.ReadAsStringAsync());

                ReqIdTB.Text = "" + myreq[0].requestId;
                ProblemNameTB.Text = myreq[0].problemName;
                DateTimeTB.Text = "" + myreq[0].reqtime;
                PriorityTB.Text = myreq[0].priorityName;
                RoomTB.Text = myreq[0].room;
                DescriptionTB.Text = myreq[0].description;
                if (myreq[0].username != null && myreq[0].responseContent!=null)
                {
                    ResponseFrom.Text = myreq[0].username;
                    ResponseTB.Text = myreq[0].responseContent;
                }
                else
                {
                   ResponseTB.Text = "";
                    ResponseFrom.Text = "";
                }
                if (myreq[0].statusName == "Новый" || myreq[0].username == UserSession.Instance.Username) {
                    ResponseTB.IsReadOnly = false;
                    StatusCB.Text = myreq[0].statusName;
                    StatusCB.Visibility = Visibility.Visible;
                    StatusTB.Text = "";
                    SaveButton.IsEnabled = true;
                }
                else
                {
                    StatusTB.Text = myreq[0].statusName;
                    StatusCB.Visibility = Visibility.Hidden;
                    SaveButton.IsEnabled = false;

                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private async void SaveChanges(object sender, RoutedEventArgs e)
        {
            if (MyListView.SelectedItem is null) return;
            Console.WriteLine(StatusCB.Text+ResponseTB.Text);
            var response = await UserSession.Instance.SendAuthorizedRequest(() => {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7006/save-changes");
                var data = new
                {
                    reqid = myreq[0].requestId,
                    statusName = StatusCB.Text,
                    responseContent = ResponseTB.Text
                };
                request.Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                return request;
        });
        }
    }
}
