using Domain.Interfaces.IModelsRepo;
using Domain.Models.Accounts;
using Infrastructure.DBContext;
using Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ModelRepo
{
    public class AccountsRepo : Repository<Employee>, IAccountsRepo
    {
        private readonly AppDbContext _context;
        private readonly ILogger<Employee> _logger;
        public AccountsRepo(AppDbContext context, ILogger<Employee> logger) : base(context,logger)
        {
            _context = context;
            _logger=logger;
        }

        public virtual async Task<Employee> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }
        

    }
}
