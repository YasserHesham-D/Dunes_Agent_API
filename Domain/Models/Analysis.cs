using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Analysis
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal NetProfit { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalOutcome { get; set; }
        public DateTime LastUpdateDate { get; set; } = DateTime.UtcNow;
    }
    public class AnalysisConfigration : IEntityTypeConfiguration<Analysis>
    {
        public void Configure(EntityTypeBuilder<Analysis> builder)
        {
            builder.ToTable("Analysis");

            // Primary Key
            builder.HasKey(b => b.Id);

            builder.Property(b => b.NetProfit)
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0);

            builder.Property(b => b.TotalIncome)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(b => b.TotalOutcome)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(b => b.LastUpdateDate)
                .HasDefaultValueSql("GETUTCDATE()");

        }
    }
}
