using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Currency
{
    public class GetCurrencyBookingsCountDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int? BookingsCount { get; set; }

        public decimal? TotalMoney { get; set; }
    }
}
