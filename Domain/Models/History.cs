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
    public  class History
    {
        public Guid Id { get; set; }=Guid.NewGuid();
        public string OperationName { get; set; } = null!;
        public DateTime Date {  get; set; } = DateTime.UtcNow;
        public string EmployeeId { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;

    }
    public class HistoryConfigration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.ToTable("History");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.OperationName).HasColumnType("NVARCHAR(100)").HasMaxLength(100);

            builder.HasOne(e => e.Employee)
             .WithMany(e => e.History)
             .HasForeignKey(e => e.EmployeeId)
             .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
