using Booking.Data;
using Booking.Data.Entities;
using Booking.Models;
using Booking.Views;
using System.Windows.Input;

namespace Booking.ViewModels
{
    public class RealtyViewModel : ViewModel
    {
        private string name;
        private string? description;
        private string? slug;
        private string? imageUrl;
        private decimal price;
        private string city;
        private string country;
        private string group;

        private string? errorMessage = "";
        private bool isViewVisible = true;

        DataContext context = new();
        RealtyModel realtyModel;
        List<Feedback> feedbacks;
        List<BookingItem> bookings;
        List<ItemImage> itemImages;

        
        public string Name
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
        public string City
        {
            get => city;
            set { city = value; OnPropertyChanged(nameof(City)); }
        }
        public string Country
        {
            get => country;
            set { country = value; OnPropertyChanged(nameof(Country)); }
        }
        public string Group
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

        public ICommand MainWindowCommand { get; }


        public RealtyViewModel(string slug)
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            this.context = new DataContext();
            this.realtyModel = new RealtyModel(context, slug);
        }

        public RealtyViewModel(DataContext context, RealtyModel model, string slug)
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            this.context = context;
            this.realtyModel = model;
        }

        public async Task InitializeAsync(string slug)
        {
            feedbacks = await realtyModel.GetFeedbacksAsync(slug);
            bookings = await realtyModel.GetBookingsAsync(slug);
            itemImages = await realtyModel.GetImagesAsync(slug);
        }

        private void ExecuteMainWindowCommand(object? obj)
        {
            MainView mainView = new MainView(context);
            mainView.Show();

            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
