using Ordering.Domain.Entities;
using System.Linq.Expressions;

namespace Ordering.Domain.Repositories
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        Task<T?> GetByIdAsync(long id);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}