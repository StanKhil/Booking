using Booking.Data;
using Booking.Data.Entities;
using Booking.Models;

namespace Booking.ViewModels
{
    public class ScheduledViewModel : ViewModel
    {
        DataContext DataContext = new();
        UserModel userModel;
        List<BookingItem> bookings;

        public ScheduledViewModel()
        {
            userModel = new(DataContext);
            bookings = new List<BookingItem>();
        }

        public List<BookingItem> Bookings
        {
            get => bookings;
            set
            {
                bookings = value;
                OnPropertyChanged(nameof(Bookings));
            }
        }

        public async Task InitializeAsync(UserAccess userAccess)
        {
            Bookings = await userModel.GetFutureBookingsAsync(userAccess);
        }
    }
}
