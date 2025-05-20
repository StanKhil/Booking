using Booking.Data.Entities;
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
        public Catalogue(MainViewModel mainViewModel, UserAccess access, string group = "-")
        {
            InitializeComponent();
            CatalogueViewModel catalogueViewModel = new(mainViewModel, access, group);
            DataContext = catalogueViewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is CatalogueViewModel vm)
            {
                await vm.InitializeAsync();
            }
        }

        private void SortOptionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = this.DataContext as CatalogueViewModel;
            if (vm == null) return;

            var selectedItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            string? tag = selectedItem.Tag as string;

            if (tag == "price")
                vm.SortByPriceCommand.Execute(null);
            else
                vm.SortByRatingCommand.Execute(null);
        }

        /*private void ApplySort_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as CatalogueViewModel;
            if (vm == null) return;

            if (vm.SortByPrice)
                vm.SortByPriceCommand.Execute(null);
            else
                vm.SortByRatingCommand.Execute(null);
        }*/
    }
}
