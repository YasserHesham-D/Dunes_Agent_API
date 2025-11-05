using Domain.Models;
using Domain.Models.Accounts;
using Domain.Models.MTM;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DBContext
{
    public class AppDbContext : IdentityDbContext<Employee>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Analysis> Analyses { get; set; }
        public DbSet<Booking>Bookings { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentStatus> PaymentsStatus { get; set; }
        public DbSet<ReciptVoucher> ReciptVouchers { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<BookingServices> BookingsServices { get; set; }
        public DbSet<CurrencyValues> CurrencyValues { get; set; }
        public DbSet<LocationServices> LocationServices { get; set; }
        public DbSet<ReciptVoucherServices> ReciptVoucherServices { get; set; }
        public DbSet<EmployeePermission> EmployeePermissions { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Currency).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CurrencyValues).Assembly);

            base.OnModelCreating(modelBuilder);

        }
    }
}
