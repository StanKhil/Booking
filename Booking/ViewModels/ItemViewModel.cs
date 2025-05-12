using Booking.Data;
using Booking.Data.Entities;

namespace Booking.ViewModels
{
    class ItemViewModel
    {
        private DataContext context = new();
        private Realty? realty;
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
