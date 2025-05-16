using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Booking.Data;
using Booking.Services.CurrencyRate;
using Booking.Data.Entities;


namespace Booking.Models
{
    public class RealtyCardModel
    {
        DataContext context;
        RealtyModel realtyModel;
        ICurrencyRate currencyRate = new NbuCurrencyRate();

        public RealtyCardModel(DataContext context, RealtyModel realtyModel)
        {
            this.context = context;
            this.realtyModel = realtyModel;
        }

        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public String Name { get; set; } = null!;
        public String? Description { get; set; }
        public String? Slug { get; set; }
        public String? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid CityId { get; set; }
        public Guid CountryId { get; set; }

        public string ImagePath
        {
            get
            {
                var projectDir = Path.GetFullPath(Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

                var fullPath = Path.Combine(
                    projectDir,
                    "Images",
                    Slug ?? "",
                    ImageUrl ?? ""
                );

                return fullPath;
            }
        }

        public string? Country
        {
            get => context.Countries
                .Where(c => c.Id == CountryId)
                .Select(c => c.Name).FirstOrDefault();
        }

        public string? City
        {
            get => context.Cities
                .Where(c => c.Id == CityId)
                .Select(c => c.Name).FirstOrDefault();
        }

        public float Rate
        {
            get => context.AccRates
                .Where(a => a.RealtyId == Id)
                .Select(a => a.AvgRate)
                .FirstOrDefault();
        }

        public double PriceUa
        {
            get => (currencyRate.GetCurrencyRateAsync().Result) * (double)Price;
        }

        public List<Feedback> Feedbacks
        {
            get => realtyModel.GetFeedbacksAsync(Slug!).Result;
        }

        public List<ItemImage> Images
        {
            get => realtyModel.GetImagesAsync(Slug!).Result;
        }

        public List<BookingItem> Bookings
        {
            get => realtyModel.GetBookingsAsync(Slug!).Result;
        }
    }
}
