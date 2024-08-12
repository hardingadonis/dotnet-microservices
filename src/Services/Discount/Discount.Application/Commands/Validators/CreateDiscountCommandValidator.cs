using FluentValidation;

namespace Discount.Application.Commands.Validators
{
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(o => o.ProductName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{ProductName} is required.");

            RuleFor(o => o.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("{Description} is required.");

            RuleFor(o => o.Amount)
                .GreaterThan(0)
                .WithMessage("{Amount} must be greater than 0.");
        }
    }
}