using AutoMapper;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.Web.ViewModels;

namespace NorthwindShop.Web.Automapper
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<ProductDTO, ProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.QuantityPerUnit, opt => opt.MapFrom(src => src.QuantityPerUnit))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.UnitsInStock, opt => opt.MapFrom(src => src.UnitsInStock))
                .ForMember(dest => dest.UnitsOnOrder, opt => opt.MapFrom(src => src.UnitsOnOrder))
                .ForMember(dest => dest.Discontinued, opt => opt.MapFrom(src => src.Discontinued))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.CompanyName))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CreateProductViewModel, ProductDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ProductDTO, EditProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CategoryDTO, CategoryForProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CategoryDTO, CategoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Picture))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SupplierDTO, SupplierForProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
