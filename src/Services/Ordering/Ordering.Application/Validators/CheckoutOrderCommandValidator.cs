using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(o => o.UserName)
                .NotEmpty()
                .WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(70)
                .WithMessage("{UserName} must not exceed 70 characters");

            RuleFor(o => o.TotalPrice)
                .NotEmpty()
                .NotNull()
                .WithMessage("{TotalPrice} is required.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("{TotalPrice} should not be negative.");

            RuleFor(o => o.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("{Email} is required");

            RuleFor(o => o.AddressLine)
                .NotEmpty()
                .NotNull()
                .WithMessage("{AddressLine} is required");

            RuleFor(o => o.Country)
                .NotEmpty()
                .NotNull()
                .WithMessage("{Country} is required");

            RuleFor(o => o.State)
                .NotEmpty()
                .NotNull()
                .WithMessage("{Country} is required");

            RuleFor(o => o.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage("{FirstName} is required");

            RuleFor(o => o.LastName)
                .NotEmpty()
                .NotNull()
                .WithMessage("{LastName} is required");
        }
    }
}