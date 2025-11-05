using Domain.Enums;
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
    public class Service
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Duration { get; set; } 

        public TimeDuration TimeDuration { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.UtcNow;
        
        public string ServiceName { get; set; } = null!;
        public string? Description { get; set; } = null;
        public string? Type { get; set; } = null;

        public string EmployeeAddedId { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;

        public ICollection<LocationServices>? LocationServices { get; set; } = new List<LocationServices>();

        public ICollection<BookingServices>? BookingServices { get; set; } = new List<BookingServices>();

        public ICollection<ReciptVoucherServices>? VoucherServices { get; set; } = new List<ReciptVoucherServices>();



    }
    public class ServiceConfigration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.ServiceName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Duration)
                .IsRequired();

            builder.Property(e => e.Type)
               //.IsRequired()
               .HasMaxLength(100);

            builder.Property(e => e.Description)
               .IsRequired()
               .HasMaxLength(300);

            builder.Property(e => e.EntryDate)
                .IsRequired();

          
            builder.Property(e => e.TimeDuration)
                .HasConversion<int>() // store enum as int
                .IsRequired();



            // Relationships --------------------------------

            // Self reference: EmployeeAdded -> EmployeesAdded
            builder.HasOne(e => e.Employee)
                .WithMany(e => e.ServicesAdded)
                .HasForeignKey(e => e.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            

           

            // Collections
            builder.HasMany(e => e.LocationServices)
                .WithOne(h => h.Service)
                .HasForeignKey(h => h.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.BookingServices)
                .WithOne(l => l.Service)
                .HasForeignKey(l => l.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.VoucherServices)
                .WithOne(s => s.Service)
                .HasForeignKey(s => s.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            
               
        }
    }

}
