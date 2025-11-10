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
    public class Booking
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;

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

        public string EmployeeAddedId { get; set; } = null!;
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

            builder.ToTable("Bookings");

            // Primary Key
            builder.HasKey(b => b.Id);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            // Indexes (Optional but Recommended)
            builder.HasIndex(b => b.OrderNumber).IsUnique(false);
            builder.HasIndex(b => b.TicketNumber).IsUnique(false);
            builder.HasIndex(b => b.PhoneNumber);

            // Property Configurations
            builder.Property(b => b.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(b => b.GuestName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Room)
                .HasMaxLength(50);

            builder.Property(b => b.PickUpStatus)
                .HasMaxLength(50);

            builder.Property(b => b.TicketNumber)
                .HasMaxLength(50);

            builder.Property(b => b.OrderNumber)
                .HasMaxLength(50);

            builder.Property(b => b.Notes)
                .HasMaxLength(500);

            // Decimal Precision for Currency Values
            builder.Property(b => b.VATPercentage)
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0);

            builder.Property(b => b.TotalPriceBeforeDiscount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(b => b.NetProfit)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(b => b.TotalPriceAfterDiscount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(b => b.DiscountPercentage)
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0);

            builder.Property(b => b.IsConfirmed)
                .HasDefaultValue(false);

            builder.Property(b => b.BookingDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(b => b.PickUpDate)
                .IsRequired();

            // Relationships ------------------------------------

            // Booking → Driver (many-to-one)
            builder.HasOne(b => b.Driver)
                .WithMany(d => d.Bookings)
                .HasForeignKey(b => b.DriverId)
                .OnDelete(DeleteBehavior.NoAction);

            // Booking → Hotel (many-to-one)
            builder.HasOne(b => b.Hotel)
                .WithMany(h => h.Bookings)
                .HasForeignKey(b => b.HotelId)
                .OnDelete(DeleteBehavior.NoAction);

            // Booking → Currency (many-to-one)
            builder.HasOne(b => b.Currency)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CurrencyId)
                .OnDelete(DeleteBehavior.NoAction);

            // Booking → PaymentMethod (many-to-one)
            builder.HasOne(b => b.PaymentMethod)
                .WithMany(pm => pm.Bookings)
                .HasForeignKey(b => b.PaymentMethodId)
                .OnDelete(DeleteBehavior.NoAction);

            // Booking → PaymentStatus (many-to-one)
            builder.HasOne(b => b.PaymentStatus)
                .WithMany(ps => ps.Bookings)
                .HasForeignKey(b => b.PaymentStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            // Booking → Employee (many-to-one)
            builder.HasOne(b => b.Employee)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            // Booking → BookingServices (many-to-many)
            builder.HasMany(b => b.Services)
                .WithOne(bs => bs.Booking)
                .HasForeignKey(bs => bs.BookingId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
