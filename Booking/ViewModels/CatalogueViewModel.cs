using Booking.Data;
using Booking.Data.Entities;
using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Booking.Views.Scenes;

namespace Booking.ViewModels
{
    class CatalogueViewModel
    {
        DataContext context = new();
        MainViewModel mainViewModel;
        RealtyModel realtyModel;
        public List<Realty> Realties { get; set; }

        public ICommand ViewItemInfoCommand { get; }
        public CatalogueViewModel(MainViewModel mainViewModel)
        {
            ViewItemInfoCommand = new RelayCommand(ExecuteViewItemInfoCommand);
            realtyModel = new(context);
            Realties = realtyModel.GetRealties();
            this.mainViewModel = mainViewModel;
        }

        private void ExecuteViewItemInfoCommand(object? obj)
        {
            Realty? realty = obj as Realty;
            if(realty == null)
            {
                MessageBox.Show("System", "Error reaching the item", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show(realty.Name);
            Item item = new(realty);
            mainViewModel.Item = item;
            mainViewModel.ItemChecked.Execute(new object());
            
        }
    }
}
