using Application.Dtos.Employee;
using Domain.Interfaces.IModelsRepo;
using Domain.Models.Accounts;
using Infrastructure.DBContext;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<Employee> GetAllEmployeesQuery(string? fullname, string? position, string? phonenumber)
        {
            return _context.Users
                .OfType<Employee>()
                .Include(e => e.Location)
                .Include(e => e.Permissions)
                .Where(e => !e.IsDeleted);
        }
    }
}
