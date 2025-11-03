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
    public class HotelRepo : Repository<Hotel>, IHotelRepo
    {
        private readonly AppDbContext _context;
        private readonly ILogger<Hotel> _logger;
        public HotelRepo(AppDbContext context , ILogger<Hotel> logger ) : base(context,logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
