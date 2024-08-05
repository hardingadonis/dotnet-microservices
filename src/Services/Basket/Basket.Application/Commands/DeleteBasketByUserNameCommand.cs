using MediatR;

namespace Basket.Application.Commands
{
    public class DeleteBasketByUserNameCommand(string userName) : IRequest<bool>
    {
        public string UserName { get; set; } = userName;
    }
}