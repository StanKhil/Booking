using Booking.Data;
using Booking.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
                System.Windows.MessageBox.Show("Message cannot be empty");
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
            };

            Realty? realty = null;
            try
            {
                realty = await dataContext.Realties
                .Include(r => r.Feedbacks)
                .FirstOrDefaultAsync(r => r.Id == realtyId && r.DeletedAt == null);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                return false;
            }

            if (realty == null)
            {
                System.Windows.MessageBox.Show("Realty not found");
                return false;
            }

            Feedback!.UserAccess = await dataContext!.UserAccesses!
                .Include(ua => ua.Feedbacks)!
                .FirstOrDefaultAsync(ua => ua.Id == userAccessId)!;
            Feedback.Realty = realty;
            realty.Feedbacks.Add(Feedback);
            dataContext.Feedbacks.Add(Feedback);
            await dataContext.SaveChangesAsync();
            System.Windows.MessageBox.Show("Feedback created");
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
                System.Windows.MessageBox.Show("Feedback not found");
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
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                return false;
            }

            System.Windows.MessageBox.Show("Feedback deleted");
            return true;
        }
    }
}
