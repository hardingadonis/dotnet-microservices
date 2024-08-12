using Catalog.Domain.Exceptions;

namespace Catalog.Application.Exceptions
{
    public class RequestHandlingException : CatalogException
    {
        public string RequestName { get; }

        public RequestHandlingException(string requestName, string message, Exception innerException)
            : base(message, innerException)
        {
            RequestName = requestName;
        }
    }
}