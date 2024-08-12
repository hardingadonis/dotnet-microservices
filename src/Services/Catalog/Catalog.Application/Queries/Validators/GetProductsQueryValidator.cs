using FluentValidation;

namespace Catalog.Application.Queries.Validators
{
    public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(x => x.CatalogSpecParams.PageIndex)
                .GreaterThanOrEqualTo(1)
                .WithMessage("{PageIndex} must be greater than or equal to 1");

            RuleFor(x => x.CatalogSpecParams.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("{PageSize} must be greater than or equal to 1");
        }
    }
}