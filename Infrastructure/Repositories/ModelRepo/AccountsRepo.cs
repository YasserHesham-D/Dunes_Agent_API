using Domain.Interfaces.IModelsRepo;
using Domain.Models;
using Infrastructure.DBContext;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ModelRepo
{
    public class AccountsRepo : Repository<Employee>, IAccountsRepo
    {
        public AccountsRepo(AppDbContext context) : base(context)
        {
        }
    }
}
