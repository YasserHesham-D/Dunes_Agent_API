using Domain.Models.Accounts;
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
    public class ReciptVoucher
    {
        public Guid Id { get; set; } = Guid.NewGuid();


        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }

        public string? Room { get; set; }

        public bool IsConfirmed { get; set; }
      
        public string GuestName { get; set; } = null!;
        public decimal TotalPrice { get; set; } = 0;

        public Guid CurrencyId { get; set; }

        public virtual Currency Currency { get; set; } = null!;

        public Guid PaymentMethodId { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; } = null!;

        public string EmployeeAddedId { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;

        public ICollection<ReciptVoucherServices> Services { get; set; } = null!;
    }
    public class ReciptVoucherConfigration : IEntityTypeConfiguration<ReciptVoucher>
    {
        public void Configure(EntityTypeBuilder<ReciptVoucher> builder)
        {

            builder.ToTable("ReciptVouchers");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.GuestName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.IsConfirmed)
               .HasDefaultValue(false);


            builder.Property(e => e.Room)
                .HasMaxLength(20);

            builder.Property(e => e.TotalPrice)
               .IsRequired()
               .HasDefaultValue(0);

            builder.Property(e => e.Notes)
               .IsRequired()
               .HasMaxLength(500);

            builder.Property(e => e.Date)
                .IsRequired();


          



            // Relationships --------------------------------

            // Self reference: EmployeeAdded -> EmployeesAdded
            builder.HasOne(e => e.Employee)
                .WithMany(e => e.Vouchers)
                .HasForeignKey(e => e.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Currency)
               .WithMany(e => e.Vouchers)
               .HasForeignKey(e => e.CurrencyId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.PaymentMethod)
              .WithMany(e => e.Vouchers)
              .HasForeignKey(e => e.PaymentMethodId)
              .OnDelete(DeleteBehavior.NoAction);





            // Collections
            builder.HasMany(e => e.Services)
                .WithOne(h => h.Voucher)
                .HasForeignKey(h => h.ReciptVoucherId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
