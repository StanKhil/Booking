using Booking.Data.Entities;
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
using Booking.ViewModels;

namespace Booking.Views.Scenes
{
    /// <summary>
    /// Interaction logic for Administrator.xaml
    /// </summary>
    public partial class Administrator : UserControl
    {
        private UserAccess access;
        public Administrator(UserAccess access)
        {
            InitializeComponent();
            this.access = access;
            this.DataContext = new AdminViewModel(access);
        }
    }
}
