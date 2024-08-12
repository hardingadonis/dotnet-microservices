using FluentValidation;

namespace Catalog.Application.Queries.Validators
{
    public class GetProductByNameQueryValidator : AbstractValidator<GetProductByNameQuery>
    {
        public GetProductByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{Name} is required");
        }
    }
}