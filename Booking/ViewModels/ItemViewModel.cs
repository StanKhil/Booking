using Booking.Data;
using Booking.Data.Entities;
using Booking.Models;
using Booking.Services.CurrencyRate;
using System.Windows;
using System.Windows.Input;
using Booking.Views;
using FontAwesome.Sharp;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Booking.ViewModels
{
    class ItemViewModel : ViewModel
    {
        private DataContext context = new();
        UserAccess access;
        private RealtyModel reltyModel;
        private Realty? realty;
        private ItemImage? activeImage;
        private int? activeImageIndex;
        private Feedback? activeFeedback;
        private int activeFeedbackIndex;

        private List<BookingItem> futureBooking;

        private double priceUa;
        private float rate;

        private DateTime startDate;
        private DateTime endDate;
        private List<Feedback> feedbacks;

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
        public ItemImage? ActiveImage
        {
            get => activeImage;
            set
            {
                activeImage = value;
                OnPropertyChanged(nameof(ActiveImage));
            }
        }
        public Feedback? ActiveFeedback
        {
            get => activeFeedback;
            set
            {
                activeFeedback = value;
                OnPropertyChanged(nameof(ActiveFeedback));
            }
        }


        public List<BookingItem> FutureBooking
        {
            get => futureBooking;
            set
            {
                futureBooking = value;
                OnPropertyChanged(nameof(FutureBooking));
            }
        }


        public DateTime? BookingStartDate
        {
            get => startDate;
            set
            {
                startDate = (DateTime)value!;
                OnPropertyChanged(nameof(BookingStartDate));
            }
        }
        public DateTime? BookingEndDate
        {
            get => endDate;
            set
            {
                endDate = (DateTime)value!;
                OnPropertyChanged(nameof(BookingEndDate));
            }
        }

        public ICommand BookCommand { get; }
        public ICommand ArrowLeftCommand { get; }
        public ICommand ArrowRightCommand { get; }
        public ICommand FeedbackArrowLeftCommand { get; }
        public ICommand FeedbackArrowRightCommand { get; }

        public ICommand UpdateFeedbackCommand { get; }
        public ItemViewModel() : this(null, null) { }
        public ItemViewModel(Realty? realty, UserAccess access)
        {
            ArrowLeftCommand = new RelayCommand(ExecuteArrowLeftCommand);
            ArrowRightCommand = new RelayCommand(ExecuteArrowRightCommand);
            FeedbackArrowLeftCommand = new RelayCommand(ExecuteFeedbackArrowLeftCommand);
            FeedbackArrowRightCommand = new RelayCommand(ExecuteFeedbackArrowRightCommand);
            BookCommand = new RelayCommand(ExecuteBookCommand);
            UpdateFeedbackCommand = new RelayCommand(ExecuteUpdateFeedbackCommand);

            this.realty = realty;
            reltyModel = new RealtyModel(context);
            

            if (this.realty?.Images.Count <= 1) activeImageIndex = null;
            else activeImageIndex = 0;

            if (activeImageIndex == 0) ActiveImage = realty?.Images[(int)activeImageIndex!];
            else ActiveImage = null;

            
            this.access = access;

            BookingStartDate = DateTime.Now;
            BookingEndDate = DateTime.Now.AddDays(1);

            //if(realty != null) MessageBox.Show(realty.Feedbacks.Count + " | " + realty.Feedbacks.FirstOrDefault().UserAccess.Login);
        }
        private void ExecuteArrowRightCommand(object? obj)
        {
            if (activeImage != null)
            {
                activeImageIndex++;
                if (activeImageIndex > realty?.Images.Count - 1)
                {
                    activeImageIndex = 0;
                }
                ActiveImage = realty?.Images[(int)activeImageIndex!];
            }
        }
        private void ExecuteArrowLeftCommand(object? obj)
        {
            if (activeImageIndex != null)
            {
                activeImageIndex--;
                if (activeImageIndex < 0)
                {
                    activeImageIndex = realty?.Images.Count - 1;
                }
                ActiveImage = realty?.Images[(int)activeImageIndex!];
            }
            
        }
        private void ExecuteFeedbackArrowRightCommand(object? obj)
        {
            if(activeFeedbackIndex != -1)
            {
                activeFeedbackIndex++;
                if(activeFeedbackIndex > feedbacks.Count - 1)
                {
                    activeFeedbackIndex = 0;
                }
                ActiveFeedback = feedbacks[activeFeedbackIndex];
            }
        }
        private void ExecuteFeedbackArrowLeftCommand(object? obj)
        {
            if(activeFeedbackIndex != -1)
            {
                activeFeedbackIndex--;
                if(activeFeedbackIndex < 0)
                {
                    activeFeedbackIndex = feedbacks.Count - 1;
                }
                ActiveFeedback = feedbacks[activeFeedbackIndex!];
            }
        }
        private async void ExecuteBookCommand(object? obj)
        {
            if (realty != null)
            {
                BookingModel bookingModel = new(context);
                var result = await bookingModel.CreateBookingAsync(access.Id, realty.Id, startDate, endDate);
                CustomMessageBox.Show("System", result, MessageBoxButton.OK, IconChar.CircleInfo);
            }
        }
        private void ExecuteUpdateFeedbackCommand(object? obj)
        {
            if (access.Id != ActiveFeedback!.UserAccessId)
            {
                CustomMessageBox.Show("System", "You can't update this feedback", MessageBoxButton.OK, IconChar.CircleExclamation);
                return;
            }
            if (realty != null)
            {
                UpdateFeedbackView feedbackUpdate = new(ActiveFeedback!);
                feedbackUpdate.ShowDialog();
            }

            if (realty?.Feedbacks[activeFeedbackIndex].DeletedAt != null)
            {
                ArrowRightCommand.Execute(null);
            }
            else
            {
                ActiveFeedback = realty?.Feedbacks[activeFeedbackIndex];
            }
        }
        public async Task Window_LoadedAsync()
        {
            if (Realty != null)
            {
                var service = new NbuCurrencyRate();
                var eurRate = await service.GetCurrencyRateAsync();

                PriceUa = eurRate * (double)Realty.Price;

                if(realty?.AccRates != null) Rate = realty.AccRates.AvgRate;
                FutureBooking = await new RealtyModel(context).GetFutureBookingAsync(realty!.Slug!);

                feedbacks = await reltyModel.GetFeedbacksAsync(realty!.Slug!);

                if (feedbacks.Count == 0) activeFeedbackIndex = -1;
                else activeFeedbackIndex = 0;

                if (activeFeedbackIndex == 0) ActiveFeedback = feedbacks[activeFeedbackIndex];
                else ActiveFeedback = null;
            }
        }
    }
}
