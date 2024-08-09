using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderingContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrderingContext(DbContextOptions<OrderingContext> options)
            : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "adonis";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = "adonis";
                        break;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}