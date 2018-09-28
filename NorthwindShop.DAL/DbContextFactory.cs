using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NorthwindShop.DAL
{
    public class DbContextFactory : IDesignTimeDbContextFactory<NorthwindContext>
    {
        public NorthwindContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<NorthwindContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            dbContextBuilder.UseSqlServer(connectionString);

            return new NorthwindContext(dbContextBuilder.Options);
        }
    }
}
