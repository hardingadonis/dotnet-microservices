using Discount.Application.Exceptions;
using Discount.Application.Mappers;
using Discount.Domain.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Queries.Handlers
{
    public class GetDiscountHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;

        public GetDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }

        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);

            if (coupon != null)
            {
                return DiscountMapper.Mapper.Map<CouponModel>(coupon);
            }

            throw new DiscountNotFoundException(request.ProductName);
        }
    }
}