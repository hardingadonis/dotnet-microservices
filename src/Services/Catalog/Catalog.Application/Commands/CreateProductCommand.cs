using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands
{
    public record CreateProductCommand
    (
        string Name,
        string Summary,
        string Description,
        string ImageFile,
        decimal Price,
        int Quantity,
        string BrandId,
        string CategoryId
    ) : IRequest<ProductResponse>;
}
