using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NorthwindShop.DAL.Entities;

namespace NorthwindShop.BLL.Services.Interfaces
{
    public interface IProductService
    {
        void Add(Product product);
        Product GetById(int id);
        List<Product> Get();
        List<Product> Get(Func<Product, bool> predicate);
        List<Product> GetWithInclude(params Expression<Func<Product, object>>[] includeProperties);
        List<Product> GetWithInclude(Func<Product, bool> predicate, params Expression<Func<Product, object>>[] includeProperties);
        void Update(Product product);
        void Remove(Product product);
    }
}
