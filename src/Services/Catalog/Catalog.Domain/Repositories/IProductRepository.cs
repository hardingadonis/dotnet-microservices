using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product?> GetById(string id);

        Task<IEnumerable<Product>> GetByName(string name);

        Task<IEnumerable<Product>> GetByBrand(string brandName);

        Task<Product> Create(Product product);

        Task<bool> Update(Product product);

        Task<bool> DeleteById(string id);
    }
}
