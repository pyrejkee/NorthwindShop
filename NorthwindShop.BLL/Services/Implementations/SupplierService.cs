using AutoMapper;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.DAL.Entities;
using NorthwindShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NorthwindShop.BLL.Services.Implementations
{
    internal sealed class SupplierService : ISupplierService
    {
        private readonly IRepository<Supplier> _repository;
        private readonly IMapper _mapper;

        public SupplierService(IRepository<Supplier> repository,
                              IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SupplierDTO>> Get()
        {
            var suppliersFromRepo = await _repository.Get();
            var suppliers = _mapper.Map<List<SupplierDTO>>(suppliersFromRepo);

            return suppliers;
        }

        public async Task<List<SupplierDTO>> Get(Expression<Func<Supplier, bool>> predicate)
        {
            var suppliersFromRepo = await _repository.Get(predicate);
            var suppliers = _mapper.Map<List<SupplierDTO>>(suppliersFromRepo);

            return suppliers;
        }

        public async Task<SupplierDTO> Add(SupplierDTO supplier)
        {
            var supplierToRepossitory = _mapper.Map<Supplier>(supplier);
            var addedSupplier = await _repository.Add(supplierToRepossitory);
            var supplierDto = _mapper.Map<SupplierDTO>(addedSupplier);

            return supplierDto;

        }

        public async Task<SupplierDTO> GetById(int id)
        {
            var suppliersFromRepo = await _repository.GetById(id);
            var supplier = _mapper.Map<SupplierDTO>(suppliersFromRepo);

            return supplier;
        }

        public async Task<List<SupplierDTO>> GetWithInclude(params Expression<Func<Supplier, object>>[] includeProperties)
        {
            var suppliersFromRepo = await _repository.GetWithInclude(includeProperties);
            var suppliers = _mapper.Map<List<SupplierDTO>>(suppliersFromRepo);

            return suppliers;
        }

        public async Task<List<SupplierDTO>> GetWithInclude(Expression<Func<Supplier, bool>> predicate, params Expression<Func<Supplier, object>>[] includeProperties)
        {
            var suppliersFromRepo = await _repository.GetWithInclude(predicate, includeProperties);
            var suppliers = _mapper.Map<List<SupplierDTO>>(suppliersFromRepo);

            return suppliers;
        }

        public async Task<SupplierDTO> Update(SupplierDTO supplier)
        {
            var supplierToRepo = _mapper.Map<Supplier>(supplier);
            var updatedSupplier = await _repository.Update(supplierToRepo);
            var supplierDto = _mapper.Map<SupplierDTO>(updatedSupplier);

            return supplierDto;
        }

        public async Task Remove(SupplierDTO supplier)
        {
            var supplierToRepo = _mapper.Map<Supplier>(supplier);
            await _repository.Remove(supplierToRepo);
        }
    }
}
