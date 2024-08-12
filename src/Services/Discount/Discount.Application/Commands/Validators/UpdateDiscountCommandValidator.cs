using FluentValidation;

namespace Discount.Application.Commands.Validators
{
    public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
    {
        public UpdateDiscountCommandValidator()
        {
            RuleFor(o => o.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("{Id} is required.");

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
                .WithMessage("{Amount} should be greater than 0.");
        }
    }
}