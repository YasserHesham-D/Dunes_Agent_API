using Domain.Enums;
using Domain.Models.MTM;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Accounts
{
    public class RoleConstants
    {
        public const string TourAgent = "TourAgent";
        public const string DiscAgent = "DiscAgent";
        public const string OperationManager = "OperationManager";
        public const string Admin = "Admin";

    }

    public class Employee : IdentityUser
    {
        public string FullName { get; set; } = null!;
        
        public DateTime JoinDate { get; set; }
        public bool IsDeleted { get; set; } 

        public SalaryType SalaryType { get; set; }
        public bool IsFromUAE { get; set; }
        public decimal SalaryValue { get; set; } = 0;
       
        public decimal CommissionRate { get; set; } = 0;

        public decimal StaffVisaCount { get; set; } = 0;

        public Guid? HotelId { get; set; }
        
        public virtual Hotel? Hotel { get; set; }
        public string EmployeeAddedId { get; set; } = null!;

        public virtual Employee EmployeeAdd { get; set; } = null!;    
        public Guid? LocationId { get; set; }

        public virtual Location? Location { get; set; }

        public ICollection<Employee>? EmployeesAdded { get; set; }

        public ICollection<Hotel>? HotelsAdded { get; set; }

        public ICollection<Location>? LocationsAdded { get; set; }

        public ICollection<Service>? ServicesAdded { get; set; }

        public ICollection<Currency>? CurrenciesAdded { get; set; }

        public ICollection<CurrencyValues>? CurrenciesValuesAdded { get; set; }

        public ICollection<Driver>? DriversAdded { get; set; }

        public ICollection<PaymentMethod>? PaymentMethodsAdded { get; set; }

        public ICollection<PaymentStatus>? PaymentStatusAdded { get; set; }

        public ICollection<History>? History { get; set; }


        public ICollection<Notification>? Notifications { get; set; }

        public ICollection<Operation>? Operations { get; set; }

        public ICollection<ReciptVoucher>? Vouchers { get; set; }

        public ICollection<Booking>? Bookings { get; set; }




    }
    public class EmployeeConfigration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            // Primary Key
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Email)
                .IsRequired();

            // Properties
            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.JoinDate)
                .IsRequired();

            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(e => e.IsFromUAE)
                .HasDefaultValue(false);

            builder.Property(e => e.SalaryType)
                .HasConversion<int>() // store enum as int
                .IsRequired();

            builder.Property(e => e.SalaryValue)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(e => e.CommissionRate)
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0);

            builder.Property(e => e.StaffVisaCount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            // Relationships --------------------------------

            // Self reference: EmployeeAdded -> EmployeesAdded
            builder.HasOne(e => e.EmployeeAdd)
                .WithMany(e => e.EmployeesAdded)
                .HasForeignKey(e => e.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            // Hotel relationship
            builder.HasOne(e => e.Hotel)
                .WithMany(h => h.EmployeesBelong)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.NoAction);

            // Location relationship
            builder.HasOne(e => e.Location)
                .WithMany(l => l.EmployeesBelong)
                .HasForeignKey(e => e.LocationId)
                .OnDelete(DeleteBehavior.NoAction);

            // Collections
            builder.HasMany(e => e.HotelsAdded)
                .WithOne(h => h.Employee)
                .HasForeignKey(h => h.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.LocationsAdded)
                .WithOne(l => l.Employee)
                .HasForeignKey(l => l.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.ServicesAdded)
                .WithOne(s => s.Employee)
                .HasForeignKey(s => s.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.CurrenciesAdded)
                .WithOne(c => c.Employee)
                .HasForeignKey(c => c.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.CurrenciesValuesAdded)
                .WithOne(cv => cv.Employee)
                .HasForeignKey(cv => cv.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.DriversAdded)
                .WithOne(d => d.Employee)
                .HasForeignKey(d => d.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.PaymentMethodsAdded)
                .WithOne(pm => pm.Employee)
                .HasForeignKey(pm => pm.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.PaymentStatusAdded)
                .WithOne(ps => ps.Employee)
                .HasForeignKey(ps => ps.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.History)
                .WithOne(h => h.Employee)
                .HasForeignKey(h => h.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Notifications)
                .WithOne(n => n.Employee)
                .HasForeignKey(n => n.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Operations)
                .WithOne(o => o.Employee)
                .HasForeignKey(o => o.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Vouchers)
                .WithOne(v => v.Employee)
                .HasForeignKey(v => v.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Bookings)
                .WithOne(b => b.Employee)
                .HasForeignKey(b => b.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        

    
    }
}
