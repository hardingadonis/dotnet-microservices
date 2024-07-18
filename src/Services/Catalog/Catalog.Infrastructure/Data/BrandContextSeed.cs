using Catalog.Domain.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        public static async void SeedData(IMongoCollection<Brand> brandCollection)
        {
            bool isExistBrand = brandCollection.Find(_ => true).Any();

            if (!isExistBrand)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "SeedData", "brands.json");
                string barndsData = File.ReadAllText(path);
                var brands = JsonSerializer.Deserialize<IEnumerable<Brand>>(barndsData);

                await brandCollection.InsertManyAsync(brands);
            }
        }
    }
}
