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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private History Hist;
        private Request Req;
        public MainWindow()
        {
            InitializeComponent();
            Hist = new History();
            Req = new Request();
            MainFrame.Content= Req;
        }

        private void Button_Click_Page1(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = Req;
        }

        private void Button_Click_Page2(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = Hist;
        }
    }
}
