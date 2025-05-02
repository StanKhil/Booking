using Booking.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Booking
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            LoginView loginView = new();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if(loginView.IsVisible == false && loginView.IsLoaded)
                {
                    MainView mainView = new();
                    mainView.Show();
                    //loginView.Close();
                }
            };
        }
    }

}
