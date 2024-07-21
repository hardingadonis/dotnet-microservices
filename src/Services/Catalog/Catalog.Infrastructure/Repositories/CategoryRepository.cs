using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class CategoryRepository(ICatalogContext context) : ICategoryRepository
    {
        private readonly ICatalogContext _context = context;

        public async Task<IEnumerable<Category>> GetAll()
            => await _context.Categories.Find(_ => true).ToListAsync();

        public async Task<Category?> GetById(string id)
        {
            var filter = Builders<Category>.Filter.Eq(category => category.Id, id);

            return await _context.Categories.Find(filter).FirstOrDefaultAsync();
        }
    }
}
