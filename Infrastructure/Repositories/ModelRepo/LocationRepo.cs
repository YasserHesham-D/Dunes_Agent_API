

using Domain.Interfaces.IModelsRepo;
using Domain.Models;
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
    public class LocationRepo : Repository<Location>, ILocationRepo
    {
        private readonly ILogger<Location> _logger;
        private readonly AppDbContext _context;
        public LocationRepo(AppDbContext context, ILogger<Location> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
    
}
