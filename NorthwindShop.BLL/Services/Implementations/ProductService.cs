using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public async Task<List<ProductDTO>> Get()
        {
            var productsFromRepo = await _repository.Get();
            var products = _mapper.Map<List<ProductDTO>>(productsFromRepo);

            return products;
        }

        public async Task<List<ProductDTO>> Get(Expression<Func<Product, bool>> predicate)
        {
            var productsFromRepo = await _repository.Get(predicate);
            var products = _mapper.Map<List<ProductDTO>>(productsFromRepo);

            return products;
        }

        public async Task<ProductDTO> Add(ProductDTO product)
        {
            var productToRepository = _mapper.Map<Product>(product);
            var addedProduct = await _repository.Add(productToRepository);
            var productDto = _mapper.Map<ProductDTO>(addedProduct);

            return productDto;
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var productFromRepo = await _repository.GetById(id);
            var product = _mapper.Map<ProductDTO>(productFromRepo);

            return product;
        }

        public async Task<List<ProductDTO>> GetWithInclude(params Expression<Func<Product, object>>[] includeProperties)
        {
            var productsFromRepo = await _repository.GetWithInclude(includeProperties);
            var products = _mapper.Map<List<ProductDTO>>(productsFromRepo);

            return products;
        }

        public async Task<List<ProductDTO>> GetWithInclude(Expression<Func<Product, bool>> predicate, params Expression<Func<Product, object>>[] includeProperties)
        {
            var productFromRepo = await _repository.GetWithInclude(predicate, includeProperties);
            var products = _mapper.Map<List<ProductDTO>>(productFromRepo);

            return products;
        }

        public async Task<ProductDTO> Update(ProductDTO product)
        {
            var productToRepo = _mapper.Map<Product>(product);
            var updatedProduct = await _repository.Update(productToRepo);
            var productDto = _mapper.Map<ProductDTO>(updatedProduct);

            return productDto;
        }

        public async Task Remove(ProductDTO product)
        {
            var productToRepo = _mapper.Map<Product>(product);
            await _repository.Remove(productToRepo);
        }
    }
}
