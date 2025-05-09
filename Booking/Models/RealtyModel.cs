using Booking.Data.Entities;
using Booking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel.DataAnnotations;

namespace Booking.Models
{
    public class RealtyModel
    {
        DataContext context;
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

        public bool CreateRealty(string name, string? description, string? slug, string? imageUrl, decimal price, string cityName, string countryName, string groupName)
        {
            if (context.Realties.Any(r => r.Slug == slug && r.DeletedAt == null))
            {
                System.Windows.MessageBox.Show("Slug already exists");
                return false;
            }

            var city = context.Cities.FirstOrDefault(c => c.Name == cityName) ?? new City { Id = Guid.NewGuid(), Name = cityName };
            var country = context.Countries.FirstOrDefault(c => c.Name == countryName) ?? new Country { Id = Guid.NewGuid(), Name = countryName };
            var group = context.RealtyGroups.FirstOrDefault(g => g.Name == groupName) ?? new RealtyGroup { Id = Guid.NewGuid(), Name = groupName };

            if (city.Id == Guid.Empty) context.Cities.Add(city);
            if (country.Id == Guid.Empty) context.Countries.Add(country);
            if (group.Id == Guid.Empty) context.RealtyGroups.Add(group);
            context.SaveChanges();

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
            context.SaveChanges();

            System.Windows.MessageBox.Show("Created", "System", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        public bool UpdateRealty(string slug, string? name, string? description, string? newSlug, string? imageUrl, decimal? price, string? cityName, string? countryName, string? groupName)
        {
            var realty = context.Realties.FirstOrDefault(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                System.Windows.MessageBox.Show("Realty not found");
                return false;
            }

            if (!string.IsNullOrEmpty(name)) realty.Name = name;
            if (description != null) realty.Description = description;
            if (!string.IsNullOrEmpty(newSlug)) realty.Slug = newSlug;
            if (imageUrl != null) realty.ImageUrl = imageUrl;
            if (price.HasValue) realty.Price = price.Value;

            if (!string.IsNullOrEmpty(cityName))
            {
                var city = context.Cities.FirstOrDefault(c => c.Name == cityName) ?? new City { Id = Guid.NewGuid(), Name = cityName };
                if (!context.Cities.Contains(city)) context.Cities.Add(city);
                realty.CityId = city.Id;
            }

            if (!string.IsNullOrEmpty(countryName))
            {
                var country = context.Countries.FirstOrDefault(c => c.Name == countryName) ?? new Country { Id = Guid.NewGuid(), Name = countryName };
                if (!context.Countries.Contains(country)) context.Countries.Add(country);
                realty.CountryId = country.Id;
            }

            if (!string.IsNullOrEmpty(groupName))
            {
                var group = context.RealtyGroups.FirstOrDefault(g => g.Name == groupName) ?? new RealtyGroup { Id = Guid.NewGuid(), Name = groupName };
                if (!context.RealtyGroups.Contains(group)) context.RealtyGroups.Add(group);
                realty.GroupId = group.Id;
            }

            context.SaveChanges();
            System.Windows.MessageBox.Show("Updated", "System", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        public bool DeleteRealty(string slug)
        {
            var realty = context.Realties.FirstOrDefault(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                System.Windows.MessageBox.Show("Realty not found");
                return false;
            }

            realty.DeletedAt = DateTime.Now;
            context.SaveChanges();
            System.Windows.MessageBox.Show("Deleted", "System", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        public List<Realty> GetRealties()
        {
            return context.Realties.Where(r => r.DeletedAt == null).ToList();
        }

        public Realty? GetRealtyBySlug(string slug)
        {
            return context.Realties.FirstOrDefault(r => r.Slug == slug && r.DeletedAt == null);
        }

        public List<Feedback> GetFeedbacks(string slug)
        {
            var realty = context.Realties.FirstOrDefault(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                System.Windows.MessageBox.Show("Realty not found");
                return new List<Feedback>();
            }
            return context.Feedbacks.Where(f => f.RealtyId == realty.Id).ToList();
        }

        public List<Realty> GetRealtiesByGroup(string groupName)
        {
            var group = context.RealtyGroups.FirstOrDefault(g => g.Name == groupName);
            if (group == null)
            {
                System.Windows.MessageBox.Show("Group not found");
                return new List<Realty>();
            }
            return context.Realties.Where(r => r.GroupId == group.Id && r.DeletedAt == null).ToList();
        }

        public List<Realty> GetRealtiesByCity(string cityName)
        {
            var city = context.Cities.FirstOrDefault(c => c.Name == cityName);
            if (city == null)
            {
                System.Windows.MessageBox.Show("City not found");
                return new List<Realty>();
            }
            return context.Realties.Where(r => r.CityId == city.Id && r.DeletedAt == null).ToList();
        }

        public List<Realty> GetRealtiesByCountry(string countryName)
        {
            var country = context.Countries.FirstOrDefault(c => c.Name == countryName);
            if (country == null)
            {
                System.Windows.MessageBox.Show("Country not found");
                return new List<Realty>();
            }
            return context.Realties.Where(r => r.CountryId == country.Id && r.DeletedAt == null).ToList();
        }

        public List<Realty> GetRealtiesByPrice(decimal minPrice, decimal maxPrice)
        {
            return context.Realties.Where(r => r.Price >= minPrice && r.Price <= maxPrice && r.DeletedAt == null).ToList();
        }

        public List<ItemImage> GetImages(string slug)
        {
            var realty = context.Realties.FirstOrDefault(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                System.Windows.MessageBox.Show("Realty not found");
                return new List<ItemImage>();
            }
            return context.ItemImages.Where(i => i.ItemId == realty.Id).ToList();
        }

        public List<BookingItem> GetBookings(string slug)
        {
            var realty = context.Realties.FirstOrDefault(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                System.Windows.MessageBox.Show("Realty not found");
                return new List<BookingItem>();
            }
            return context.BookingItems.Where(b => b.RealtyId == realty.Id).ToList();
        }

        public float GetAvgRate(string slug)
        {
            var realty = context.Realties.FirstOrDefault(r => r.Slug == slug && r.DeletedAt == null);
            if (realty == null)
            {
                System.Windows.MessageBox.Show("Realty not found");
                return 0;
            }
            var accRate = context.AccRates.FirstOrDefault(ar => ar.RealtyId == realty.Id);
            if (accRate == null)
            {
                return 0;
            }

            return accRate.AvgRate;
        }
    }
}
