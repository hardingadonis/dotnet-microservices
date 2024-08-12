using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class RequestHandlingException : OrderingException
    {
        public string RequestName { get; }

        public RequestHandlingException(string requestName, string message, Exception innerException)
            : base(message, innerException)
        {
            RequestName = requestName;
        }
    }
}