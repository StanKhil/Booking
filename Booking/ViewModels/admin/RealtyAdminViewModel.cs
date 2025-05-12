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
    public class RealtyAdminViewModel : ViewModel
    {
        private string? name;
        private string? description;
        private string? slug;
        private string? imageUrl;
        private decimal price;
        private string? city;
        private string? country;
        private string? group;

        private string? updateSlug;

        private string? newName;
        private string? newDescription;
        private string? newSlug;
        private string? newImageUrl;
        private decimal newPrice;
        private string? newCity;
        private string? newCountry;
        private string? newGroup;

        private string? errorMessageOnCreate = "";
        private string? errorMessageOnUpdate = "";
        private string? errorMessageOnDelete = "";
        private bool isViewVisible = true;

        DataContext context = new();
        RealtyModel realtyModel;

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
        public string? Slug
        {
            get => slug;
            set { slug = value; OnPropertyChanged(nameof(Slug)); }
        }
        public string? ImageUrl
        {
            get => imageUrl;
            set { imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); }
        }
        public decimal Price
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

        public string? UpdateSlug
        {
            get => updateSlug;
            set
            {
                updateSlug = value;
                OnPropertyChanged(nameof(UpdateSlug));
            }
        }
        public string? NewName
        {
            get => newName;
            set
            {
                newName = value;
                OnPropertyChanged(nameof(NewName));
            }
        }
        public string? NewDescription
        {
            get => newDescription;
            set
            {
                newDescription = value;
                OnPropertyChanged(nameof(NewDescription));
            }
        }
        public string? NewSlug
        {
            get => newSlug;
            set
            {
                newSlug = value;
                OnPropertyChanged(nameof(NewSlug));
            }
        }
        public string? NewImageUrl
        {
            get => newImageUrl;
            set
            {
                newImageUrl = value;
                OnPropertyChanged(nameof(NewImageUrl));
            }
        }
        public decimal NewPrice
        {
            get => newPrice;
            set
            {
                newPrice = value;
                OnPropertyChanged(nameof(NewPrice));
            }
        }
        public string? NewCity
        {
            get => newCity;
            set
            {
                newCity = value;
                OnPropertyChanged(nameof(NewCity));
            }
        }
        public string? NewCountry
        {
            get => newCountry;
            set
            {
                newCountry = value;
                OnPropertyChanged(nameof(NewCountry));
            }
        }
        public string? NewGroup
        {
            get => newGroup;
            set
            {
                newGroup = value;
                OnPropertyChanged(nameof(NewGroup));
            }
        }

        public string? ErrorMessageOnCreate
        {
            get => errorMessageOnCreate;
            set { errorMessageOnCreate = value; OnPropertyChanged(nameof(ErrorMessageOnCreate)); }
        }
        public string? ErrorMessageOnUpdate
        {
            get => errorMessageOnUpdate;
            set { errorMessageOnUpdate = value; OnPropertyChanged(nameof(ErrorMessageOnUpdate)); }
        }
        public string? ErrorMessageOnDelete
        {
            get => errorMessageOnDelete;
            set { errorMessageOnDelete = value; OnPropertyChanged(nameof(ErrorMessageOnDelete)); }
        }
        public bool IsViewVisible
        {
            get => isViewVisible;
            set { isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); }
        }

        public event EventHandler OnRequestClose;
        public event EventHandler OnRequestClearRealtyCreateForm;
        public event EventHandler OnRequestClearRealtyDeleteForm;
        public event EventHandler OnRequestClearRealtyUpdateForm;

        public ICommand MainWindowCommand { get; }
        public ICommand CreateRealtyCommand { get; }
        public ICommand DeleteRealtyCommand { get; }
        public ICommand UpdateRealtyCommand { get; }

        public RealtyAdminViewModel()
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            CreateRealtyCommand = new RelayCommand(ExecuteCreateRealtyCommand);
            DeleteRealtyCommand = new RelayCommand(ExecuteDeleteRealtyCommand);
            UpdateRealtyCommand = new RelayCommand(ExecuteUpdateRealtyCommand);
            this.context = new();
            this.realtyModel = new(context);
        }

        public RealtyAdminViewModel(DataContext context, RealtyModel model)
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            CreateRealtyCommand = new RelayCommand(ExecuteCreateRealtyCommand);
            DeleteRealtyCommand = new RelayCommand(ExecuteDeleteRealtyCommand);
            UpdateRealtyCommand = new RelayCommand(ExecuteUpdateRealtyCommand);
            this.context = context;
            this.realtyModel = model;
        }

        private void ExecuteMainWindowCommand(object? obj)
        {
            MainView mainView = new MainView(context);
            mainView.Show();

            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private async void ExecuteCreateRealtyCommand(object? obj)
        {
            bool success = await realtyModel.CreateRealtyAsync(name, description, slug, imageUrl, price, city, country, group);
            if (!success)
            {
                ErrorMessageOnCreate = "Invalid data";
                return;
            }
            OnRequestClearRealtyCreateForm?.Invoke(this, EventArgs.Empty);
        }

        private async void ExecuteDeleteRealtyCommand(object? obj)
        {
            bool success = await realtyModel.DeleteRealtyAsync(slug);
            if (!success)
            {
                ErrorMessageOnDelete = "Invalid slug";
                return;
            }
            OnRequestClearRealtyDeleteForm?.Invoke(this, EventArgs.Empty);
        }

        private async void ExecuteUpdateRealtyCommand(object? obj)
        {
            bool success = await realtyModel.UpdateRealtyAsync(slug, name, description, newSlug, imageUrl, price, city, country, group);
            if (!success)
            {
                ErrorMessageOnUpdate = "Invalid data";
                return;
            }
            OnRequestClearRealtyUpdateForm?.Invoke(this, EventArgs.Empty);
        }
    }
}
