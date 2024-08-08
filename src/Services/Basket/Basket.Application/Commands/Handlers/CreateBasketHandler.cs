using Basket.Application.GrpcServices;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Domain.Entities;
using Basket.Domain.Repositories;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Commands.Handlers
{
    public class CreateBasketHandler : IRequestHandler<CreateBasketCommand, BasketResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly ILogger<CreateBasketHandler> _logger;

        public CreateBasketHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService, ILogger<CreateBasketHandler> logger)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BasketResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Items)
            {
                try
                {
                    var coupon = await _discountGrpcService.GetDiscount(item.ProductName);

                    item.Price -= coupon.Amount;
                }
                catch (RpcException ex)
                {
                    _logger.LogInformation("No coupon availble for product with name = {ProductName}", item.ProductName);
                    _logger.LogDebug("Discount grpc service is not available. {StatusCode} - {StatusDetail}", ex.StatusCode, ex.Status.Detail);
                }
            }

            var shoppingCart = new ShoppingCart
            {
                UserName = request.UserName,
                Items = request.Items
            };

            var result = await _basketRepository.UpdateBasket(shoppingCart);

            return BasketMapper.Mapper.Map<BasketResponse>(result);
        }
    }
}
