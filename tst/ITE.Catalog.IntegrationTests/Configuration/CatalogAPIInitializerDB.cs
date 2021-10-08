using ITE.Catalog.Domain.Entities;
using ITE.Catalog.Infrastructure.Data;

namespace ApiCatalogoxUnitTests
{
    public class CatalogAPIInitializerDB
    {
        public CatalogAPIInitializerDB()
        { }
        public void Seed(CatalogDbContext context)
        {

            context.Products.Add(new ProductModel()
            {
                Title = "Sandalia",
                Description = "Sandália Preta Couro Salto Fino",
                Price = 249.50,
                Quantity = 100
            });

            context.Products.Add(new ProductModel()
            {
                Title = "Bolsa",
                Description = "Bola Preta Couro",
                Price = 499.90,
                Quantity = 750
            });

            context.Products.Add(new ProductModel()
            {
                Title = "Sapatilha",
                Description = "Sapatilha Tecido Platino ",
                Price = 142.50,
                Quantity = 25
            });

            context.Products.Add(new ProductModel()
            {
                Title = "Chinelo",
                Description = "Chinelo Tradicional Adulto-Unissex",
                Price = 60.50,
                Quantity = 50
            });

            context.SaveChanges();
        }

    }
}
