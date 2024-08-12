using Basket.Domain.Exceptions;

namespace Basket.Application.Exceptions
{
    public class RequestHandlingException : BasketException
    {
        public string RequestName { get; }

        public RequestHandlingException(string requestName, string message, Exception innerException)
            : base(message, innerException)
        {
            RequestName = requestName;
        }
    }
}