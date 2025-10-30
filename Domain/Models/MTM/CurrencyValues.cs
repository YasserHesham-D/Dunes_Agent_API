using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Guid EmployeeAddedId { get; set; }
        public virtual Employee Employee { get; set; } = null!;
    }

    public class CurrencyValuesConfigration : IEntityTypeConfiguration<CurrencyValues>
    {
        public void Configure(EntityTypeBuilder<CurrencyValues> builder)
        {


        }
    }
}
