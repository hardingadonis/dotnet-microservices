using FluentValidation;

namespace Catalog.Application.Queries.Validators
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("{Id} is required")
                .MinimumLength(24)
                .MaximumLength(24)
                .WithMessage("{Id} must be 24 characters");
        }
    }
}