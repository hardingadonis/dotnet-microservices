using Discount.Domain.Exceptions;

namespace Discount.Infrastructure.Exceptions
{
    public class DatabaseMigrationException : DiscountException
    {
        public DatabaseMigrationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}