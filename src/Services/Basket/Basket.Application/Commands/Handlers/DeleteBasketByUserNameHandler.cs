using Basket.Application.Exceptions;
using Basket.Domain.Repositories;
using MediatR;

namespace Basket.Application.Commands.Handlers
{
    public class DeleteBasketByUserNameHandler : IRequestHandler<DeleteBasketByUserNameCommand, bool>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteBasketByUserNameHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        public async Task<bool> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            return await _basketRepository.DeleteBasket(request.UserName) ? true : throw new DeleteBasketException(request.UserName);
        }
    }
}