using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.DAL.Entities;

namespace NorthwindShop.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> Add(ProductDTO product);

        Task<ProductDTO> GetById(int id);

        Task<List<ProductDTO>> Get();

        Task<List<ProductDTO>> Get(Expression<Func<Product, bool>> predicate);

        Task<List<ProductDTO>> GetWithInclude(params Expression<Func<Product, object>>[] includeProperties);

        Task<List<ProductDTO>> GetWithInclude(Expression<Func<Product, bool>> predicate, params Expression<Func<Product, object>>[] includeProperties);

        Task<ProductDTO> Update(ProductDTO product);

        Task Remove(int id);
    }
}
