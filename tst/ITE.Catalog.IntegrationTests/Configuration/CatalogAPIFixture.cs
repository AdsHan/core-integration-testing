using ITE.Catalog.API;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace ITE.Catalog.IntegrationTests.Configuration
{
    [CollectionDefinition(nameof(TestsFixtureCollection))]
    public class TestsFixtureCollection : ICollectionFixture<CatalogAPIFixture<StartupTests>> { }

    public class CatalogAPIFixture<T> : IDisposable where T : class
    {
        public readonly CatalogAPIFactory<T> Factory;
        public HttpClient Client;

        public CatalogAPIFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost"),
            };

            Factory = new CatalogAPIFactory<T>();
            Client = Factory.CreateClient(clientOptions);
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}