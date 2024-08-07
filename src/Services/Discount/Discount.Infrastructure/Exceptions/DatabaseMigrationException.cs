namespace Discount.Infrastructure.Exceptions
{
    public class DatabaseMigrationException : Exception
    {
        public DatabaseMigrationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}