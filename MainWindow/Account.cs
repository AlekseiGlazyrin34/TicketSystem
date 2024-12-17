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
    
    public partial class Account : Page
    {
        public Account()
        {
            InitializeComponent();
            FIO.Text = UserSession.Instance.Username;
            Job.Text=UserSession.Instance.JobtTitle;
            Role.Text=UserSession.Instance.Role;
            LoginT.Text = UserSession.Instance.Login;
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            UserSession.Instance.Clear();
            LoginWindow LW = new LoginWindow();
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            currentWindow.Close();
            LW.Show();
        }
    }
}
