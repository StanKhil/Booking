using Booking.Data;
using Booking.Data.Entities;

namespace Booking.ViewModels
{
    class ItemViewModel : ViewModel
    {
        private DataContext context = new();
        private Realty? realty;
        public Realty? Realty
        {
            get => realty;
            set
            {
                realty = value;
                OnPropertyChanged(nameof(Realty));
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
    }
}
