using Booking.Data.Entities;
using Booking.Data;
using Booking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Models
{
    [TestClass]
    public class FeedbackModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var model = new FeedbackModel();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public async Task CreateFeedbackAsync()
        {
            var context = new DataContext();

            var userId = Guid.Parse("92cd36b8-ea5a-4cbb-a232-268d942c97fd");
            var realtyId = Guid.Parse("37dcc68e-b7e7-4b55-b04e-147c1a4126b7");


            var model = new FeedbackModel();
            /*model.GetType().GetField("dataContext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(model, context);*/

            var result = await model.CreateFeedbackAsync(userId, realtyId, "Nice place", 5);
            Assert.IsTrue(result);


            await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => model.CreateFeedbackAsync(userId, realtyId, "Nice place", 6),
                "Rate must be between 1 and 5"
            );
        }

        [TestMethod]
        public async Task CreateFeedbackAsyncEmptyMessage()
        {
            var model = new FeedbackModel();
            var result = await model.CreateFeedbackAsync(Guid.NewGuid(), Guid.NewGuid(), "", 4);
            Assert.IsFalse(result);
        }



        [TestMethod]
        public async Task DeleteFeedbackAsync()
        {
            var context = new DataContext();
            var feedbackId = Guid.Parse("e83d47e2-58c9-4fae-9dd2-298f64eee96f");
            var realtyId = Guid.Parse("37dcc68e-b7e7-4b55-b04e-147c1a4126b7");

            var model = new FeedbackModel();
            /*model.GetType().GetField("dataContext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(model, context);*/

            //var result = await model.DeleteFeedbackAsync(feedbackId);
            //Assert.IsTrue(result);

            var updated = await context.Feedbacks.FirstOrDefaultAsync(f => f.Id == feedbackId);
            Assert.IsNotNull(updated?.DeletedAt);


            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await model.DeleteFeedbackAsync(Guid.NewGuid()));

            Assert.AreEqual("Feedback not found", ex.Message);
        }

        [TestMethod]
        public async Task DeleteFeedbackAsyncNotFound()
        {
            var context = new DataContext();
            var model = new FeedbackModel();
            /*model.GetType().GetField("dataContext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(model, context);*/

            var result = await model.DeleteFeedbackAsync(Guid.NewGuid());
            Assert.IsFalse(result);
        }
    }
}
