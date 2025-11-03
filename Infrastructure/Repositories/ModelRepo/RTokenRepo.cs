using Domain.Interfaces.IModelsRepo;
using Domain.Models;
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
    public class RTokenRepo : Repository<RefreshToken>, IRefreshToken
    {

        private readonly AppDbContext _context;
        private readonly ILogger<RefreshToken> _logger;
        public RTokenRepo(AppDbContext context , ILogger<RefreshToken> logger) : base(context,logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _context.RefreshTokens.FirstOrDefaultAsync(x =>x.Id == id) ;
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
