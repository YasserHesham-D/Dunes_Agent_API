using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
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


    public class BookingServicesConfigration : IEntityTypeConfiguration<BookingServices>
    {
        public void Configure(EntityTypeBuilder<BookingServices> builder)
        {
            builder.ToTable("BookingServices");

            // Primary Key
            builder.HasKey(bs => bs.Id);

            // Indexes (optional for performance)
            builder.HasIndex(bs => new { bs.BookingId, bs.ServiceId, bs.LocationId });

            // Relationships ------------------------------

            // Booking (Many-to-One)
            builder.HasOne(bs => bs.Booking)
                .WithMany(b => b.Services)
                .HasForeignKey(bs => bs.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            // Service (Many-to-One)
            builder.HasOne(bs => bs.Service)
                .WithMany(s => s.BookingServices)
                .HasForeignKey(bs => bs.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            // Location (Many-to-One)
            builder.HasOne(bs => bs.Location)
                .WithMany(l => l.BookingServices)
                .HasForeignKey(bs => bs.LocationId)
                .OnDelete(DeleteBehavior.NoAction);

            // Property Configurations ---------------------

            builder.Property(bs => bs.KidsCount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(bs => bs.ChildsCount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(bs => bs.AdultsCount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(bs => bs.KidsTotalPrice)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(bs => bs.ChildsTotalPrice)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(bs => bs.AdultsTotalPrice)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(bs => bs.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);
        }
    }
}
