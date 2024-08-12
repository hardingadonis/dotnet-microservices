using FluentValidation;

namespace Basket.Application.Queries.Validators
{
    public class GetBasketByUserNameQueryValidator : AbstractValidator<GetBasketByUserNameQuery>
    {
        public GetBasketByUserNameQueryValidator()
        {
            RuleFor(o => o.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{UserName} is required");
        }
    }
}