using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();

        Task<Category?> GetById(string id);
    }
}
