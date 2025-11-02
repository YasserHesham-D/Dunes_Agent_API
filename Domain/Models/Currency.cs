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
    public class Currency
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string EmployeeAddedId { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;

        public ICollection<CurrencyValues> CurrenciesFrom { get; set; } = new List<CurrencyValues>();
        public ICollection<CurrencyValues> CurrenciesTo { get; set; } = new List<CurrencyValues>();

        public ICollection<Operation> Operations { get; set; } =  new List<Operation>();

        public ICollection<ReciptVoucher>? Vouchers { get; set; }

        public ICollection<Booking>? Bookings { get; set; }


    }
    public class CurrencyConfigration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name).HasColumnType("NVARCHAR(50)").HasMaxLength(50);

            builder.HasOne(e => e.Employee)
            .WithMany(e => e.CurrenciesAdded)
            .HasForeignKey(e => e.EmployeeAddedId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.CurrenciesFrom)
                .WithOne(c => c.CurrencyFrom)
                .HasForeignKey(c => c.CurrencyFromId)
                .OnDelete(DeleteBehavior.NoAction)
             .IsRequired();

            builder.HasMany(e => e.CurrenciesTo)
                .WithOne(c => c.CurrencyTo)
                .HasForeignKey(c => c.CurrencyToId)
                .OnDelete(DeleteBehavior.NoAction).IsRequired();

            builder.HasMany(e => e.Operations)
               .WithOne(o => o.Currency)
               .HasForeignKey(o => o.CurrencyId)
               .OnDelete(DeleteBehavior.NoAction).IsRequired();

            builder.HasMany(e => e.Vouchers)
                .WithOne(v => v.Currency)
                .HasForeignKey(v => v.CurrencyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Bookings)
                .WithOne(b => b.Currency)
                .HasForeignKey(b => b.CurrencyId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
