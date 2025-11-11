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
    public class DriverRepo : Repository<Driver>, IDriverRepo
    {
        private readonly AppDbContext context;
        private readonly ILogger<Driver> logger;

        public DriverRepo(AppDbContext context, ILogger<Driver> logger) : base(context, logger)
        {
            this.context = context;
            this.logger = logger;
        }
    }

}
