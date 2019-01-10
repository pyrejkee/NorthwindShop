using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NorthwindShop.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Add(T entity);

        Task<T> GetById(int id);

        Task<IEnumerable<T>> Get();

        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<T> Update(T entity);

        Task Remove(T entity);
    }
}
