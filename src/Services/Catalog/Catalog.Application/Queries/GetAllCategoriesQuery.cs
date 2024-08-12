using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResponse>>;
}
