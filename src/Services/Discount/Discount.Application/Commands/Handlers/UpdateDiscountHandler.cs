using Discount.Application.Exceptions;
using Discount.Application.Mappers;
using Discount.Domain.Entities;
using Discount.Domain.Repositories;
using MediatR;

namespace Discount.Application.Commands.Handlers
{
    public class UpdateDiscountHandler : IRequestHandler<UpdateDiscountCommand, bool>
    {
        private readonly IDiscountRepository _discountRepository;

        public UpdateDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ??  throw new ArgumentNullException(nameof(discountRepository));
        }

        public async Task<bool> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = DiscountMapper.Mapper.Map<Coupon>(request);
            var result = await _discountRepository.UpdateDiscount(coupon);

            return result ? result : throw new DiscountNotFoundException(coupon.ProductName);
        }
    }
}