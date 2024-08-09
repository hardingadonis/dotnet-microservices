using MediatR;
using Ordering.Application.Mappers;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;

namespace Ordering.Application.Commands.Handlers
{
    public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, long>
    {
        private readonly IOrderRepository _orderRepository;

        public CheckoutOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<long> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = OrderingMapper.Mapper.Map<Order>(request);
            var generateOrder = await _orderRepository.AddAsync(order);

            return generateOrder.Id;
        }
    }
}