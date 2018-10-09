using AutoMapper;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.DAL.Entities;
using NorthwindShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NorthwindShop.BLL.Services.Implementations
{
    internal sealed class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repository,
                              IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<CategoryDTO> Get()
        {
            var categoriesFromRepo = _repository.Get().ToList();
            var categories = _mapper.Map<List<CategoryDTO>>(categoriesFromRepo);

            return categories;
        }

        public List<CategoryDTO> Get(Func<Category, bool> predicate)
        {
            var categoriesFromRepo = _repository.Get(predicate);
            var categories = _mapper.Map<List<CategoryDTO>>(categoriesFromRepo);
            return categories;
        }

        public void Add(CategoryDTO category)
        {
            var categoryToRepossitory = _mapper.Map<Category>(category);
            _repository.Add(categoryToRepossitory);
        }

        public CategoryDTO GetById(int id)
        {
            var categoriesFromRepo = _repository.GetById(id);
            var category = _mapper.Map<CategoryDTO>(categoriesFromRepo);
            return category;
        }

        public List<CategoryDTO> GetWithInclude(params Expression<Func<Category, object>>[] includeProperties)
        {
            var categoriesFromRepo = _repository.GetWithInclude(includeProperties).ToList();
            var categories = _mapper.Map<List<CategoryDTO>>(categoriesFromRepo);

            return categories;
        }

        public List<CategoryDTO> GetWithInclude(Func<Category, bool> predicate, params Expression<Func<Category, object>>[] includeProperties)
        {
            var categoriesFromRepo = _repository.GetWithInclude(predicate, includeProperties);
            var categories = _mapper.Map<List<CategoryDTO>>(categoriesFromRepo);
            return categories;
        }

        public void Update(CategoryDTO category)
        {
            var categoryToRepo = _mapper.Map<Category>(category);
            _repository.Update(categoryToRepo);
        }

        public void Remove(CategoryDTO category)
        {
            var categoryToRepo = _mapper.Map<Category>(category);
            _repository.Remove(categoryToRepo);
        }
    }
}
