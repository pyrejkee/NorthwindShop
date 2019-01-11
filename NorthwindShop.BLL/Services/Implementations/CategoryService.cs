using AutoMapper;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.DAL.Entities;
using NorthwindShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NorthwindShop.BLL.Services.Implementations
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repository,
                              IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> Get()
        {
            var categoriesFromRepo = await _repository.Get();
            var categories = _mapper.Map<List<CategoryDTO>>(categoriesFromRepo);

            return categories;
        }

        public async Task<List<CategoryDTO>> Get(Expression<Func<Category, bool>> predicate)
        {
            var categoriesFromRepo = await _repository.Get(predicate);
            var categories = _mapper.Map<List<CategoryDTO>>(categoriesFromRepo);

            return categories;
        }

        public async Task<CategoryDTO> Add(CategoryDTO category)
        {
            var categoryToRepossitory = _mapper.Map<Category>(category);
            var addedCategory = await _repository.Add(categoryToRepossitory);
            var categoryDto = _mapper.Map<CategoryDTO>(addedCategory);

            return categoryDto;
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var categoriesFromRepo = await _repository.GetById(id);
            var category = _mapper.Map<CategoryDTO>(categoriesFromRepo);

            return category;
        }

        public async Task<List<CategoryDTO>> GetWithInclude(params Expression<Func<Category, object>>[] includeProperties)
        {
            var categoriesFromRepo = await _repository.GetWithInclude(includeProperties);
            var categories = _mapper.Map<List<CategoryDTO>>(categoriesFromRepo);

            return categories;
        }

        public async Task<List<CategoryDTO>> GetWithInclude(Expression<Func<Category, bool>> predicate, params Expression<Func<Category, object>>[] includeProperties)
        {
            var categoriesFromRepo = await _repository.GetWithInclude(predicate, includeProperties);
            var categories = _mapper.Map<List<CategoryDTO>>(categoriesFromRepo);

            return categories;
        }

        public async Task<CategoryDTO> Update(CategoryDTO category)
        {
            var categoryToRepo = _mapper.Map<Category>(category);
            var updatedCategory = await _repository.Update(categoryToRepo);
            var updatedCategoryDto = _mapper.Map<CategoryDTO>(updatedCategory);

            return updatedCategoryDto;
        }

        public async Task Remove(int id)
        {
            await _repository.Remove(id);
        }
    }
}
