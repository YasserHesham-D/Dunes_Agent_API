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
    public class Location
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Place {  get; set; } = null!;

        public DateTime EntryDate { get; set; } = DateTime.UtcNow;
        public string? EmployeeAddedId { get; set; } = null!;

        public virtual Employee? Employee { get; set; } = null!;

        public ICollection<LocationServices> Services { get; set; } = null!;

        public ICollection<BookingServices> BookingServices { get; set; } = null!;

        public ICollection<ReciptVoucherServices> VoucherServices { get; set; } = null!;

        public ICollection<Employee>? EmployeesBelong { get; set; }


    }
    public class LocationConfigration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {

            builder.ToTable("Locations");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name).HasColumnType("NVARCHAR(100)").HasMaxLength(100);

            builder.Property(e => e.Place).HasColumnType("NVARCHAR(300)").HasMaxLength(300);

            builder.HasOne(e => e.Employee)
              .WithMany(e => e.LocationsAdded)
              .HasForeignKey(e => e.EmployeeAddedId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Services)
                .WithOne(v => v.Location)
                .HasForeignKey(v => v.LocationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.BookingServices)
                .WithOne(b => b.Location)
                .HasForeignKey(b => b.LocationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.VoucherServices)
               .WithOne(b => b.Location)
               .HasForeignKey(b => b.LocationId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.EmployeesBelong)
               .WithOne(b => b.Location)
               .HasForeignKey(b => b.LocationId)
               .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
