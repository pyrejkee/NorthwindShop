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

        public List<SupplierDTO> Get()
        {
            var suppliersFromRepo = _repository.Get().ToList();
            var suppliers = _mapper.Map<List<SupplierDTO>>(suppliersFromRepo);

            return suppliers;
        }

        public List<SupplierDTO> Get(Func<Supplier, bool> predicate)
        {
            var suppliersFromRepo = _repository.Get(predicate);
            var suppliers = _mapper.Map<List<SupplierDTO>>(suppliersFromRepo);
            return suppliers;
        }

        public void Add(SupplierDTO supplier)
        {
            var supplierToRepossitory = _mapper.Map<Supplier>(supplier);
            _repository.Add(supplierToRepossitory);
        }

        public SupplierDTO GetById(int id)
        {
            var suppliersFromRepo = _repository.GetById(id);
            var supplier = _mapper.Map<SupplierDTO>(suppliersFromRepo);
            return supplier;
        }

        public List<SupplierDTO> GetWithInclude(params Expression<Func<Supplier, object>>[] includeProperties)
        {
            var suppliersFromRepo = _repository.GetWithInclude(includeProperties).ToList();
            var suppliers = _mapper.Map<List<SupplierDTO>>(suppliersFromRepo);

            return suppliers;
        }

        public List<SupplierDTO> GetWithInclude(Func<Supplier, bool> predicate, params Expression<Func<Supplier, object>>[] includeProperties)
        {
            var suppliersFromRepo = _repository.GetWithInclude(predicate, includeProperties);
            var suppliers = _mapper.Map<List<SupplierDTO>>(suppliersFromRepo);
            return suppliers;
        }

        public void Update(SupplierDTO supplier)
        {
            var categoryToRepo = _mapper.Map<Supplier>(supplier);
            _repository.Update(categoryToRepo);
        }

        public void Remove(SupplierDTO supplier)
        {
            var supplierToRepo = _mapper.Map<Supplier>(supplier);
            _repository.Remove(supplierToRepo);
        }
    }
}
