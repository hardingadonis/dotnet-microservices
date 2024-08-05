namespace Basket.Domain.Exceptions
{
    public class BasketException : Exception
    {
        public BasketException()
        {
        }

        public BasketException(string message)
            : base(message)
        {
        }

        public BasketException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
