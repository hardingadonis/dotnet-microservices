using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product?> GetById(string id);

        Task<IEnumerable<Product>> GetProductByName(string name);

        Task<Product> Create(Product product);

        Task<Product> Update(Product product);

        Task<bool> Delete(string id);
    }
}
