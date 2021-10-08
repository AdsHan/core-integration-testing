using ApiCatalogoxUnitTests;
using ITE.Catalog.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ITE.Catalog.IntegrationTests.Configuration
{
    public class CatalogAPIFactory<T> : WebApplicationFactory<T> where T : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<T>();
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                //services.AddScoped<DBUnitTestsMockInitializer>();

                // Cria o service provider
                var serviceProvider = services.BuildServiceProvider();

                // Cria o scope para obter o context
                using var scope = serviceProvider.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

                // Força para ter certeza que o banco foi criado
                context.Database.EnsureCreated();

                try
                {
                    CatalogAPIInitializerDB db = new CatalogAPIInitializerDB();
                    db.Seed(context);
                }
                catch (Exception ex)
                {

                }

            });
        }
    }
}