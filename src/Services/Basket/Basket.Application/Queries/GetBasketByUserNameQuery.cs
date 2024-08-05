using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Queries
{
    public class GetBasketByUserNameQuery(string userName) : IRequest<BasketResponse>
    {
        public string UserName { get; set; } = userName;
    }
}
