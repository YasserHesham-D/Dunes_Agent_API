using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Currency_Values
{
    public class GetAllCurrencyValuesDTO
    {
        public Guid Id { get; set; }

        public string CurrencyFrom { get; set; } = null!;

        public decimal Price { get; set; }

        public string CurrencyTo { get; set; } = null!;

        public string? EmployeeAdd { get; set; } 


    }
}
