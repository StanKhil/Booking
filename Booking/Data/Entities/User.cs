using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
