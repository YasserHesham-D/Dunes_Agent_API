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
        public string? DriverName { get; set; }
        public string? Notes { get; set; }


        public decimal VATPercentage { get; set; }
        public decimal TotalPriceBeforeDiscount { get; set; }
        public decimal NetProfit { get; set; }
        public decimal TotalPriceAfterDiscount { get; set; }
        public decimal DiscountPercentage { get; set; }




    }
}
