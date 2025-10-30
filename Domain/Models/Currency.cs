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
        public Guid EmployeeAddedId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public ICollection<CurrencyValues> CurrenciesFrom { get; set; } = null!;

        public ICollection<CurrencyValues> CurrenciesTo { get; set; } = null!;

        public ICollection<Operation> Operations { get; set; } = null!;

        public ICollection<ReciptVoucher>? Vouchers { get; set; }

        public ICollection<Booking>? Bookings { get; set; }


    }
    public class CurrencyConfigration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {


        }
    }
}
