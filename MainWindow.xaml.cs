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
       
        private Request Req;
        public MainWindow()
        {
            InitializeComponent();
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // Устанавливаем скроллинг в зависимости от размеров
            if ( this.ActualHeight > screenHeight)
            {
                MyScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else
            {
                MyScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }
            
            Req = new Request();
            MainFrame.Content= Req;

            
        }

        

        
    }
}
