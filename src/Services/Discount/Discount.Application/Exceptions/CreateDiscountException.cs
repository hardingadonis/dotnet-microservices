using Grpc.Core;

namespace Discount.Application.Exceptions
{
    public class CreateDiscountException : RpcException
    {
        public string ProductName { get; private set; }

        public CreateDiscountException(string productName)
            : base(new Status(StatusCode.AlreadyExists, $"Discount with ProductName: {productName} was not created"))
        {
            ProductName = productName;
        }
    }
}