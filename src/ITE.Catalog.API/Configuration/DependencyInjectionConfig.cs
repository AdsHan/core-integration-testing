using ITE.Catalog.API.Service;
using ITE.Catalog.Domain.Repositories;
using ITE.Catalog.Infrastructure.Data;
using ITE.Catalog.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITE.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {

        public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options => options.UseInMemoryDatabase("ProductsDB"));

            services.AddTransient<ProductPopulateService>();

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}