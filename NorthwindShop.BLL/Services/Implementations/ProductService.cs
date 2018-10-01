using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.DAL.Entities;
using NorthwindShop.DAL.Interfaces;
using NorthwindShop.BLL.Services.Interfaces;

namespace NorthwindShop.BLL.Services.Implementations
{
    internal sealed class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> repository,
                              IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<ProductDTO> Get()
        {
            var productsFromRepo = _repository.Get().ToList();
            var products = _mapper.Map<List<ProductDTO>>(productsFromRepo);

            return products;
        }

        public List<ProductDTO> Get(Func<Product, bool> predicate)
        {
            var productsFromRepo = _repository.Get(predicate);
            var products = _mapper.Map<List<ProductDTO>>(productsFromRepo);
            return products;
        }

        public void Add(ProductDTO product)
        {
            var productToRepository = _mapper.Map<Product>(product);
            _repository.Add(productToRepository);
        }

        public ProductDTO GetById(int id)
        {
            var productFromRepo = _repository.GetById(id);
            var product = _mapper.Map<ProductDTO>(productFromRepo);
            return product;
        }

        public List<ProductDTO> GetWithInclude(params Expression<Func<Product, object>>[] includeProperties)
        {
            var productsFromRepo = _repository.GetWithInclude(includeProperties).ToList();
            var products = _mapper.Map<List<ProductDTO>>(productsFromRepo);

            return products;
        }

        public List<ProductDTO> GetWithInclude(Func<Product, bool> predicate, params Expression<Func<Product, object>>[] includeProperties)
        {
            var productFromRepo = _repository.GetWithInclude(predicate, includeProperties);
            var products = _mapper.Map<List<ProductDTO>>(productFromRepo);
            return products;
        }

        public void Update(ProductDTO product)
        {
            var productToRepo = _mapper.Map<Product>(product);
            _repository.Update(productToRepo);
        }

        public void Remove(ProductDTO product)
        {
            var productToRepo = _mapper.Map<Product>(product);
            _repository.Remove(productToRepo);
        }
    }
}
