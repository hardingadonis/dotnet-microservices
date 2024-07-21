using MediatR;

namespace Catalog.Application.Commands
{
    public record UpdateProductCommand
    (
        string Id,
        string Name,
        string Summary,
        string Description,
        string ImageFile,
        decimal Price,
        int Quantity,
        string BrandId,
        string CategoryId
    ) : IRequest<bool>;
}