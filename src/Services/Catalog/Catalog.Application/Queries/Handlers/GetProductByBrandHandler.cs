using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Handlers
{
    public class GetProductByBrandHandler : IRequestHandler<GetProductByBrandQuery, IEnumerable<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByBrandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<IEnumerable<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetByBrand(request.BrandName);

            if (productList.Any())
            {
                return CatalogMapper.Mapper.Map<IEnumerable<ProductResponse>>(productList);
            }

            throw new ProductNotFoundException($"brand name: {request.BrandName}");
        }
    }
}
