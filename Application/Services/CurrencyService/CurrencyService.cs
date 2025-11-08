using Application.Dtos.Currency;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IUnitOfWork;
using Domain.Models;
using Domain.Models.Accounts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CurrencyService
{
    public class CurrencyService(UserManager<Employee> userManager, ICurrencyRepo currencyRepo,IUnitOfWork unitOfWork) : ICurrencyService
    {
        public async Task<bool> AddNewCurrency(string Id ,AddNewCurrencyRequest request)
        {
            if (request == null) throw new Exception("Invalid Service");

            var user = await userManager.FindByIdAsync(Id);
            if (user == null) throw new Exception("Invalid Service");

            if(await currencyRepo.AnyAsync(x => x.Name == request.Name))
                return false;


            var currency = new Currency
            {
                EmployeeAddedId = Id,
                Name = request.Name,
            };

            await currencyRepo.AddAsync(currency);
            await unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
