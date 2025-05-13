using Booking.Data.Entities;
using System.Windows.Controls;
using Booking.ViewModels;
using System.Windows;


namespace Booking.Views.Scenes
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public event EventHandler? OnRequestClose;
        public Settings(UserAccess access)
        {
            InitializeComponent();
            SettingsViewModel viewModel = new(access);
            viewModel.OnRequestClose += (s, e) => this.Close();
            DataContext = viewModel;
        }

        private void Close()
        {
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
