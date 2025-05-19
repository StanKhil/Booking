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

            var userId = Guid.NewGuid();
            var realtyId = Guid.NewGuid();

            context.UserAccesses.Add(new UserAccess { Id = userId });
            context.Realties.Add(new Realty
            {
                Id = realtyId,
                Feedbacks = new List<Feedback>(),
                DeletedAt = null
            });
            await context.SaveChangesAsync();

            var model = new FeedbackModel();
            model.GetType().GetField("dataContext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(model, context);

            var result = await model.CreateFeedbackAsync(userId, realtyId, "Nice place", 5);
            Assert.IsTrue(result);
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
            var feedbackId = Guid.NewGuid();
            var realtyId = Guid.NewGuid();

            var feedback = new Feedback
            {
                Id = feedbackId,
                RealtyId = realtyId,
                DeletedAt = null
            };

            var realty = new Realty
            {
                Id = realtyId,
                DeletedAt = null,
                Feedbacks = new List<Feedback> { feedback }
            };

            context.Feedbacks.Add(feedback);
            context.Realties.Add(realty);
            await context.SaveChangesAsync();

            var model = new FeedbackModel();
            model.GetType().GetField("dataContext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(model, context);

            var result = await model.DeleteFeedbackAsync(feedbackId);
            Assert.IsTrue(result);

            var updated = await context.Feedbacks.FirstOrDefaultAsync(f => f.Id == feedbackId);
            Assert.IsNotNull(updated?.DeletedAt);
        }

        [TestMethod]
        public async Task DeleteFeedbackAsyncNotFound()
        {
            var context = new DataContext();
            var model = new FeedbackModel();
            model.GetType().GetField("dataContext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(model, context);

            var result = await model.DeleteFeedbackAsync(Guid.NewGuid());
            Assert.IsFalse(result);
        }
    }
}
