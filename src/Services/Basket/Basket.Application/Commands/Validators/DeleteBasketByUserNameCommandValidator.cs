using FluentValidation;

namespace Basket.Application.Commands.Validators
{
    public class DeleteBasketByUserNameCommandValidator : AbstractValidator<DeleteBasketByUserNameCommand>
    {
        public DeleteBasketByUserNameCommandValidator()
        {
            RuleFor(o => o.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{UserName} is required");
        }
    }
}