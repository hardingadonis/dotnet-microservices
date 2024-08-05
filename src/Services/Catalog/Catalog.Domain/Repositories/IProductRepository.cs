using Catalog.Domain.Entities;
using Catalog.Domain.Specs;

namespace Catalog.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Pagination<Product>> GetProducts(CatalogSpecParams catalogSpecParams);

        Task<Product?> GetById(string id);

        Task<IEnumerable<Product>> GetByName(string name);

        Task<IEnumerable<Product>> GetByBrand(string brandName);

        Task<Product> Create(Product product);

        Task<bool> Update(Product product);

        Task<bool> DeleteById(string id);
    }
}
