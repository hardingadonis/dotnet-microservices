using Grpc.Core;

namespace Discount.Application.Exceptions
{
    public class DiscountNotFoundException : RpcException
    {
        public string ProductName { get; private set; }

        public DiscountNotFoundException(string productName)
            : base(new Status(StatusCode.NotFound, $"Discount with ProductName: {productName} was not found"))
        {
            ProductName = productName;
        }
    }
}