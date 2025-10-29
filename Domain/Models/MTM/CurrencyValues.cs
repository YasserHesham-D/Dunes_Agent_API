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
        public decimal Price { get; set; }
        public Guid CurrencyToId { get; set; }
        public Guid EmployeeAddedId { get; set; }
    }
}
