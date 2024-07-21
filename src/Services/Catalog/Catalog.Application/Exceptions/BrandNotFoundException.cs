using Catalog.Domain.Exceptions;

namespace Catalog.Application.Exceptions
{
    public class BrandNotFoundException : CatalogException
    {
        public string BrandId { get; set; }

        public BrandNotFoundException(string brandId)
            : base($"Brand with id: {brandId} was not found.")
        {
            BrandId = brandId;
        }
    }
}
