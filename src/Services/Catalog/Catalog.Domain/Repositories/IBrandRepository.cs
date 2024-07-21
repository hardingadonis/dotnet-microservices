using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAll();

        Task<Brand?> GetById(string id);
    }
}