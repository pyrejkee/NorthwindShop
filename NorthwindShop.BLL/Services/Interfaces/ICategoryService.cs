using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NorthwindShop.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        void Add(CategoryDTO category);
        CategoryDTO GetById(int id);
        List<CategoryDTO> Get();
        List<CategoryDTO> Get(Func<Category, bool> predicate);
        List<CategoryDTO> GetWithInclude(params Expression<Func<Category, object>>[] includeProperties);
        List<CategoryDTO> GetWithInclude(Func<Category, bool> predicate, params Expression<Func<Category, object>>[] includeProperties);
        CategoryDTO Update(CategoryDTO category);
        void Remove(CategoryDTO category);
    }
}
