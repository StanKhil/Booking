using Booking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
