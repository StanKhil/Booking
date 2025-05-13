using System.Windows;
using System.Windows.Controls;
using Booking.Data.Entities;
using Booking.ViewModels;

namespace Booking.Views.Scenes
{
    /// <summary>
    /// Interaction logic for Scheduled.xaml
    /// </summary>
    public partial class Scheduled : UserControl
    {
        UserAccess userAccess;
        public Scheduled(UserAccess userAccess)
        {
            InitializeComponent();
            this.userAccess = userAccess;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (ScheduledViewModel)DataContext;
            viewModel.InitializeAsync(userAccess);
        }
    }
}
