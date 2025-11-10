using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Currency
{
    public class GetCurrencyVouchersCountDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int? VouchersCount { get; set; }

        public decimal? TotalMoney { get; set; }
    }
}
