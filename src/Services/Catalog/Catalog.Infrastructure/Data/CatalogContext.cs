using Catalog.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<Category> Categories { get; }

        public IMongoCollection<Brand> Brands { get; }

        public CatalogContext(IConfiguration configuration)
        {
            // New instance of MongoClient & get the database
            var client = new MongoClient(
                configuration.GetValue<string>("DatabaseSettings:ConnectionString")
                );
            var database = client.GetDatabase(
                configuration.GetValue<string>("DatabaseSettings:DatabaseName")
                );

            // Get the collections
            Products = database.GetCollection<Product>(
                configuration.GetValue<string>("DatabaseSettings:ProductsCollection")
                );
            Categories = database.GetCollection<Category>(
                configuration.GetValue<string>("DatabaseSettings:CategoriesCollection")
                );
            Brands = database.GetCollection<Brand>(
                configuration.GetValue<string>("DatabaseSettings:BrandsCollection")
                );

            // Seed the data
            var brandTask = BrandContextSeed.SeedData(Brands);
            var categoryTask = CategoryContextSeed.SeedData(Categories);
            var productTask = ProductContextSeed.SeedData(Products);

            Task.WhenAll(brandTask, categoryTask, productTask).Wait();
        }
    }
}
