using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NorthwindShop.DAL.Entities;
using NorthwindShop.DAL.Interfaces;
using NorthwindShop.BLL.Services.Interfaces;

namespace NorthwindShop.BLL.Services.Implementations
{
    internal sealed class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public List<Product> Get()
        {
            return _repository.Get().ToList();
        }

        public List<Product> Get(Func<Product, bool> predicate)
        {
            return _repository.Get(predicate).ToList();
        }

        public void Add(Product product)
        {
            _repository.Add(product);
        }

        public Product GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<Product> GetWithInclude(params Expression<Func<Product, object>>[] includeProperties)
        {
            return _repository.GetWithInclude(includeProperties).ToList();
        }

        public List<Product> GetWithInclude(Func<Product, bool> predicate, params Expression<Func<Product, object>>[] includeProperties)
        {
            return _repository.GetWithInclude(predicate, includeProperties).ToList();
        }

        public void Update(Product product)
        {
            _repository.Update(product);
        }

        public void Remove(Product product)
        {
            _repository.Remove(product);
        }
    }
}
