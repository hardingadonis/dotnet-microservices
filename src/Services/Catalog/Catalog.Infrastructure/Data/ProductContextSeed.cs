using Catalog.Domain.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class ProductContextSeed
    {
        public static async void SeedData(IMongoCollection<Product> productCollection)
        {
            bool isExistProduct = productCollection.Find(_ => true).Any();

            if (!isExistProduct)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "SeedData", "products.json");
                string productsData = File.ReadAllText(path);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(productsData);

                await productCollection.InsertManyAsync(products);
            }
        }
    }
}
