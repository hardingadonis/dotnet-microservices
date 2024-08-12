using Basket.Application.Responses;
using Basket.Domain.Entities;
using MediatR;

namespace Basket.Application.Commands
{
    public record CreateBasketCommand
    (
        string UserName,
        IList<ShoppingCartItem> Items
    ) : IRequest<BasketResponse>;
}
