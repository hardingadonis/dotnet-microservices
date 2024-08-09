using Ordering.Domain.Exceptions;

namespace Ordering.Application.Exceptions
{
    public class OrderNotFoundException : OrderingException
    {
        public OrderNotFoundException(string name, object key)
            : base($"Order {name} with Id: {key} was not found")
        {
        }
    }
}