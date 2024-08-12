using FluentValidation;

namespace Discount.Application.Queries.Validators
{
    public class GetDiscountQueryValidator : AbstractValidator<GetDiscountQuery>
    {
        public GetDiscountQueryValidator()
        {
            RuleFor(o => o.ProductName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{ProductName} is required.");
        }
    }
}