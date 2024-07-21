using Catalog.Domain.Exceptions;

namespace Catalog.Application.Exceptions
{
    public class CategoryNotFoundException : CatalogException
    {
        public string CategoryId { get; set; }

        public CategoryNotFoundException(string categoryId)
            : base($"Category with id: {categoryId} was not found.")
        {
            CategoryId = categoryId;
        }
    }
}
