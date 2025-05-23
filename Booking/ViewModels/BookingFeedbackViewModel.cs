using Booking.Data.Entities;
using Booking.Models;
using System.Windows.Input;
using System.Windows;
using Booking.Data;
using FontAwesome.Sharp;

namespace Booking.ViewModels
{
    public class BookingFeedbackViewModel : ViewModel
    {
        DataContext context = new DataContext();
        public BookingItem Booking { get; set; }

        private string? text;
        private int rate;

        public string? Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public int Rate
        {
            get => rate;
            set
            {
                rate = value;
                OnPropertyChanged(nameof(Rate));
            }
        }
        public ICommand SubmitReviewCommand { get; }
        public ICommand DeleteBookingCommnad { get; }

        private readonly FeedbackModel feedbackModel;
        private readonly UserAccess access;
        private readonly BookingModel bookingModel;

        public BookingFeedbackViewModel(BookingItem booking, UserAccess userAccess)
        {
            Booking = booking;
            access = userAccess;
            feedbackModel = new FeedbackModel();
            bookingModel = new BookingModel(context);
            SubmitReviewCommand = new RelayCommand(SubmitReview);
            DeleteBookingCommnad = new RelayCommand(DeleteBookingCommand);
        }

        private async void SubmitReview(object? obj)
        {
            if (string.IsNullOrWhiteSpace(Text) || Rate <= 0)
            {
                CustomMessageBox.Show("System", "Text and Rate are required", MessageBoxButton.OK, IconChar.CircleExclamation);
                return;
            }


            bool result = await feedbackModel.CreateFeedbackAsync(access.Id, Booking.Realty.Id, Text, Rate);
            if(result)
            {
                CustomMessageBox.Show("System", "Feedback submitted successfully", MessageBoxButton.OK, IconChar.CircleCheck);
            }
            else
            {
                CustomMessageBox.Show("System", "Error submitting feedback", MessageBoxButton.OK, IconChar.CircleExclamation);
            }
        }

        private async void DeleteBookingCommand(object? obj)
        {
            var result = await bookingModel.DeleteBookingAsync(Booking.Id);
            if(result)
            {
                CustomMessageBox.Show("System", "Booking deleted", MessageBoxButton.OK, IconChar.CircleExclamation);
            }
            else
            {
                CustomMessageBox.Show("System", "Error deleting booking", MessageBoxButton.OK, IconChar.CircleExclamation);
            }
        }
    }
}
