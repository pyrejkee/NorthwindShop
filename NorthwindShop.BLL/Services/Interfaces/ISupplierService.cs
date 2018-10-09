using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NorthwindShop.BLL.Services.Interfaces
{
    public interface ISupplierService
    {
        void Add(SupplierDTO supplier);
        SupplierDTO GetById(int id);
        List<SupplierDTO> Get();
        List<SupplierDTO> Get(Func<Supplier, bool> predicate);
        List<SupplierDTO> GetWithInclude(params Expression<Func<Supplier, object>>[] includeProperties);
        List<SupplierDTO> GetWithInclude(Func<Supplier, bool> predicate, params Expression<Func<Supplier, object>>[] includeProperties);
        void Update(SupplierDTO supplier);
        void Remove(SupplierDTO supplier);
    }
}
