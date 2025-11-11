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
    public class PermissionRepo : Repository<Permission>, IPermissionRepo
    {
        private readonly AppDbContext context;
        private readonly ILogger logger;
        public PermissionRepo(AppDbContext context, ILogger<Permission> logger) : base(context, logger)
        {
            this.context = context;
            this.logger = logger;
        }
    }
}
