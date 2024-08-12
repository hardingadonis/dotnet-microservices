using FluentValidation;

namespace Catalog.Application.Commands.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(o => o.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{Name} is required");

            RuleFor(o => o.Summary)
                .NotNull()
                .NotEmpty()
                .WithMessage("{Summary} is required");

            RuleFor(o => o.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("{Description} is required");

            RuleFor(o => o.ImageFile)
                .NotNull()
                .NotEmpty()
                .WithMessage("{ImageFile} is required");

            RuleFor(o => o.Price)
                .GreaterThan(0)
                .WithMessage("{Price} should be greater than 0");

            RuleFor(o => o.Quantity)
                .GreaterThan(1)
                .WithMessage("{Quantity} should be greater than 1");

            RuleFor(o => o.BrandId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{BrandId} is required");

            RuleFor(o => o.CategoryId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{CategoryId} is required");
        }
    }
}