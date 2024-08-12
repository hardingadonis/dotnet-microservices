using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetProductByNameQuery
    (
        string Name
    ) : IRequest<IEnumerable<ProductResponse>>;
}
