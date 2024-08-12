using Discount.Domain.Exceptions;

namespace Discount.Application.Exceptions
{
    public class RequestHandlingException : DiscountException
    {
        public string RequestName { get; }

        public RequestHandlingException(string requestName, string message, Exception innerException)
            : base(message, innerException)
        {
            RequestName = requestName;
        }
    }
}