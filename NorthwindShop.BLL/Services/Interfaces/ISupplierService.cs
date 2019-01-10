using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NorthwindShop.BLL.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<SupplierDTO> Add(SupplierDTO supplier);

        Task<SupplierDTO> GetById(int id);

        Task<List<SupplierDTO>> Get();

        Task<List<SupplierDTO>> Get(Expression<Func<Supplier, bool>> predicate);

        Task<List<SupplierDTO>> GetWithInclude(params Expression<Func<Supplier, object>>[] includeProperties);

        Task<List<SupplierDTO>> GetWithInclude(Expression<Func<Supplier, bool>> predicate, params Expression<Func<Supplier, object>>[] includeProperties);

        Task<SupplierDTO> Update(SupplierDTO supplier);

        Task Remove(SupplierDTO supplier);
    }
}
