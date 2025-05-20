using Booking.Data;
using Booking.Data.Entities;
using Booking.Models;
using Booking.Services.CurrencyRate;
using System.Windows;
using System.Windows.Input;

namespace Booking.ViewModels
{
    class ItemViewModel : ViewModel
    {
        private DataContext context = new();
        UserAccess access;
        private Realty? realty;
        private ItemImage? activeImage;
        private int? activeImageIndex;
        private Feedback? activeFeedback;
        private int? activeFeedbackIndex;

        private List<BookingItem> futureBooking;

        private double priceUa;
        private float rate;

        private DateTime startDate;
        private DateTime endDate;

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
        public ItemViewModel() : this(null, null) { }
        public ItemViewModel(Realty? realty, UserAccess access)
        {
            ArrowLeftCommand = new RelayCommand(ExecuteArrowLeftCommand);
            ArrowRightCommand = new RelayCommand(ExecuteArrowRightCommand);
            FeedbackArrowLeftCommand = new RelayCommand(ExecuteFeedbackArrowLeftCommand);
            FeedbackArrowRightCommand = new RelayCommand(ExecuteFeedbackArrowRightCommand);
            BookCommand = new RelayCommand(ExecuteBookCommand);

            this.realty = realty;


            if (this.realty?.Images.Count <= 1) activeImageIndex = null;
            else activeImageIndex = 0;

            if (activeImageIndex == 0) ActiveImage = realty?.Images[(int)activeImageIndex!];
            else ActiveImage = null;

            if (this.realty?.Feedbacks.Count == 0) activeFeedbackIndex = null;
            else activeFeedbackIndex = 0;

            if (activeFeedbackIndex == 0) ActiveFeedback = realty?.Feedbacks[(int)activeFeedbackIndex];
            else ActiveFeedback = null;
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
            if(activeFeedbackIndex != null)
            {
                activeFeedbackIndex++;
                if(activeFeedbackIndex > realty?.Feedbacks.Count - 1)
                {
                    activeFeedbackIndex = 0;
                }
                ActiveFeedback = realty?.Feedbacks[(int)activeFeedbackIndex];
            }
        }
        private void ExecuteFeedbackArrowLeftCommand(object? obj)
        {
            if(activeFeedbackIndex != null)
            {
                activeFeedbackIndex--;
                if(activeFeedbackIndex < 0)
                {
                    activeFeedbackIndex = realty?.Feedbacks.Count - 1;
                }
                ActiveFeedback = realty?.Feedbacks[(int)activeFeedbackIndex!];
            }
        }

        private async void ExecuteBookCommand(object? obj)
        {
            if (realty != null)
            {
                BookingModel bookingModel = new(context);
                var result = await bookingModel.CreateBookingAsync(access.Id, realty.Id, startDate, endDate);
                //if (result) MessageBox.Show("Booking created successfully");
                //else MessageBox.Show("Booking failed");
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
            }
        }
    }
}
