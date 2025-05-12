using Booking.ViewModels;
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

namespace Booking.Views.Scenes
{
    /// <summary>
    /// Interaction logic for Catalogue.xaml
    /// </summary>
    public partial class Catalogue : UserControl
    {
        public Catalogue(MainViewModel mainViewModel)
        {
            InitializeComponent();
            CatalogueViewModel catalogueViewModel = new(mainViewModel);
            DataContext = catalogueViewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is CatalogueViewModel vm)
            {
                await vm.InitializeAsync();
            }
        }
    }
}
