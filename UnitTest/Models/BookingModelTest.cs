using Booking.Data.Entities;
using Booking.Data;
using Booking.Models;
using Microsoft.EntityFrameworkCore;



namespace UnitTest.Models
{
    [TestClass]
    public class BookingModelTest
    {

        [TestMethod]
        public void ConstructorTest()
        {
            var context = new DataContext();
            var model = new BookingModel(context);
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public async Task CreateBookingAsync()
        {
            var context = new DataContext();
            var userId = Guid.NewGuid();
            var realtyId = Guid.NewGuid();

            context.UserAccesses.Add(new UserAccess { Id = userId });
            context.Realties.Add(new Realty
            {
                Id = realtyId,
                BookingItems = new List<BookingItem>(),
                DeletedAt = null
            });
            await context.SaveChangesAsync();

            var model = new BookingModel(context);
            var start = DateTime.Now.AddDays(1);
            var end = DateTime.Now.AddDays(3);

            var result = await model.CreateBookingAsync(userId, realtyId, start, end);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CreateBookingAsyncOverlappingDates()
        {
            var context = new DataContext();
            var userId = Guid.NewGuid();
            var realtyId = Guid.NewGuid();

            var existingBooking = new BookingItem
            {
                Id = Guid.NewGuid(),
                StartDate = DateTime.Today.AddDays(2),
                EndDate = DateTime.Today.AddDays(5),
                DeletedAt = null
            };

            var realty = new Realty
            {
                Id = realtyId,
                BookingItems = new List<BookingItem> { existingBooking },
                DeletedAt = null
            };

            context.UserAccesses.Add(new UserAccess { Id = userId });
            context.Realties.Add(realty);
            await context.SaveChangesAsync();

            var model = new BookingModel(context);

            var overlapStart = DateTime.Today.AddDays(3);
            var overlapEnd = DateTime.Today.AddDays(6);

            var result = await model.CreateBookingAsync(userId, realtyId, overlapStart, overlapEnd);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task DeleteBookingAsync()
        {
            var context = new DataContext();
            var bookingId = Guid.NewGuid();
            var realtyId = Guid.NewGuid();

            var booking = new BookingItem
            {
                Id = bookingId,
                RealtyId = realtyId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                DeletedAt = null
            };

            var realty = new Realty
            {
                Id = realtyId,
                BookingItems = new List<BookingItem> { booking },
                DeletedAt = null
            };

            context.BookingItems.Add(booking);
            context.Realties.Add(realty);
            await context.SaveChangesAsync();

            var model = new BookingModel(context);
            var result = await model.DeleteBookingAsync(bookingId);

            Assert.IsTrue(result);
            Assert.IsNotNull(context.BookingItems.First().DeletedAt);
        }
    }
}
