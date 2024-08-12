using FluentValidation;

namespace Basket.Application.Commands.Validators
{
    public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
    {
        public CreateBasketCommandValidator()
        {
            RuleFor(o => o.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{UserName} is required");

            RuleFor(o => o.Items)
                .NotNull()
                .NotEmpty()
                .WithMessage("{Items} is required")
                .Must(o => o.Count > 1)
                .WithMessage("{Items} must contain at least one item");
        }
    }
}