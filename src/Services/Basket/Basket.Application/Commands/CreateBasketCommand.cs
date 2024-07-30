using Basket.Application.Responses;
using Basket.Domain.Entities;
using MediatR;

namespace Basket.Application.Commands
{
    public class CreateBasketCommand : IRequest<BasketResponse>
    {
        public string UserName { get; set; } = string.Empty;

        public List<ShoppingCartItem> Items { get; set; } = [];
    }
}
