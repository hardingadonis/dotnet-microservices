using Catalog.Domain.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class CategoryContextSeed
    {
        public static async Task SeedData(IMongoCollection<Category> categoryCollection)
        {
            bool isExistCategory = await categoryCollection.Find(_ => true).AnyAsync();

            if (!isExistCategory)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "SeedData", "categories.json");
                string categoriesData = await File.ReadAllTextAsync(path);
                var categories = JsonSerializer.Deserialize<IEnumerable<Category>>(categoriesData);

                await categoryCollection.InsertManyAsync(categories);
            }
        }
    }
}
