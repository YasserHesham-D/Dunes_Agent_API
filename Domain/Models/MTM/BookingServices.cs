using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.MTM
{
    public class BookingServices
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; } = null!;
        public Guid LocationId { get; set; }
        public virtual Location Location { get; set; } = null!;
        public Guid BookingId { get; set; }

        public virtual Booking Booking { get; set; } = null!;

        public int KidsCount { get; set; }
        public decimal KidsTotalPrice { get; set; }
        public int ChildsCount { get; set; }
        public decimal ChildsTotalPrice { get; set; }
        public int AdultsCount { get; set; }
        public decimal AdultsTotalPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
