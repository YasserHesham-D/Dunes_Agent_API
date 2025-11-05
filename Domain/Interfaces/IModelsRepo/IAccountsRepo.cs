using Domain.Interfaces.IRepository;
using Domain.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Interfaces.IModelsRepo
{
    public interface IAccountsRepo : IRepo<Employee>
    {
        
        Task<Employee> GetByIdAsync(string id);

            IQueryable<Employee> GetAllEmployeesQuery(
                string? fullname,
                string? position,
                string? phonenumber
                );
        
    }

}
