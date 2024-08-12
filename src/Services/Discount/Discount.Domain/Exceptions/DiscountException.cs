namespace Discount.Domain.Exceptions
{
    public class DiscountException : Exception
    {
        public DiscountException()
        {
        }

        public DiscountException(string message)
            : base(message)
        {
        }

        public DiscountException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}