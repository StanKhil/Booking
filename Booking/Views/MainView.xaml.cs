using Booking.Data;
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
using System.Windows.Shapes;
using Booking.ViewModels;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Booking.Data.Entities;
using Booking.Models;
using System.IO;

namespace Booking.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        DataContext context;
        public MainView(DataContext context)
        {
            InitializeComponent();
            //MessageBox.Show("Another");
            this.context = context;
            MainViewModel viewModel = new();
            viewModel.OnRequestClose += (s, e) => this.Close();
            DataContext = viewModel;
        }
        public MainView(DataContext context, UserAccess access)
        {
            InitializeComponent();
            //MessageBox.Show(access.Login);
            this.context = context;
            MainViewModel viewModel = new(access);
            viewModel.OnRequestClose += (s, e) => this.Close();
            DataContext = viewModel;

            /*ImageModel imageModel = new ImageModel(context);
            imageModel.LoadImageAsync("villa-sunny", "‪C:\\Users\\user\\OneDrive\\Рабочий стол\\css.jpg");*/
            //FeedbackModel feedbackModel = new FeedbackModel();
            //feedbackModel.CreateFeedbackAsync(access.Id, Guid.Parse("37dcc68e-b7e7-4b55-b04e-147c1a4126b7"), "hello world", 5);
            //feedbackModel.DeleteFeedbackAsync(Guid.Parse("6c7697be-f0be-4884-b12f-53ed7689a407"));
            //BookingModel bookingModel = new BookingModel(context);
            //bookingModel.CreateBookingAsync(access.Id, Guid.Parse("37dcc68e-b7e7-4b55-b04e-147c1a4126b7"), DateTime.Now, DateTime.Now.AddDays(5));
            //bookingModel.DeleteBookingAsync(Guid.Parse("dba25ddc-a700-4dea-9769-23f05fe7691b"));
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
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

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            LogoutButton.Visibility = LogoutButton.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
    }
}
