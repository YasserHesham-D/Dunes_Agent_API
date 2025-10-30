using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PaymentMethod
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public Guid EmployeeAddedId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public ICollection<Operation>? Operations { get; set; }

        public ICollection<ReciptVoucher>? Vouchers { get; set; }

        public ICollection<Booking>? Bookings { get; set; }


    }
    public class PaymentMethodConfigration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {

            builder.ToTable("PaymentStatuses");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasOne(e => e.Employee)
                .WithMany(e => e.PaymentMethodsAdded)
                .HasForeignKey(e => e.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Bookings)
              .WithOne(b => b.PaymentMethod)
              .HasForeignKey(b => b.PaymentMethodId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Operations)
             .WithOne(b => b.PaymentMethod)
             .HasForeignKey(b => b.PaymentMethodId)
             .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Vouchers)
            .WithOne(b => b.PaymentMethod)
            .HasForeignKey(b => b.PaymentMethodId)
            .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
