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
using Booking.Data;
using Booking.Models;
using Booking.ViewModels;

namespace Booking.Views.Scenes.Sub_Scenes
{
    /// <summary>
    /// Логика взаимодействия для RealtyView.xaml
    /// </summary>
    public partial class RealtyView : Window
    {
        DataContext context;
        public RealtyView(string slug)
        {
            InitializeComponent();
            this.context = new();
            RealtyViewModel realtyViewModel = new(slug);
            realtyViewModel.OnRequestClose += (s, e) => this.Close();
            DataContext = realtyViewModel;

        }
    }
}
