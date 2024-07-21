using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (!request.Id.IsValidHex24Id())
            {
                throw new InvalidIdException(request.Id);
            }

            var product = await _productRepository.GetById(request.Id);

            if (product == null)
            {
                throw new ProductNotFoundException($"id: `{request.Id}`");
            }

            return CatalogMapper.Mapper.Map<ProductResponse>(product);
        }
    }
}
