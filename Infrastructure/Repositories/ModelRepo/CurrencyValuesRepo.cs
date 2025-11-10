using Domain.Interfaces.IModelsRepo;
using Domain.Models;
using Domain.Models.MTM;
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
    public class CurrencyValuesRepo : Repository<CurrencyValues>, ICurrencyValuesRepo
    {
        private readonly AppDbContext context;
        private readonly ILogger<CurrencyValues> logger;

        public CurrencyValuesRepo(AppDbContext context, ILogger<CurrencyValues> logger) : base(context, logger)
        {
            this.context = context;
            this.logger = logger;
        }
    }
}
