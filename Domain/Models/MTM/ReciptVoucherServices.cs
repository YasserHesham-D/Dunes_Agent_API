using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.MTM
{
    public class ReciptVoucherServices
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;

        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; } = null!;

        public Guid LocationId { get; set; }
        public virtual Location Location { get; set; } = null!;

        public int ReciptVoucherId { get; set; }
        public virtual ReciptVoucher Voucher { get; set; } = null!;

        public int KidsCount { get; set; }
        public decimal KidsTotalPrice { get; set; }
        public int ChildsCount { get; set; }
        public decimal ChildsTotalPrice { get; set; }
        public int AdultsCount { get; set; }
        public decimal AdultsTotalPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
    public class ReciptVoucherServicesConfigration : IEntityTypeConfiguration<ReciptVoucherServices>
    {
        public void Configure(EntityTypeBuilder<ReciptVoucherServices> builder)
        {
            builder.ToTable("ReciptVoucherServices");

            // Primary Key
            builder.HasKey(bs => bs.Id);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            // Indexes (optional for performance)
            builder.HasIndex(bs => new { bs.ReciptVoucherId, bs.ServiceId, bs.LocationId });

            // Relationships ------------------------------

            // Booking (Many-to-One)
            builder.HasOne(bs => bs.Voucher)
                .WithMany(b => b.Services)
                .HasForeignKey(bs => bs.ReciptVoucherId)
                .OnDelete(DeleteBehavior.NoAction);

            // Service (Many-to-One)
            builder.HasOne(bs => bs.Service)
                .WithMany(s => s.VoucherServices)
                .HasForeignKey(bs => bs.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            // Location (Many-to-One)
            builder.HasOne(bs => bs.Location)
                .WithMany(l => l.VoucherServices)
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
