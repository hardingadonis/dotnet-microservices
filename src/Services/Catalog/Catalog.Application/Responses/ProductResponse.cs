using Catalog.Domain.Entities;

namespace Catalog.Application.Responses
{
    public class ProductResponse
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageFile { get; set; } = string.Empty;

        public Brand? Brand { get; set; }

        public Category? Category { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
