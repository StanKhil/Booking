using Booking.Data;
using Booking.Models;
using Booking.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Booking.ViewModels.admin
{
    public class AdminUpdateRealtyViewModel : ViewModel
    {
        private string slug;
        private string? name;
        private string? description;
        private string? newSlug;
        private string? imageUrl;
        private decimal? price;
        private string? city;
        private string? country;
        private string? group;

        private string? errorMessage = "";
        private bool isViewVisible = true;

        DataContext context = new();
        RealtyModel realtyModel;

        public string Slug 
        { 
            get => slug; 
            set { slug = value; OnPropertyChanged(nameof(Slug)); } 
        }
        public string? Name 
        { 
            get => name; 
            set { name = value; OnPropertyChanged(nameof(Name)); } 
        }
        public string? Description 
        { 
            get => description; 
            set { description = value; OnPropertyChanged(nameof(Description)); } 
        }
        public string? NewSlug 
        { 
            get => newSlug; 
            set { newSlug = value; OnPropertyChanged(nameof(NewSlug)); } 
        }
        public string? ImageUrl 
        { 
            get => imageUrl; 
            set { imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); } 
        }
        public decimal? Price 
        { 
            get => price; 
            set { price = value; OnPropertyChanged(nameof(Price)); } 
        }
        public string? City 
        { 
            get => city; 
            set { city = value; OnPropertyChanged(nameof(City)); } 
        }
        public string? Country 
        { 
            get => country; 
            set { country = value; OnPropertyChanged(nameof(Country)); } 
        }
        public string? Group 
        { 
            get => group; 
            set { group = value; OnPropertyChanged(nameof(Group)); } 
        }

        public string? ErrorMessage
        {
            get => errorMessage;
            set { errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }

        public bool IsViewVisible
        {
            get => isViewVisible;
            set { isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); }
        }

        public event EventHandler OnRequestClose;
        public event EventHandler OnRequestClearRealtyUpdateForm;

        public ICommand MainWindowCommand { get; }
        public ICommand UpdateRealtyCommand { get; }

        public AdminUpdateRealtyViewModel()
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            UpdateRealtyCommand = new RelayCommand(ExecuteUpdateRealtyCommand);
            this.context = new();
            this.realtyModel = new(context);
        }

        public AdminUpdateRealtyViewModel(DataContext context, RealtyModel model)
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            UpdateRealtyCommand = new RelayCommand(ExecuteUpdateRealtyCommand);
            this.context = context;
            this.realtyModel = model;
        }

        private void ExecuteMainWindowCommand(object obj)
        {
            MainView mainView = new MainView(context);
            mainView.Show();
            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteUpdateRealtyCommand(object obj)
        {
            bool success = realtyModel.UpdateRealty(slug, name, description, newSlug, imageUrl, price, city, country, group);
            if (!success)
            {
                ErrorMessage = "Invalid data";
                return;
            }
            OnRequestClearRealtyUpdateForm?.Invoke(this, EventArgs.Empty);
        }
    }

}
