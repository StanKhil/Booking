using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Entities
{
    public class Realty
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public String Name { get; set; } = null!;
        public String? Description { get; set; }
        public String? Slug { get; set; }
        public String? ImageUrl { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid CityId { get; set; }


        public City City { get; set; } = null!;
        public List<BookingItem> BookingItems { get; set; } = [];
        public RealtyGroup RealtyGroup { get; set; } = null!;
        public List<ItemImage> Images { get; set; } = [];
        public List<Feedback> Feedbacks { get; set; } = [];
    }
}
