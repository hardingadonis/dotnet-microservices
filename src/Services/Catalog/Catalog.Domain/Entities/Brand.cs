using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Domain.Entities
{
    public class Brand : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;
    }
}