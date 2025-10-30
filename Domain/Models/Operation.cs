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

        public DateTime Date { get; set; }

        public string OperationName { get; set; } = null!;
        public OpreationType Type { get; set; } 
        public decimal Value { get; set; }

        public Guid CurrencyId { get; set; }

        public virtual Currency Currency { get; set; } = null!;

        public Guid PaymentMethodId { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; } = null!;

        public Guid EmployeeAddedId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
    public class OperationConfigration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {


        }
    }
}
