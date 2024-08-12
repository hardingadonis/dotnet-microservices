using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands
{
    public record CreateDiscountCommand
    (
        string ProductName,
        string Description,
        int Amount
    ) : IRequest<CouponModel>;
}