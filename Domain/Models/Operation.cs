using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Operation
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string OperationName { get; set; } = null!;
        public OpreationType Type { get; set; } 
        public decimal Value { get; set; }

        public Guid CurrencyId { get; set; }

        public virtual Currency Currency { get; set; } = null!;

        public Guid PaymentMethodId { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; } = null!;

        public string EmployeeAddedId { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;
    }
    public class OperationConfigration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {

            builder.ToTable("Opreations");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.OperationName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Value)
                .IsRequired();

            builder.Property(e => e.Type)
               .IsRequired()
               .HasMaxLength(100);

           

            builder.Property(e => e.Date)
                .IsRequired();


            builder.Property(e => e.Type)
                .HasConversion<int>() // store enum as int
                .IsRequired();



            // Relationships --------------------------------

            // Self reference: EmployeeAdded -> EmployeesAdded
            builder.HasOne(e => e.Employee)
                .WithMany(e => e.Operations)
                .HasForeignKey(e => e.EmployeeAddedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Currency)
                .WithMany(e => e.Operations)
                .HasForeignKey(e => e.CurrencyId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(e => e.PaymentMethod)
                .WithMany(e => e.Operations)
                .HasForeignKey(e => e.PaymentMethodId)
                .OnDelete(DeleteBehavior.NoAction);





            // Collections


        }
    }
}
