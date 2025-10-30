using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PaymentStatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid EmployeeAddedId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public ICollection<Booking>? Bookings { get; set; }
    }
    public class PaymentStatusConfigration : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {


        }
    }
}
