using FluentValidation;

namespace Discount.Application.Commands.Validators
{
    public class DeleteDiscountCommandValidator : AbstractValidator<DeleteDiscountCommand>
    {
        public DeleteDiscountCommandValidator()
        {
            RuleFor(o => o.ProductName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{ProductName} is required.");
        }
    }
}