using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.DAL.Entities;

namespace NorthwindShop.BLL.Services.Interfaces
{
    public interface IProductService
    {
        void Add(ProductDTO product);
        ProductDTO GetById(int id);
        List<ProductDTO> Get();
        List<ProductDTO> Get(Func<Product, bool> predicate);
        List<ProductDTO> GetWithInclude(params Expression<Func<Product, object>>[] includeProperties);
        List<ProductDTO> GetWithInclude(Func<Product, bool> predicate, params Expression<Func<Product, object>>[] includeProperties);
        void Update(ProductDTO product);
        void Remove(ProductDTO product);
    }
}
