using Domain.Interfaces.IRepository;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepo<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public AppDbContext GetContext()
        {
            return _context;
        }
        
        // GET BY ID
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        // GET ALL
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // GET WITH PREDICATE
        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        // GET FIRST OR DEFAULT
        public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        // GET PAGED
        public virtual async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true)
        {
            var query = _dbSet.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var totalCount = await query.CountAsync();

            if (orderBy != null)
            {
                query = ascending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        // ADD
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        // ADD RANGE
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        // UPDATE
        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        // PATCH 
        public virtual async Task<T> PatchAsync(Guid id, Action<T> patchAction)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found");

            // Apply the patch action to modify the entity
            patchAction(entity);

            // Mark as modified
            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        // DELETE BY ID
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        // DELETE ENTITY
        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }


        // EXISTS
        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null;
        }

        // ANY
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        // COUNT
        public virtual async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        // COUNT WITH PREDICATE
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
