namespace Discount.API.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IMediator mediator, ILogger<DiscountService> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetDiscountQuery(request.ProductName));

            _logger.LogInformation($"Executing: {nameof(GetDiscountQuery)}");
            _logger.LogDebug("Discount is retrieved for ProductName : {ProductName}", request.ProductName);

            return result;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = new CreateDiscountCommand(
                ProductName: request.Coupon.ProductName,
                Description: request.Coupon.Description,
                Amount: request.Coupon.Amount
            );

            var result = await _mediator.Send(coupon);

            _logger.LogInformation($"Executing: {nameof(CreateDiscountCommand)}");
            _logger.LogDebug("Discount is created for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);

            return result;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new DeleteDiscountCommand(request.ProductName));

            _logger.LogInformation($"Executing: {nameof(DeleteDiscountCommand)}");
            _logger.LogDebug("Discount is deleted for ProductName : {ProductName}", request.ProductName);

            var response = new DeleteDiscountResponse
            {
                Success = result
            };

            return response;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = new UpdateDiscountCommand(
                Id: request.Coupon.Id,
                ProductName: request.Coupon.ProductName,
                Description: request.Coupon.Description,
                Amount: request.Coupon.Amount
            );

            await _mediator.Send(coupon);

            _logger.LogInformation($"Executing: {nameof(UpdateDiscountCommand)}");
            _logger.LogDebug("Discount is updated for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);

            return request.Coupon;
        }
    }
}