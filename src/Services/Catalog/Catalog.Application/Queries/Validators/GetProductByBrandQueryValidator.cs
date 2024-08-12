using FluentValidation;

namespace Catalog.Application.Queries.Validators
{
    public class GetProductByBrandQueryValidator : AbstractValidator<GetProductByBrandQuery>
    {
        public GetProductByBrandQueryValidator()
        {
            RuleFor(x => x.BrandName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{BrandName} is required.");
        }
    }
}