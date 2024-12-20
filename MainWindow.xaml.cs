
using System.Windows;
using System.Windows.Controls;

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
