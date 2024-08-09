using MediatR;
using Ordering.Application.Exceptions;
using Ordering.Application.Mappers;
using Ordering.Application.Responses;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;

namespace Ordering.Application.Queries.Handlers
{
    public class GetOrdersByUserHandler : IRequestHandler<GetOrdersByUserQuery, IEnumerable<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByUserHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByUserName(request.UserName);

            if (orders != null)
            {
                return OrderingMapper.Mapper.Map<IEnumerable<OrderResponse>>(orders);
            }

            throw new OrderNotFoundException(nameof(Order), request.UserName);
        }
    }
}