using Booking.Data;
using Booking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using FontAwesome.Sharp;
using System.Windows;


namespace Booking.Models
{
    public class BookingModel
    {
        DataContext context;
        public BookingItem? BookingItem;

        public BookingModel(DataContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateBookingAsync(Guid userAccessId, Guid realtyId, DateTime start, DateTime finish)
        {
            if (start == null || finish == null)
            {
                CustomMessageBox.Show("System", "Start or finish date is empty", MessageBoxButton.OK, IconChar.ExclamationCircle);
                return false;
            }

            if (start >= finish)
            {
                CustomMessageBox.Show("System", "Start date is greater than finish date", MessageBoxButton.OK, IconChar.ExclamationCircle);
                return false;
            }

            var realty = await context.Realties
                .Include(r => r.BookingItems)
                .FirstOrDefaultAsync(r => r.Id == realtyId && r.DeletedAt == null);

            if (realty == null)
            {
                CustomMessageBox.Show("System", "Realty not found", MessageBoxButton.OK, IconChar.ExclamationCircle);
                return false;
            }

            bool hasOverlap = realty.BookingItems.Any(b =>
                b.DeletedAt == null &&
                ((start >= b.StartDate && start < b.EndDate) ||
                 (finish > b.StartDate && finish <= b.EndDate) ||
                 (start <= b.StartDate && finish >= b.EndDate)));

            if (hasOverlap)
            {
                CustomMessageBox.Show("System", "This realty is already booked for the selected dates.", MessageBoxButton.OK, IconChar.ExclamationCircle);
                return false;
            }

            BookingItem bookingItem = new BookingItem()
            {
                Id = Guid.NewGuid(),
                RealtyId = realty.Id,
                StartDate = start,
                EndDate = finish,
                CreatedAt = DateTime.Now,
                DeletedAt = null,
                UserAccessId = userAccessId
            };

            bookingItem.UserAccess = await context.UserAccesses
                .Include(ua => ua.BookingItems)
                .FirstOrDefaultAsync(ua => ua.Id == userAccessId);
            bookingItem.Realty = realty;
            realty.BookingItems.Add(bookingItem);
            context.BookingItems.Add(bookingItem);
            await context.SaveChangesAsync();
            CustomMessageBox.Show("System", "Booking created successfully", MessageBoxButton.OK, IconChar.CircleInfo);
            return true;
        }

        public async Task<bool> DeleteBookingAsync(Guid bookingId)
        {
            var bookingItem = await context.BookingItems
                .Include(b => b.Realty)
                .FirstOrDefaultAsync(b => b.Id == bookingId && b.DeletedAt == null);

            var realty = await context.Realties
                .Include(r => r.BookingItems)
                .FirstOrDefaultAsync(r => r.Id == bookingItem.RealtyId && r.DeletedAt == null);
            if (bookingItem == null)
            {
                CustomMessageBox.Show("System", "Booking not found", MessageBoxButton.OK, IconChar.ExclamationCircle);
                return false;
            }
            if(bookingItem.StartDate.AddDays(-3) < DateTime.Now)
            {
                CustomMessageBox.Show("System", "Booking cannot be deleted less than 3 days before the start date", MessageBoxButton.OK, IconChar.ExclamationCircle);
                return false;
            }

            bookingItem.DeletedAt = DateTime.Now;
            //realty.BookingItems.Remove(bookingItem);
            await context.SaveChangesAsync();
            CustomMessageBox.Show("System", "Booking deleted successfully", MessageBoxButton.OK, IconChar.CircleInfo);
            return true;
        }
    }
}
