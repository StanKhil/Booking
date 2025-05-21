using Booking.Data.Entities;
using System.Windows;
using System.Windows.Input;
using Booking.ViewModels;



namespace Booking.Views
{
    /// <summary>
    /// Логика взаимодействия для UpdateFeedbackView.xaml
    /// </summary>
    public partial class UpdateFeedbackView : Window
    {
        public UpdateFeedbackView(Feedback feedback)
        {
            InitializeComponent();
            var feedbackViewModel = new UpdateFeedbackViewModel(feedback);
            this.DataContext = feedbackViewModel;
            feedbackViewModel.OnRequestClose += (s, e) => this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void buttonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
