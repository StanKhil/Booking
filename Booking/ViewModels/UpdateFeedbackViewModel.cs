using System.Windows.Input;
using Booking.Data.Entities;
using Booking.Models;
using Booking.Views;
using System.Windows;
using FontAwesome.Sharp;

namespace Booking.ViewModels
{
    public class UpdateFeedbackViewModel : ViewModel
    {
        private string? text;
        private int rate;
        private Guid feedbackId;

        private FeedbackModel feedbackModel = new();
        private string? errorMessage = "";

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

        public string? ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public event EventHandler OnRequestClose;

        public UpdateFeedbackViewModel(Feedback feedback)
        {
            feedbackId = feedback.Id;
            Text = feedback.Text;
            Rate = feedback.Rate;
            UpdateCommand = new RelayCommand(ExecuteUpdateCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        }

        private async void ExecuteUpdateCommand(object? parameter)
        {
            if (string.IsNullOrEmpty(Text))
            {
                CustomMessageBox.Show("System", "Please fill in all fields", MessageBoxButton.OK, IconChar.CircleExclamation);
                return;
            }
            bool success = await feedbackModel.UpdateFeedbackAsync(feedbackId, Text, Rate);

            if (success)
            {
                CustomMessageBox.Show("System", "Feedback updated", MessageBoxButton.OK, IconChar.CircleInfo);
                OnRequestClose?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                CustomMessageBox.Show("System", "Error updating feedback", MessageBoxButton.OK, IconChar.CircleExclamation);
            }
        }

        private async void ExecuteDeleteCommand(object? parameter)
        {
            bool success = await feedbackModel.DeleteFeedbackAsync(feedbackId);
            if (success)
            {
                CustomMessageBox.Show("System", "Feedback deleted", MessageBoxButton.OK, IconChar.CircleInfo);
                OnRequestClose?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                CustomMessageBox.Show("System", "Error deleting feedback", MessageBoxButton.OK, IconChar.CircleExclamation);
            }
        }
    }
}
