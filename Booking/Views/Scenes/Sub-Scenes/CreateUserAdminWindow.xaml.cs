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

namespace Booking.Views.Scenes.Sub_Scenes
{
    /// <summary>
    /// Interaction logic for CreateUserAdminWindow.xaml
    /// </summary>
    public partial class CreateUserAdminWindow : Window
    {
        DataContext context;
        public CreateUserAdminWindow(DataContext context)
        {
            InitializeComponent();
            this.context = context;
            CreateUserAdminViewModel userAdminViewModel = new();
            userAdminViewModel.OnRequestClose += (s, e) => this.Close();
            userAdminViewModel.OnRequestClearUserCreateForm += (s, e) => ClearCreateUserForm();
            userAdminViewModel.OnRequestClearUserUpdateForm += (s, e) => ClearCreateUserForm();
            userAdminViewModel.OnRequestClearUserDeleteForm += (s, e) => ClearCreateUserForm();
            DataContext = userAdminViewModel;
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
            this.Close();
        }

        public void ClearCreateUserForm()
        {
        }
        public void ClearUpdateUserForm()
        {
        }
        public void ClearDeleteUserForm()
        {
        }
    }
}
