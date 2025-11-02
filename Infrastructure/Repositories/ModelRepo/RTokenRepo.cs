using Domain.Interfaces.IModelsRepo;
using Domain.Models.Accounts;
using Infrastructure.DBContext;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ModelRepo
{
    public class RTokenRepo : Repository<RefreshToken>, IRefreshToken
    {
        public RTokenRepo(AppDbContext context) : base(context)
        {
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
