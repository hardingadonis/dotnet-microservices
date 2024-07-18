using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Domain.Entities
{
    public class Category : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;
    }
}