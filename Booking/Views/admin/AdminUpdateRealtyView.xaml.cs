using Booking.Data;
using Booking.ViewModels.admin;
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

namespace Booking.Views.admin
{
    /// <summary>
    /// Логика взаимодействия для AdminUpdateRealtyView.xaml
    /// </summary>
    public partial class AdminUpdateRealtyView : Window
    {
        DataContext context;
        public AdminUpdateRealtyView(DataContext context)
        {
            InitializeComponent();
            this.context = context;
            AdminUpdateRealtyViewModel viewModel = new();
            viewModel.OnRequestClose += (s, e) => this.Close();
            viewModel.OnRequestClearRealtyUpdateForm += (s, e) => ClearUpdateRealtyForm();
            DataContext = viewModel;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void Minimise(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public void ClearUpdateRealtyForm()
        {

        }
    }
}
