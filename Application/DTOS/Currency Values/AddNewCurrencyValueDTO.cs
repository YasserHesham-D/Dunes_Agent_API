using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Currency_Values
{
    public class AddNewCurrencyValueDTO
    {
        public Guid CurrencyFrom { get; set; }

        public decimal Price { get; set; }

        public Guid CurrencyTo { get; set; }


    }
}
