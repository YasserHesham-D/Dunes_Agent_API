using Domain.Models.MTM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Booking
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public string PhoneNumber { get; set; } = null!;
        public string GuestName { get; set; } = null!;
       

        public string? Room { get; set; }

        public string? PickUpStatus { get; set; }

        public string? TicketNumber { get; set; }

        public string? OrderNumber { get; set; }

        public DateTime PickUpDate { get; set; }
        public bool IsConfirmed { get; set; }

        public Guid DriverId { get; set; }

        public virtual Driver Driver { get; set; } = null!;
        public Guid HotelId { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
        public Guid CurrencyId { get; set; }

        public virtual Currency Currency { get; set; } = null!;
        public Guid PaymentMethodId { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; } = null!;
        public Guid PaymentStatusId { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; } = null!;
        public Guid EmployeeAddedId { get; set; }
        public virtual Employee Employee { get; set; } = null!;


        public string? Notes { get; set; }


        public decimal VATPercentage { get; set; }
        public decimal TotalPriceBeforeDiscount { get; set; }
        public decimal NetProfit { get; set; }
        public decimal TotalPriceAfterDiscount { get; set; }
        public decimal DiscountPercentage { get; set; }


        public ICollection<BookingServices> Services { get; set; } = null!;




    }
    public class BookingConfigration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {


        }
    }
}
