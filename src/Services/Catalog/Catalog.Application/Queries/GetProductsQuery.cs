using Catalog.Application.Responses;
using Catalog.Domain.Specs;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductsQuery(CatalogSpecParams catalogSpecParams) : IRequest<Pagination<ProductResponse>>
    {
        public CatalogSpecParams CatalogSpecParams { get; set; } = catalogSpecParams;
    }
}
