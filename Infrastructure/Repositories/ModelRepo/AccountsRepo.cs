using Domain.Interfaces.IModelsRepo;
using Domain.Models;
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
        private readonly ILogger<AccountsRepo> _logger;
        private readonly AppDbContext _context;
        public AccountsRepo(AppDbContext context, ILogger<AccountsRepo> logger) : base(context,logger)
        {
            _context = context;
            _logger = logger;
        }
        
    }
}
