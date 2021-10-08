using ITE.Catalog.API;
using ITE.Catalog.Domain.Entities;
using ITE.Catalog.IntegrationTests.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace ITE.Catalog.IntegrationTests
{

    [Collection(nameof(TestsFixtureCollection))]
    public class CatalogApiTests
    {
        private readonly CatalogAPIFixture<StartupTests> _testsFixture;

        public CatalogApiTests(CatalogAPIFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Obtêm todos os produtos")]
        [Trait("Layer", "Application - API")]
        public async Task ReturnAllProducts_CallAPI_ReturnStatusCodeOK()
        {
            // Arrange            
            _testsFixture.Client.DefaultRequestHeaders.Clear();
            _testsFixture.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await _testsFixture.Client.GetAsync("api/products/");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductModel>>(stringResponse);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(4, products.Count);
        }

        [Fact(DisplayName = "O produto não foi localizado")]
        [Trait("Layer", "Application - API")]
        public async Task ProductInvalid_CallAPI_ReturnStatusCodeNotFound()
        {
            // Arrange            
            _testsFixture.Client.DefaultRequestHeaders.Clear();
            _testsFixture.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await _testsFixture.Client.GetAsync($"api/products/584f4e68-4394-4ea1-aa56-4658e2a5c66e");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "Inclusão de produto")]
        [Trait("Layer", "Application - API")]
        public async Task AddProduct_CallAPI_ReturnStatusCode201()
        {
            // Arrange
            var newProduct = new ProductModel()
            {
                Title = "Bolsa",
                Description = "Bola Preta Couro",
                Price = 499.90,
                Quantity = 750
            };

            // Act
            var response = await _testsFixture.Client.PostAsJsonAsync("api/products/", newProduct);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}