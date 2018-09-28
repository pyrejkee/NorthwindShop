using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _northwindDbContext.SaveChanges();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> Get()
        {
            return _dbSet.AsNoTracking().AsEnumerable();
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties).AsEnumerable();
        }

        public IEnumerable<T> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate);
        }

        public void Update(T entity)
        {
            _northwindDbContext.Entry(entity).State = EntityState.Modified;
            _northwindDbContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
            _northwindDbContext.SaveChanges();
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
