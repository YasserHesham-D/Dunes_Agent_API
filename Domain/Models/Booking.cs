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
        public DateTime BookingDate { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string GuestName { get; set; } = null!;
        public string CarNumber { get; set; } = null!;

        // public ???? Room { get; set; }
        // public ???? PickUpStatus { get; set; }

        public DateTime PickUpDate { get; set; }
        public bool IsConfirmed { get; set; }

        public Guid DriverId { get; set; }
        public string? DriverName { get; set; }

        public Guid HotelId { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public Guid PaymentStatusId { get; set; }
        public Guid EmployeeAddedId { get; set; }
        public int TicketNumber { get; set; }


        public string? Notes { get; set; }


        public decimal VATPercentage { get; set; }
        public decimal TotalPriceBeforeDiscount { get; set; }
        public decimal NetProfit { get; set; }
        public decimal TotalPriceAfterDiscount { get; set; }
        public decimal DiscountPercentage { get; set; }




    }
    public class BookingConfigration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {


        }
    }
}
