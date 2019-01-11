using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthwindShop.DAL.Interfaces;

namespace NorthwindShop.DAL.Repositories
{
    internal class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly NorthwindContext _northwindDbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(NorthwindContext northwindDbContext)
        {
            _northwindDbContext = northwindDbContext;
            _dbSet = _northwindDbContext.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            _dbSet.Add(entity);
            await _northwindDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return await Include(includeProperties).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T> Update(T entity)
        {
            _northwindDbContext.Entry(entity).State = EntityState.Modified;
            await _northwindDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Remove(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                throw new InvalidOperationException($"Entity with id {id} was not found.");
            }

            _dbSet.Remove(entity);
            await _northwindDbContext.SaveChangesAsync();
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
