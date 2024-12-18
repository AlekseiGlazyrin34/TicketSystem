using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для Request.xaml
    /// </summary>
    public partial class Request : Page
    {
        
        private CreateRequest CR = new CreateRequest();
        private Account Acc = new Account();
        private MyRequests MR;
        private AdminRequest AMR;

        public Request()
        {
            InitializeComponent();
           
            RequestFrame.Content = CR;

            if (UserSession.Instance.Role == "User")
            {
                MR = new MyRequests();
            }
            if (UserSession.Instance.Role == "Admin")
            {
                AMR = new AdminRequest();
            }
            CrReqButton.Background = new SolidColorBrush(Color.FromRgb(243,243,214));
            CrReqButton.Foreground = Brushes.Black;
            MyReqButton.Background = new SolidColorBrush(Color.FromRgb(129, 166, 240));
            MyReqButton.Foreground = Brushes.Black;

        }

        private void CrReqButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestFrame.Content != CR) {
                CrReqButton.Background = new SolidColorBrush(Color.FromRgb(243, 243, 214));
                CrReqButton.Foreground = Brushes.Black;
                CrReqButText.Foreground = Brushes.Black;
                MyReqButton.Background = new SolidColorBrush(Color.FromRgb(129, 166, 240));
                MyReqButton.Foreground = Brushes.Black;
                MyReqButText.Foreground=Brushes.White;
                RequestFrame.Content = CR;
                Line1.Visibility = Visibility.Visible;
                Line2.Visibility = Visibility.Visible;
                Line3.Visibility = Visibility.Hidden;
                Line4.Visibility = Visibility.Hidden;
            }
            else return;
            
        }

        private void MyReqButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestFrame.Content != MR || RequestFrame.Content != AMR)
            {
                MyReqButton.Background = new SolidColorBrush(Color.FromRgb(243, 243, 214));
                MyReqButton.Foreground = Brushes.Black;
                CrReqButText.Foreground = Brushes.White;
                CrReqButton.Background = new SolidColorBrush(Color.FromRgb(35, 181, 41));
                CrReqButton.Foreground = Brushes.Black;
                MyReqButText.Foreground = Brushes.Black;
                if (MR != null) RequestFrame.Content = MR;
                if (AMR != null) RequestFrame.Content = AMR;
                Line1.Visibility = Visibility.Hidden;
                Line2.Visibility = Visibility.Hidden;
                Line3.Visibility = Visibility.Visible;
                Line4.Visibility = Visibility.Visible;
            }
            else return;
        }
        private void Button_Click_Acc(object sender, RoutedEventArgs e)
        {
            RequestFrame.Content = Acc;            
        }

        
    }
}
