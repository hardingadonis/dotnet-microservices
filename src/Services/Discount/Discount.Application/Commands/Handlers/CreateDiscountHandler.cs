using Discount.Application.Exceptions;
using Discount.Application.Mappers;
using Discount.Domain.Entities;
using Discount.Domain.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands.Handlers
{
    public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;

        public CreateDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }

        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = DiscountMapper.Mapper.Map<Coupon>(request);
            var result = await _discountRepository.CreateDiscount(coupon);

            if (result != null)
            {
                return DiscountMapper.Mapper.Map<CouponModel>(result);
            }

            throw new CreateDiscountException(request.ProductName);
        }
    }
}