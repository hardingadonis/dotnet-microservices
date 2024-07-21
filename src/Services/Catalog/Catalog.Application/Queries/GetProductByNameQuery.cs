using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductByNameQuery(string name) : IRequest<IEnumerable<ProductResponse>>
    {
        public string Name { get; set; } = name;
    }
}
