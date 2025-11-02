using Domain.Interfaces.IRepository;
using Domain.Models;
using Infrastructure.DBContext;
using Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class HotelRepo : Repository<Hotel>, IHotelRepo
    {
        private readonly AppDbContext _context;
        public HotelRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
