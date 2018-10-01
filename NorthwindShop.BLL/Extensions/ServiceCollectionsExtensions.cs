using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthwindShop.DAL.Extensions;
using NorthwindShop.BLL.Services.Implementations;
using NorthwindShop.BLL.Services.Interfaces;

namespace NorthwindShop.BLL.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection RegisterBllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterRepositories(configuration);
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddAutoMapper();

            return services;
        }
    }
}
