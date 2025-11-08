using Application.Dtos.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CurrencyService
{
    public interface ICurrencyService
    {
         Task<bool> AddNewCurrency(string Id, AddNewCurrencyRequest request);

    }
}
