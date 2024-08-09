using MediatR;
using Ordering.Application.Exceptions;
using Ordering.Application.Mappers;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;

namespace Ordering.Application.Commands.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);
            if (order == null)
            {
                throw new OrderNotFoundException(nameof(Order), request.Id);
            }

            OrderingMapper.Mapper.Map(request, order);

            await _orderRepository.UpdateAsync(order);

            return Unit.Value;
        }
    }
}