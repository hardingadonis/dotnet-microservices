using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Queries
{
    public record GetOrdersByUserQuery(string UserName) : IRequest<IEnumerable<OrderResponse>>;
}