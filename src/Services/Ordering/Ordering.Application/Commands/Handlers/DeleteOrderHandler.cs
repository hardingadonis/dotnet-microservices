using MediatR;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;

namespace Ordering.Application.Commands.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order == null)
            {
                throw new OrderNotFoundException(nameof(Order), request.Id);
            }

            await _orderRepository.DeleteAsync(order);

            return Unit.Value;
        }
    }
}