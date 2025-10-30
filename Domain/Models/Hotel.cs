using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Hotel
    {
        public Guid Id { get; set; } =Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Place { get; set; } = null!;
        public DateTime EntryDate { get; set; } = DateTime.UtcNow;
        public Guid EmployeeAddedId {  get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = null!;

        public ICollection<Employee>? EmployeesBelong {  get; set; } 

    }
    public class HotelConfigration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {

            builder.ToTable("Hotels");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name).HasColumnType("NVARCHAR(100)").HasMaxLength(100);

            builder.Property(e => e.Place).HasColumnType("NVARCHAR(300)").HasMaxLength(300);

            builder.HasOne(e => e.Employee)
              .WithMany(e => e.HotelsAdded)
              .HasForeignKey(e => e.EmployeeAddedId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Bookings)
                .WithOne(v => v.Hotel)
                .HasForeignKey(v => v.HotelId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.EmployeesBelong)
                .WithOne(b => b.Hotel)
                .HasForeignKey(b => b.HotelId)
                .OnDelete(DeleteBehavior.NoAction);

            

        }
    }
}
