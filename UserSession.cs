﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace TicketSystem
{
    public class UserSession
    {
        private static UserSession _instance;
        public static UserSession Instance => _instance ??= new UserSession();

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string JobtTitle { get; set; }
        public string Login { get; set; }
        public string Password { get; set; } 
        public string Role { get; set; }

        private UserSession() { }

        public async Task RefreshAccessToken()
        {
            var client = new HttpClient();
            var content = new StringContent(this.RefreshToken, Encoding.UTF8, "application/plain");

            var response = await client.PostAsync("https://localhost:7006/refresh-token", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                this.AccessToken = responseData;
            }
            string res = await response.Content.ReadAsStringAsync();
            if (res=="LoginAgain")
            {
                
                var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                LoginWindow lw = new LoginWindow();
                currentWindow?.Close();
                
                lw.Show();
            }

        }

        public async Task<HttpResponseMessage> SendAuthorizedRequest(Func<HttpRequestMessage> requestFactory)
        {
            var client = new HttpClient();

            // Устанавливаем токен в заголовок
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);
            var request = requestFactory();
            var response = await client.SendAsync(request);

            // Проверяем, не истёк ли токен
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await RefreshAccessToken(); // Обновляем токен

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);
                request = requestFactory();
                response = await client.SendAsync(request);
            }

            return response;
        }
        public void Clear()
        {
            AccessToken = null;
            RefreshToken = null;
            Username = null;
            JobtTitle = null;
            Login = null;
            Password = null;
            Role = null;
            UserId = 0;
        }
    }

}
