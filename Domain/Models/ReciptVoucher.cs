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


        public DateTime Date { get; set; }

        public string? Notes { get; set; }
        public bool IsConfirmed { get; set; }
        // public ???? Room { get; set; }
        public string GuestName { get; set; } = null!;
        public decimal TotalPrice { get; set; }
       
        public Guid CurrencyId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public Guid EmployeeAddedId {  get; set; }
    }
    public class ReciptVoucherConfigration : IEntityTypeConfiguration<ReciptVoucher>
    {
        public void Configure(EntityTypeBuilder<ReciptVoucher> builder)
        {


        }
    }
}
