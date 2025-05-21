using Booking.Data;
using Booking.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontAwesome.Sharp;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace Booking.Models
{
    public class FeedbackModel
    {
        DataContext dataContext;
        public Feedback? Feedback;

        public FeedbackModel()
        {
            dataContext = new DataContext();
        }

        public async Task<bool> CreateFeedbackAsync(Guid userAccessId, Guid realtyId, string message, int rate)
        {
            if(string.IsNullOrEmpty(message))
            {
                CustomMessageBox.Show("System", "Message cannot be empty", MessageBoxButton.OK, IconChar.CircleExclamation);
                return false;
            }

            Feedback = new Feedback()
            {
                Id = Guid.NewGuid(),
                RealtyId = realtyId,
                UserAccessId = userAccessId,
                Text = message,
                Rate = rate,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                DeletedAt = null
            }!;

            Realty? realty = null!;
            try
            {
                realty = await dataContext.Realties
                .Include(r => r.Feedbacks)
                .FirstOrDefaultAsync(r => r.Id == realtyId && r.DeletedAt == null);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("System", "Error: " + ex.Message, MessageBoxButton.OK, IconChar.TriangleExclamation);
                return false;
            }

            if (realty == null)
            {
                CustomMessageBox.Show("System","Realty not found", MessageBoxButton.OK, IconChar.TriangleExclamation);
                return false;
            }

            Feedback!.UserAccess = await dataContext!?.UserAccesses!?
                .Include(ua => ua!.Feedbacks!)!?
                .FirstOrDefaultAsync(ua => ua!.Id! == userAccessId!)!;
            Feedback.Realty = realty;
            realty.Feedbacks.Add(Feedback);
            dataContext.Feedbacks.Add(Feedback);
            await dataContext.SaveChangesAsync();
            CustomMessageBox.Show("System", "Feedback created", MessageBoxButton.OK, IconChar.TriangleExclamation);
            return true;
        }

        public async Task<bool> UpdateFeedbackAsync(Guid feedbackId, string message, int rate)
        {
            var feedback = await dataContext.Feedbacks
                .FirstOrDefaultAsync(f => f.Id == feedbackId && f.DeletedAt == null);
            if (feedback == null)
            {
                CustomMessageBox.Show("System", "Feedback not found", MessageBoxButton.OK, IconChar.CircleExclamation);
                return false;
            }
            feedback.Text = message;
            feedback.Rate = rate;
            feedback.UpdatedAt = DateTime.Now;
            try
            {
                await dataContext.Database.ExecuteSqlInterpolatedAsync(
    $"UPDATE Feedbacks SET Text = {message}, Rate = {rate}, UpdatedAt = {DateTime.Now} WHERE Id = {feedbackId}");

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("System", "Error: " + ex.Message, MessageBoxButton.OK, IconChar.TriangleExclamation);
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteFeedbackAsync(Guid feedbackId)
        {
            var feedback = await dataContext.Feedbacks
                .FirstOrDefaultAsync(f => f.Id == feedbackId && f.DeletedAt == null);
            var realty = await dataContext.Realties
                    .Include(r => r.Feedbacks)
                    .FirstOrDefaultAsync(r => r.Id == feedback!.RealtyId && r.DeletedAt == null);
            

            if (feedback == null)
            {
               CustomMessageBox.Show("System","Feedback not found", MessageBoxButton.OK, IconChar.CircleExclamation);
                return false;
            }
            feedback.DeletedAt = DateTime.Now;
            realty?.Feedbacks.Remove(feedback);

            try
            {
                //await dataContext.SaveChangesAsync();
                await dataContext.Database.ExecuteSqlRawAsync("UPDATE Feedbacks SET DeletedAt = {0} WHERE Id = {1}", DateTime.Now, feedbackId);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("System", "Error: " + ex.Message, MessageBoxButton.OK, IconChar.TriangleExclamation);
                return false;
            }
            return true;
        }
    }
}
