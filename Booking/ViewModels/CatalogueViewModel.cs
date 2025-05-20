using Booking.Data;
using Booking.Data.Entities;
using Booking.Models;
using System.Windows;
using System.Windows.Input;
using Booking.Views.Scenes;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using FontAwesome.Sharp;


namespace Booking.ViewModels
{
    class CatalogueViewModel : INotifyPropertyChanged
    {
        DataContext context = new();
        UserAccess access;
        MainViewModel mainViewModel;
        RealtyModel realtyModel;
        private List<Realty> realties;
        public List<Realty> Realties
        {
            get => realties;
            set
            {
                realties = value;
                OnPropertyChanged(nameof(Realties));
            }
        }

        private bool sortByPrice = false;
        private bool sortAscending = false;

        private string? cityFilter = null;
        public string? CityFilter
        {
            get => cityFilter;
            set
            {
                cityFilter = value;
                OnPropertyChanged(nameof(CityFilter));
            }
        }
        private string? countryFilter = null;
        public string? CountryFilter
        {
            get => countryFilter;
            set
            {
                countryFilter = value;
                OnPropertyChanged(nameof(CountryFilter));
            }
        }
        private string? groupFilter = null;
        public string? GroupFilter
        {
            get => groupFilter;
            set
            {
                groupFilter = value;
                OnPropertyChanged(nameof(GroupFilter));
            }
        }
        private string selectedSortOption = "rating";
        public string SelectedSortOption
        {
            get => selectedSortOption;
            set
            {
                if (selectedSortOption != value)
                {
                    selectedSortOption = value;
                    OnPropertyChanged(nameof(SelectedSortOption));
                    ApplySort();
                }
            }
        }

        public bool SortAscending
        {
            get => sortAscending;
            set
            {
                if (sortAscending != value)
                {
                    sortAscending = value;
                    OnPropertyChanged(nameof(SortAscending));
                    ApplySort();
                }
            }
        }

        private List<string> groups = new()
        {
            "Hotels",
            "Apartments",
            "Houses",
            "Villas",
            "-"
        };

        public List<string> Groups
        {
            get => groups;
            set
            {
                groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        private List<string> cities;
        public List<string> Cities
        {
            get => cities;
            set
            {
                cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }

        private List<string> countries;
        public List<string> Countries
        {
            get => countries;
            set
            {
                countries = value;
                OnPropertyChanged(nameof(Countries));
            }
        }

        public ICommand ViewItemInfoCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand SortByPriceCommand { get; }
        public ICommand SortByRatingCommand { get; }

        public CatalogueViewModel(MainViewModel mainViewModel, UserAccess access, string group)
        {
            ViewItemInfoCommand = new RelayCommand(ExecuteViewItemInfoCommand);
            FilterCommand = new RelayCommand(ExecuteFilterCommand);
            SortByPriceCommand = new RelayCommand(ExecuteSortByPriceCommand);
            //SortByRatingCommand = new RelayCommand(ExecuteSortByRatingCommand);

            this.mainViewModel = mainViewModel;
            realtyModel = new RealtyModel(new DataContext());
            this.access = access;
            GroupFilter = group;
        }

        public async Task InitializeAsync()
        {
            Realties = await realtyModel.GetRealtiesAsync();
            Realties = await realtyModel.GetRealtiesSortedByRatingAsync(Realties, SortAscending);
            Cities = await context.Cities.Select(c => c.Name).ToListAsync();
            Countries = await context.Countries.Select(c => c.Name).ToListAsync();
            Cities.Add("-");
            Countries.Add("-");

            CityFilter = Cities.Last();
            CountryFilter = Countries.Last();
            if (GroupFilter == "-")
                GroupFilter = Groups.Last();
            else
                FilterCommand.Execute(null);
        }

        private void ExecuteViewItemInfoCommand(object? obj)
        {
            Realty? realty = obj as Realty;
            if(realty == null)
            {
                //MessageBox.Show("System", "Error reaching the item", MessageBoxButton.OK, MessageBoxImage.Error);
                CustomMessageBox.Show("System", "Error reaching the item", MessageBoxButton.OK, IconChar.TriangleExclamation);
                return;
            }
            //MessageBox.Show(realty.Name + " | " + realty.Country.Name);
            Item item = new(realty, access);
            mainViewModel.Item = item;
            mainViewModel.ItemChecked.Execute(new object());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private async void ExecuteFilterCommand(object? obj)
        {
            Realties = await realtyModel.GetRealtiesByFilterAsync(CityFilter!, CountryFilter!, GroupFilter!);
        }
        private async void ExecuteSortByPriceCommand(object? obj)
        {
            Realties = await realtyModel.GetRealtiesSortedByPriceAsync(Realties, SortAscending);
        }
        /*private async void ExecuteSortByRatingCommand(object? obj)
        {
            Realties = await realtyModel.GetRealtiesSortedByRatingAsync(Realties, SortAscending);
        }*/

        private async void ApplySort()
        {
            if (SelectedSortOption == "price")
                Realties = await realtyModel.GetRealtiesSortedByPriceAsync(Realties, SortAscending);
            else
                Realties = await realtyModel.GetRealtiesSortedByRatingAsync(Realties, SortAscending);
        }


    }
}
