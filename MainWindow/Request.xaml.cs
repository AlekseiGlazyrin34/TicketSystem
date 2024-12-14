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
        private MyRequests MR = new MyRequests();
        public Request()
        {
            InitializeComponent();
            RequestFrame.Content = CR;
        }

        private void CrReqButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestFrame.Content != CR) RequestFrame.Content = CR;
            else return;
        }

        private void MyReqButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestFrame.Content != MR) RequestFrame.Content = MR;
            else return;
        }
    }
}
