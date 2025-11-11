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
    public class Driver
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;

        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string CarNumber { get; set; } = null!;
        public string PlaceOfWork { get; set; } = null!;
        public DateTime DateOfAdd { get; set; }= DateTime.UtcNow;
        public string? EmployeeAddedId { get; set; } = null!;

        public virtual Employee? Employee { get; set; } = null!;

        public ICollection<Booking>? Bookings { get; set; }

    }
    public class DriverConfigration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Driver");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.Property(e => e.Name).HasColumnType("NVARCHAR(100)").HasMaxLength(100);

            builder.Property(e => e.PhoneNumber).HasColumnType("NVARCHAR(20)").HasMaxLength(20);

            builder.Property(e => e.CarNumber).HasColumnType("NVARCHAR(20)").HasMaxLength(20);

            builder.Property(e => e.PlaceOfWork).HasColumnType("NVARCHAR(50)").HasMaxLength(50);

            builder.HasOne(e => e.Employee)
             .WithMany(e => e.DriversAdded)
             .HasForeignKey(e => e.EmployeeAddedId)
             .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
