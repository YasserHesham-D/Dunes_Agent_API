using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Accounts;

namespace Domain.Models.MTM
{
    public class CurrencyValues
    {
        public Guid Id { get; set; }= Guid.NewGuid();

        public Guid CurrencyFromId { get; set; }
        public virtual Currency CurrencyFrom { get; set; } = null!;

        public decimal Price { get; set; }

        public Guid CurrencyToId { get; set; }
        public virtual Currency CurrencyTo { get; set; } = null!;

        public string EmployeeAddedId { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
    }

    public class CurrencyValuesConfigration : IEntityTypeConfiguration<CurrencyValues>
    {
        public void Configure(EntityTypeBuilder<CurrencyValues> builder)
        {

            builder.ToTable("CurrencyValues");

            // Primary Key
            builder.HasKey(bs => bs.Id);

            // Indexes (optional for performance)
            builder.HasIndex(bs => new { bs.CurrencyFromId, bs.CurrencyToId, bs.EmployeeAddedId });

            // Relationships ------------------------------


            builder.HasOne(bs => bs.CurrencyFrom)
                .WithMany(b => b.CurrenciesFrom)
                .HasForeignKey(bs => bs.CurrencyFromId)
                .OnDelete(DeleteBehavior.NoAction)
             .IsRequired();


            builder.HasOne(bs => bs.CurrencyTo)
                .WithMany(s => s.CurrenciesTo)
                .HasForeignKey(bs => bs.CurrencyToId)
                .OnDelete(DeleteBehavior.NoAction).IsRequired();

            // Location (Many-to-One)
            builder.HasOne(bs => bs.Employee)
                .WithMany(l => l.CurrenciesValuesAdded)
                .HasForeignKey(bs => bs.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(bs => bs.Price)
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0);

        }
    }
}
