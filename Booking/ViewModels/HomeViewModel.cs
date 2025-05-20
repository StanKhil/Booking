using Booking.Data;
using Booking.Data.Entities;
using System.Windows.Input;
using Booking.Views.Scenes;

namespace Booking.ViewModels
{
    public class HomeViewModel : ViewModel
    {
        DataContext context = new();
        UserAccess access;
        MainViewModel mainViewModel;

        public ICommand HotelsCommand { get; }
        public ICommand ApartmentsCommand { get; }
        public ICommand HousesCommand { get; }
        public ICommand VillasCommand { get; }

        public HomeViewModel(MainViewModel mainViewModel, UserAccess access) 
        {
            HotelsCommand = new RelayCommand(ExecuteHotelsCommand);
            ApartmentsCommand = new RelayCommand(ExecuteApartmentsCommand);
            HousesCommand = new RelayCommand(ExecuteHousesCommand);
            VillasCommand = new RelayCommand(ExecuteVillasCommand);

            this.access = access;
            this.mainViewModel = mainViewModel;
        }

        private void ExecuteHotelsCommand(object? parameter)
        {
            string group = "Hotels";
            mainViewModel.Content = "Hotels";
            mainViewModel.SceneContainer.Content = new Catalogue(mainViewModel, access, group);
        }

        private void ExecuteApartmentsCommand(object? parameter)
        {
            string group = "Apartments";
            mainViewModel.Content = "Apartments";
            mainViewModel.SceneContainer.Content = new Booking.Views.Scenes.Catalogue(mainViewModel, access, group);
        }

        private void ExecuteHousesCommand(object? parameter)
        {
            string group = "Houses";
            mainViewModel.Content = "Houses";
            mainViewModel.SceneContainer.Content = new Booking.Views.Scenes.Catalogue(mainViewModel, access, group);
        }

        private void ExecuteVillasCommand(object? parameter)
        {
            string group = "Villas";
            mainViewModel.Content = "Villas";
            mainViewModel.SceneContainer.Content = new Booking.Views.Scenes.Catalogue(mainViewModel, access, group);
        }
    }
}
