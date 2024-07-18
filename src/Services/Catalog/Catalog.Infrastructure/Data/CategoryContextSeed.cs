using Catalog.Domain.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class CategoryContextSeed
    {
        public static async void SeedData(IMongoCollection<Category> categoryCollection)
        {
            bool isExistCategory = categoryCollection.Find(_ => true).Any();

            if (!isExistCategory)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "SeedData", "categories.json");
                string categoriesData = File.ReadAllText(path);
                var categories = JsonSerializer.Deserialize<IEnumerable<Category>>(categoriesData);

                await categoryCollection.InsertManyAsync(categories);
            }
        }
    }
}
