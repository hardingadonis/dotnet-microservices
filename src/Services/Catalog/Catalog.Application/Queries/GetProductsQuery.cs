using Catalog.Application.Responses;
using Catalog.Domain.Specs;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetProductsQuery
    (
        CatalogSpecParams CatalogSpecParams
    ) : IRequest<Pagination<ProductResponse>>;
}
