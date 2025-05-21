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
using Booking.Data.Entities;
using Booking.ViewModels.admin;

namespace Booking.Views.Scenes.Admin_Scenes
{
    /// <summary>
    /// Interaction logic for UserScene.xaml
    /// </summary>
    public partial class UserScene : UserControl
    {
        private UserAccess access;
        public UserScene(UserAccess access)
        {
            InitializeComponent();
            this.access = access;
            this.DataContext = new UserAdminViewModel(access);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(this.DataContext != null)
            {
                ((UserAdminViewModel)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }
        private void PasswordBox_PasswordChanged_OnUpdate(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((UserAdminViewModel)this.DataContext).NewPassword = ((PasswordBox)sender).Password;
            }
        }
    }
}
