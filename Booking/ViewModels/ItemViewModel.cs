using Booking.Data;
using Booking.Data.Entities;
using Booking.Services.CurrencyRate;

namespace Booking.ViewModels
{
    class ItemViewModel : ViewModel
    {
        private DataContext context = new();
        private Realty? realty;
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
        {
            realty = null;
        }
        public ItemViewModel(Realty realty)
        {
            this.realty = realty;
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
