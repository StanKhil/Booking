using Booking.Data.Entities;
using Booking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Booking.Models
{
    public class RealtyModel
    {
        private readonly DataContext context;
        public Realty? realty = null;

        public RealtyModel(DataContext context)
        {
            this.context = context;
        }

        public RealtyModel(DataContext context, string slug)
        {
            this.context = context;
            realty = context.Realties.FirstOrDefault(r => r.Slug == slug && r.DeletedAt == null);
        }

        public async Task<bool> CreateRealtyAsync(string? name, string? description, string? slug, string? imageUrl, decimal price, string? cityName, string? countryName, string? groupName)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(slug) || string.IsNullOrEmpty(cityName) || string.IsNullOrEmpty(countryName) || string.IsNullOrEmpty(groupName))
            {
                MessageBox.Show("All fields are required");
                return false;
            }
            if (await context.Realties.AnyAsync(r => r.Slug == slug && r.DeletedAt == null))
            {
                MessageBox.Show("Slug already exists");
                return false;
            }

            var city = await context.Cities.FirstOrDefaultAsync(c => c.Name == cityName) ?? new City { Id = Guid.NewGuid(), Name = cityName };
            var country = await context.Countries.FirstOrDefaultAsync(c => c.Name == countryName) ?? new Country { Id = Guid.NewGuid(), Name = countryName };
            var group = await context.RealtyGroups.FirstOrDefaultAsync(g => g.Name == groupName) ?? new RealtyGroup { Id = Guid.NewGuid(), Name = groupName };

            if (!await context.Cities.AnyAsync(c => c.Id == city.Id)) context.Cities.Add(city);
            if (!await context.Countries.AnyAsync(c => c.Id == country.Id)) context.Countries.Add(country);
            if (!await context.RealtyGroups.AnyAsync(g => g.Id == group.Id)) context.RealtyGroups.Add(group);
            await context.SaveChangesAsync();

            Realty realty = new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Slug = slug,
                ImageUrl = imageUrl,
                Price = price,
                CityId = city.Id,
                CountryId = country.Id,
                GroupId = group.Id
            };

            context.Realties.Add(realty);
            await context.SaveChangesAsync();

            MessageBox.Show("Created", "System", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        public async Task<bool> UpdateRealtyAsync(string slug, string? name, string? description, string? newSlug, string? imageUrl, decimal? price, string? cityName, string? countryName, string? groupName)
        {
            var realty = await context.Realties.FirstOrDefaultAsync(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                MessageBox.Show("Realty not found");
                return false;
            }

            if (!string.IsNullOrEmpty(name)) realty.Name = name;
            if (description != null) realty.Description = description;
            if (!string.IsNullOrEmpty(newSlug)) realty.Slug = newSlug;
            if (imageUrl != null) realty.ImageUrl = imageUrl;
            if (price.HasValue) realty.Price = price.Value;

            if (!string.IsNullOrEmpty(cityName))
            {
                var city = await context.Cities.FirstOrDefaultAsync(c => c.Name == cityName) ?? new City { Id = Guid.NewGuid(), Name = cityName };
                if (!await context.Cities.AnyAsync(c => c.Id == city.Id)) context.Cities.Add(city);
                realty.CityId = city.Id;
            }

            if (!string.IsNullOrEmpty(countryName))
            {
                var country = await context.Countries.FirstOrDefaultAsync(c => c.Name == countryName) ?? new Country { Id = Guid.NewGuid(), Name = countryName };
                if (!await context.Countries.AnyAsync(c => c.Id == country.Id)) context.Countries.Add(country);
                realty.CountryId = country.Id;
            }

            if (!string.IsNullOrEmpty(groupName))
            {
                var group = await context.RealtyGroups.FirstOrDefaultAsync(g => g.Name == groupName) ?? new RealtyGroup { Id = Guid.NewGuid(), Name = groupName };
                if (!await context.RealtyGroups.AnyAsync(g => g.Id == group.Id)) context.RealtyGroups.Add(group);
                realty.GroupId = group.Id;
            }

            await context.SaveChangesAsync();

            MessageBox.Show("Updated", "System", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        public async Task<bool> DeleteRealtyAsync(string slug)
        {
            var realty = await context.Realties.FirstOrDefaultAsync(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                MessageBox.Show("Realty not found");
                return false;
            }

            realty.DeletedAt = DateTime.Now;
            await context.SaveChangesAsync();

            MessageBox.Show("Deleted", "System", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        public async Task<List<Realty>> GetRealtiesAsync()
        {
            return await context.Realties.Where(r => r.DeletedAt == null).ToListAsync();
        }

        public async Task<Realty?> GetRealtyBySlugAsync(string slug)
        {
            return await context.Realties.FirstOrDefaultAsync(r => r.Slug == slug && r.DeletedAt == null);
        }

        public async Task<List<Feedback>> GetFeedbacksAsync(string slug)
        {
            var realty = await context.Realties.FirstOrDefaultAsync(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                MessageBox.Show("Realty not found");
                return new List<Feedback>();
            }
            return await context.Feedbacks.Where(f => f.RealtyId == realty.Id && f.DeletedAt == null).ToListAsync();
        }

        public async Task<List<Realty>> GetRealtiesByGroupAsync(string groupName)
        {
            var group = await context.RealtyGroups.FirstOrDefaultAsync(g => g.Name == groupName);
            if (group == null)
            {
                MessageBox.Show("Group not found");
                return new List<Realty>();
            }
            return await context.Realties.Where(r => r.GroupId == group.Id && r.DeletedAt == null).ToListAsync();
        }

        public async Task<List<Realty>> GetRealtiesByCityAsync(string cityName)
        {
            var city = await context.Cities.FirstOrDefaultAsync(c => c.Name == cityName);
            if (city == null)
            {
                MessageBox.Show("City not found");
                return new List<Realty>();
            }
            return await context.Realties.Where(r => r.CityId == city.Id && r.DeletedAt == null).ToListAsync();
        }

        public async Task<List<Realty>> GetRealtiesByCountryAsync(string countryName)
        {
            var country = await context.Countries.FirstOrDefaultAsync(c => c.Name == countryName);
            if (country == null)
            {
                MessageBox.Show("Country not found");
                return new List<Realty>();
            }
            return await context.Realties.Where(r => r.CountryId == country.Id && r.DeletedAt == null).ToListAsync();
        }

        public async Task<List<Realty>> GetRealtiesByPriceAsync(decimal minPrice, decimal maxPrice)
        {
            return await context.Realties.Where(r => r.Price >= minPrice && r.Price <= maxPrice && r.DeletedAt == null).ToListAsync();
        }

        public async Task<List<ItemImage>> GetImagesAsync(string slug)
        {
            var realty = await context.Realties.FirstOrDefaultAsync(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                MessageBox.Show("Realty not found");
                return new List<ItemImage>();
            }
            return await context.ItemImages.Where(i => i.ItemId == realty.Id).ToListAsync();
        }

        public async Task<List<BookingItem>> GetBookingsAsync(string slug)
        {
            var realty = await context.Realties.FirstOrDefaultAsync(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                MessageBox.Show("Realty not found");
                return new List<BookingItem>();
            }
            return await context.BookingItems.Where(b => b.RealtyId == realty.Id).ToListAsync();
        }

        public async Task<List<BookingItem>> GetFutureBookingAsync(string slug)
        {
            var realty = await context.Realties.FirstOrDefaultAsync(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                MessageBox.Show("Realty not found");
                return new List<BookingItem>();
            }
            var bookings = await context.BookingItems.Where(b => b.RealtyId == realty.Id).ToListAsync();
            var futureBookings = new List<BookingItem>();

            foreach (var booking in bookings)
            {
                if (booking.StartDate > DateTime.Now)
                {
                    futureBookings.Add(booking);
                }
            }

            return futureBookings;
        }

        public async Task<float> GetAvgRateAsync(string slug)
        {
            var realty = await context.Realties.FirstOrDefaultAsync(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                MessageBox.Show("Realty not found");
                return 0;
            }

            var accRate = await context.AccRates.FirstOrDefaultAsync(ar => ar.RealtyId == realty.Id);
            return accRate?.AvgRate ?? 0;
        }

        public async Task<List<Realty>> GetRealtiesSortedByPriceAsync(bool ascending = true)
        {
            return ascending
                ? await context.Realties
                .Where(r => r.DeletedAt == null)
                .OrderBy(r => r.Price).ToListAsync()
                : await context.Realties
                .Where(r => r.DeletedAt == null)
                .OrderByDescending(r => r.Price).ToListAsync();
        }

        public async Task<List<Realty>> GetRealtiesSortedByRatingAsync(bool ascending = false)
        {
            return ascending
                ? await context.Realties
                    .Where(r => r.DeletedAt == null)
                    .OrderBy(r => context.AccRates.FirstOrDefault(ar => ar.RealtyId == r.Id).AvgRate)
                    .ToListAsync()
                : await context.Realties
                    .Where(r => r.DeletedAt == null)
                    .OrderByDescending(r => context.AccRates.FirstOrDefault(ar => ar.RealtyId == r.Id).AvgRate)
                    .ToListAsync();
        }
    }
}
