using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAll();

            return CatalogMapper.Mapper.Map<IEnumerable<ProductResponse>>(productList);
        }
    }
}
