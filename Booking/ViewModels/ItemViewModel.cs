using Booking.Data;
using Booking.Data.Entities;
using Booking.Services.CurrencyRate;
using System.Windows;
using System.Windows.Input;

namespace Booking.ViewModels
{
    class ItemViewModel : ViewModel
    {
        private DataContext context = new();
        private Realty? realty;
        private ItemImage? activeImage; 
        private int? activeImageIndex;

        private double priceUa;
        private float rate;

        public double PriceUa
        {
            get => priceUa;
            set
            {
                priceUa = value;
                OnPropertyChanged(nameof(PriceUa));
            }
        }
        public Realty? Realty
        {
            get => realty;
            set
            {
                realty = value;
                OnPropertyChanged(nameof(Realty));
            }
        }

        public double Rate
        {
            get => rate;
            set
            {
                rate = (float)value;
                OnPropertyChanged(nameof(Rate));
            }
        }
        public ItemViewModel()
        public ItemImage? ActiveImage
        {
            get => activeImage;
            set
            {
                activeImage = value;
                OnPropertyChanged(nameof(ActiveImage));
            }
        }
        public ICommand ArrowLeftCommand { get; }
        public ICommand ArrowRightCommand { get; }
        public ItemViewModel() : this(null)
        { }
        public ItemViewModel(Realty? realty)
        {
            ArrowLeftCommand = new RelayCommand(ExecuteArrowLeftCommand);
            ArrowRightCommand = new RelayCommand(ExecuteArrowRightCommand);
            this.realty = realty;

            if (this.realty?.Images.Count <= 1) activeImageIndex = null;
            else activeImageIndex = 1;

            if(activeImageIndex == 1) ActiveImage = realty?.Images[(int)activeImageIndex!];
            else ActiveImage = null;

            if(realty != null) MessageBox.Show(realty.Images.Count + "");


        }

        private void ExecuteArrowRightCommand(object? obj)
        {
            if (activeImage != null)
            {
                activeImageIndex++;
                if (activeImageIndex > realty?.Images.Count - 1)
                {
                    activeImageIndex = 1;
                }
                ActiveImage = realty?.Images[(int)activeImageIndex!];
            }
        }

        private void ExecuteArrowLeftCommand(object? obj)
        {
            //MessageBox.Show(activeImageIndex + "");
            if (activeImageIndex != null)
            {
                activeImageIndex--;
                if (activeImageIndex < 1)
                {
                    activeImageIndex = realty?.Images.Count - 1;
                }
                ActiveImage = realty?.Images[(int)activeImageIndex!];
            }
            
        }

        public async Task Window_LoadedAsync()
        {
            if (Realty != null)
            {
                var service = new NbuCurrencyRate();
                var eurRate = await service.GetCurrencyRateAsync();

                PriceUa = eurRate * (double)Realty.Price;

                Rate = realty.AccRates.AvgRate;
            }
        }
    }
}
