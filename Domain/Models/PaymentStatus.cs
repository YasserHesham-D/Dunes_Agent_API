using Domain.Models.Accounts;
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
        public string? EmployeeAddedId { get; set; }

        public virtual Employee? Employee { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
    public class PaymentStatusConfigration : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {

            builder.ToTable("PaymentStatuses");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasOne(e => e.Employee)
                .WithMany(e => e.PaymentStatusAdded)
                .HasForeignKey(e => e.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Bookings)
              .WithOne(b => b.PaymentStatus)
              .HasForeignKey(b => b.PaymentStatusId)
              .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
