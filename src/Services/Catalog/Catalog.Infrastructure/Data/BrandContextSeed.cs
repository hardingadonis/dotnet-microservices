using Catalog.Domain.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        public static async Task SeedData(IMongoCollection<Brand> brandCollection)
        {
            bool isExistBrand = await brandCollection.Find(_ => true).AnyAsync();

            if (!isExistBrand)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "SeedData", "brands.json");
                string barndsData = await File.ReadAllTextAsync(path);
                var brands = JsonSerializer.Deserialize<IEnumerable<Brand>>(barndsData);

                await brandCollection.InsertManyAsync(brands);
            }
        }
    }
}
