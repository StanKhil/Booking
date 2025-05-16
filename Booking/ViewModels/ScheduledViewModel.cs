using Booking.Data;
using Booking.Data.Entities;
using Booking.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace Booking.ViewModels
{
    public class ScheduledViewModel : ViewModel
    {
        private readonly DataContext DataContext;
        private readonly UserModel userModel;

        public ObservableCollection<BookingFeedbackViewModel> Bookings { get; set; } = new();

        private UserAccess access;

        public ScheduledViewModel()
        {
            DataContext = new DataContext();
            userModel = new UserModel(DataContext);
        }

        public async Task InitializeAsync(UserAccess userAccess)
        {
            access = userAccess;
            var bookingItems = await userModel.GetBookingsAsync(access);

            Bookings = new ObservableCollection<BookingFeedbackViewModel>(
                bookingItems.Select(b => new BookingFeedbackViewModel(b,access))
            );

            OnPropertyChanged(nameof(Bookings));
        }
    }
}
