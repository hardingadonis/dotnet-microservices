using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Queries
{
    public record GetDiscountQuery
    (
        string ProductName
    ) : IRequest<CouponModel>;
}