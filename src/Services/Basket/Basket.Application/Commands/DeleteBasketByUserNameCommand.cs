using MediatR;

namespace Basket.Application.Commands
{
    public record DeleteBasketByUserNameCommand
    (
        string UserName
    ) : IRequest<bool>;
}