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
    public class ReciptVoucher
    {
        public Guid Id { get; set; } = Guid.NewGuid();


        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }

        public string? Room { get; set; }

        public bool IsConfirmed { get; set; }
      
        public string GuestName { get; set; } = null!;
        public decimal TotalPrice { get; set; }

        public Guid CurrencyId { get; set; }

        public virtual Currency Currency { get; set; } = null!;

        public Guid PaymentMethodId { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; } = null!;

        public Guid EmployeeAddedId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public ICollection<ReciptVoucherServices> Services { get; set; } = null!;
    }
    public class ReciptVoucherConfigration : IEntityTypeConfiguration<ReciptVoucher>
    {
        public void Configure(EntityTypeBuilder<ReciptVoucher> builder)
        {


        }
    }
}
