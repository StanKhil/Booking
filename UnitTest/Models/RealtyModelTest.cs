using Booking.Data;
using Booking.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Data.Entities;

namespace UnitTest.Models
{
    [TestClass]
    public sealed class RealtyModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            DataContext context = new DataContext();
            RealtyModel model = new RealtyModel(context);
            Assert.IsNotNull(model);
            Assert.IsNull(model.realty);
        }

        [TestMethod]
        public void CreateRealtyAsyncTest()
        {
            DataContext context = new DataContext();
            RealtyModel model = new(context);

            var result = model.CreateRealtyAsync("", "Description", "Slug", "ImageUrl", (decimal)10.10, "City", "Country", "Group");
            Assert.IsInstanceOfType<Task<string>>(result);
            Assert.IsFalse(result.Result == "Created"); // As empty string was passed

        }
        [TestMethod]
        public void UpdateRealtyAsyncTest()
        {
            DataContext context = new DataContext();
            RealtyModel model = new(context);

            var result = model.UpdateRealtyAsync("", "Name", "Description", "NewSlug", "ImageUrl", (decimal)10.10, "City", "Country", "Group");
            Assert.IsInstanceOfType<Task<string>>(result);
            Assert.IsFalse(result.Result == "Updated"); // As empty string was passed

        }
        [TestMethod]
        public void DeleteRealtyAsyncTest()
        {
            DataContext context = new DataContext();
            RealtyModel model = new(context);

            var result = model.DeleteRealtyAsync("");
            Assert.IsInstanceOfType<Task<bool>>(result);
            Assert.IsFalse(result.Result); // As empty string was passed

        }
        [TestMethod]
        public void GetRealtiesAsyncTest()
        {
            var context = new DataContext();
            RealtyModel model = new(context);
            var result = model.GetRealtiesAsync();

            Assert.IsInstanceOfType<Task<List<Realty>>>(result);
        }
        [TestMethod]
        public void GetRealtyBySlugAsyncTest()
        {
            DataContext context = new();
            RealtyModel model = new(context);
            var realty = model.GetRealtyBySlugAsync("hotel-forest");

            Assert.IsInstanceOfType<Task<Realty?>>(realty);
        }
        [TestMethod]
        public void GetFeedbacksAsyncTest()
        {
            var context = new DataContext();
            RealtyModel model = new(context);
            var result = model.GetFeedbacksAsync("hotel-forest");

            Assert.IsInstanceOfType<Task<List<Feedback>>>(result);
        }
        [TestMethod]
        public void GetRealtiesByGroupAsyncTest()
        {
            DataContext context = new();
            RealtyModel model = new(context);
            var realty = model.GetRealtiesByGroupAsync("Group");

            Assert.IsInstanceOfType<Task<List<Realty>>>(realty);
        }
        [TestMethod]
        public void GetRealtiesByCityAsyncTest()
        {
            DataContext context = new();
            RealtyModel model = new(context);
            var realty = model.GetRealtiesByCityAsync("City");

            Assert.IsInstanceOfType<Task<List<Realty>>>(realty);
        }
        [TestMethod]
        public void GetRealtiesByCountryAsyncTest()
        {
            DataContext context = new();
            RealtyModel model = new(context);
            var realty = model.GetRealtiesByCountryAsync("Name");

            Assert.IsInstanceOfType<Task<List<Realty>>>(realty);
        }
        [TestMethod]
        public void GetRealtiesByPriceAsyncTest()
        {
            DataContext context = new();
            RealtyModel model = new(context);
            var realty = model.GetRealtiesByPriceAsync(0, (decimal)10.00);

            Assert.IsInstanceOfType<Task<List<Realty>>>(realty);
        }
        [TestMethod]
        public void GetImagesAsyncTest()
        {
            DataContext context = new();
            RealtyModel model = new(context);
            var realty = model.GetImagesAsync("Slug");

            Assert.IsInstanceOfType<Task<List<ItemImage>>>(realty);
        }
        [TestMethod]
        public void GetBookingsAsyncTest()
        {
            DataContext context = new();
            RealtyModel model = new(context);
            var realty = model.GetBookingsAsync("Slug");

            Assert.IsInstanceOfType<Task<List<BookingItem>>>(realty);
        }
        [TestMethod]
        public void GetFutureBookingAsyncTest()
        {
            DataContext context = new();
            RealtyModel model = new(context);
            var realty = model.GetFutureBookingAsync("Slug");

            Assert.IsInstanceOfType<Task<List<BookingItem>>>(realty);
        }
        [TestMethod]
        public void GetAvgRateAsyncTest()
        {
            DataContext context = new();
            RealtyModel model = new(context);
            var realty = model.GetAvgRateAsync("Slug");

            Assert.IsInstanceOfType<Task<float>>(realty);
        }
        [TestMethod]
        public void GetRealtiesSortedByPriceAsyncTest()
        {
            DataContext context = new();
            RealtyModel model = new(context);
            var realty = model.GetRealtiesSortedByPriceAsync(new List<Realty>());

            Assert.IsInstanceOfType<Task<List<Realty>>>(realty);
        }
        [TestMethod]
        public void GetRealtiesSortedByRatingAsyncTest()
        {
            DataContext context = new();
            RealtyModel model = new(context);
            var realty = model.GetRealtiesSortedByRatingAsync(new List<Realty>());

            Assert.IsInstanceOfType<Task<List<Realty>>>(realty);
        }
        [TestMethod]
        public void GetRealtiesByFilterAsyncTest()
        {
            DataContext context = new();
            RealtyModel model = new(context);
            var realty = model.GetRealtiesByFilterAsync("City", "Country", "Group");

            Assert.IsInstanceOfType<Task<List<Realty>>>(realty);
        }
    }
}
