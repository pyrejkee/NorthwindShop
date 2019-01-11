using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NorthwindShop.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDTO> Add(CategoryDTO category);

        Task<CategoryDTO> GetById(int id);

        Task<List<CategoryDTO>> Get();

        Task<List<CategoryDTO>> Get(Expression<Func<Category, bool>> predicate);

        Task<List<CategoryDTO>> GetWithInclude(params Expression<Func<Category, object>>[] includeProperties);

        Task<List<CategoryDTO>> GetWithInclude(Expression<Func<Category, bool>> predicate, params Expression<Func<Category, object>>[] includeProperties);

        Task<CategoryDTO> Update(CategoryDTO category);

        Task Remove(int id);
    }
}
