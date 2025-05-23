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
            var userId = Guid.Parse("92cd36b8-ea5a-4cbb-a232-268d942c97fd");
            var realtyId = Guid.Parse("37dcc68e-b7e7-4b55-b04e-147c1a4126b7");

            

            var model = new BookingModel(context);
            var start = DateTime.Now.AddDays(1);
            var end = DateTime.Now.AddDays(3);

            var result = await model.CreateBookingAsync(userId, realtyId, start, end);

            Assert.ThrowsException<ArgumentException>(
                () => model.CreateBookingAsync(userId, realtyId, end, start),
                "Start date is greater than finish date"
            );

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CreateBookingAsyncOverlappingDates()
        {
            var context = new DataContext();
            var userId = Guid.Parse("92cd36b8-ea5a-4cbb-a232-268d942c97fd");
            var realtyId = Guid.Parse("37dcc68e-b7e7-4b55-b04e-147c1a4126b7");

            
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
            var bookingId = Guid.Parse("005e900a-9d2e-4227-9b10-0047f638fa0e");

            var model = new BookingModel(context);
            var result = await model.DeleteBookingAsync(bookingId);

            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await model.DeleteBookingAsync(Guid.NewGuid()),
                "Booking not found"
            );

            Assert.AreEqual("Booking not found", ex.Message);

            //Assert.IsTrue(result);
            //Assert.IsNotNull(context.BookingItems.First().DeletedAt);
        }
    }
}
