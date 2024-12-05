﻿using System;
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
            Console.WriteLine(response.StatusCode+"\n"+UserSession.Instance.AccessToken);
            if (response.IsSuccessStatusCode) Console.WriteLine("Отправилось");
            else Console.WriteLine("Proval");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CrReq();
        }
    }
}
