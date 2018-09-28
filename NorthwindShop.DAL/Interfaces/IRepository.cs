using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NorthwindShop.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        T GetById(int id);
        IEnumerable<T> Get();
        IEnumerable<T> Get(Func<T, bool> predicate);
        IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Update(T entity);
        void Remove(T entity);
    }
}
