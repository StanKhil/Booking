using Booking.Data;
using Booking.Data.Entities;
using Booking.Models;
using System.Windows;
using System.Windows.Input;
using Booking.Views.Scenes;
using System.ComponentModel;


namespace Booking.ViewModels
{
    class CatalogueViewModel : INotifyPropertyChanged
    {
        DataContext context = new();
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

        public ICommand ViewItemInfoCommand { get; }
        public CatalogueViewModel(MainViewModel mainViewModel)
        {
            ViewItemInfoCommand = new RelayCommand(ExecuteViewItemInfoCommand);
            this.mainViewModel = mainViewModel;
            realtyModel = new RealtyModel(new DataContext());
        }

        public async Task InitializeAsync()
        {
            Realties = await realtyModel.GetRealtiesAsync();
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
