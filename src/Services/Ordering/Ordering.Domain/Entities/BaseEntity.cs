namespace Ordering.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; protected set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}