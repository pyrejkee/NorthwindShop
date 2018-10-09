using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthwindShop.DAL.Interfaces;
using NorthwindShop.DAL.Repositories;

namespace NorthwindShop.DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<NorthwindContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("NorthwindShop.DAL"));
                });

            return services;
        }
    }
}
