using Catalog.Domain.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class ProductContextSeed
    {
        public static async Task SeedData(IMongoCollection<Product> productCollection)
        {
            bool isExistProduct = await productCollection.Find(_ => true).AnyAsync();

            if (!isExistProduct)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "SeedData", "products.json");
                string productsData = await File.ReadAllTextAsync(path);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(productsData);

                await productCollection.InsertManyAsync(products);
            }
        }
    }
}
