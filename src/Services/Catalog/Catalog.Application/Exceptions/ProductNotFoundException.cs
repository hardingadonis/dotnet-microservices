using Catalog.Domain.Exceptions;

namespace Catalog.Application.Exceptions
{
    public class ProductNotFoundException : CatalogException
    {
        public ProductNotFoundException(string message)
            : base($"Product with {message} was not found.")
        {
        }
    }
}
