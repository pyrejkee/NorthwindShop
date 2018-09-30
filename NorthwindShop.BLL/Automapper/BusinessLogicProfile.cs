using AutoMapper;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.DAL.Entities;

namespace NorthwindShop.BLL.Automapper
{
    class BusinessLogicProfile : Profile
    {
        public BusinessLogicProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.QuantityPerUnit, opt => opt.MapFrom(src => src.QuantityPerUnit))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.UnitsInStock, opt => opt.MapFrom(src => src.UnitsInStock))
                .ForMember(dest => dest.UnitsOnOrder, opt => opt.MapFrom(src => src.UnitsOnOrder))
                .ForMember(dest => dest.Discontinued, opt => opt.MapFrom(src => src.Discontinued))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.Picture))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Supplier, SupplierDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SupplierId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactName))
                .ForMember(dest => dest.ContactTitle, opt => opt.MapFrom(src => src.ContactTitle))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Fax))
                .ForMember(dest => dest.HomePage, opt => opt.MapFrom(src => src.HomePage));
        }
    }
}
