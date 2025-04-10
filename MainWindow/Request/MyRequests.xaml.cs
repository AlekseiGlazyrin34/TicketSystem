﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


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
                 
                var myreq = JsonSerializer.Deserialize<List<MyReqAdd>>(await response.Content.ReadAsStringAsync());
                
                ReqIdTB.Text = ""+myreq[0].requestId;
                ProblemNameTB.Text = myreq[0].problemName;
                DateTimeTB.Text =""+ myreq[0].reqtime;
                StatusTB.Text = myreq[0].statusName;
                PriorityTB.Text = myreq[0].priorityName;
                switch (myreq[0].priorityName){
                    case "Отложенный":
                        PriorityTB.Foreground = Brushes.Green;
                        break;
                    case "Срочный":
                        PriorityTB.Foreground = Brushes.Orange;
                        break;
                    case "Критический":
                        PriorityTB.Foreground = Brushes.Red;
                        break;
                }
                RoomTB.Text =myreq[0].room;
                DescriptionTB.Text =  myreq[0].description;
                if (myreq[0].respusername != null)
                {
                    ResponseFrom.Text = myreq[0].respusername;
                    ResponseTB.Text = myreq[0].responseContent;
                }
                else
                {
                    ResponseTB.Text = "";
                    ResponseFrom.Text = "";
                }

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
        public string? respusername { get; set; }
        public string username { get; set; }
    }
}
