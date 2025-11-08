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
    public class CurrencyRepo : Repository<Currency>, ICurrencyRepo
    {
        private readonly AppDbContext context;
        private readonly ILogger<Currency> logger;

        public CurrencyRepo(AppDbContext context, ILogger<Currency> logger) : base(context, logger)
        {
            this.context = context;
            this.logger = logger;
        }
    }
}
