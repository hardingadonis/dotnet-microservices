using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.Domain.Entities
{
    public class Product : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageFile { get; set; } = string.Empty;

        public Brand? Brand { get; set; }

        public Category? Category { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
    }
}
