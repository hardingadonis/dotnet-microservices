using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Handlers
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandResponse>>
    {
        private readonly IBrandRepository _brandRepository;

        public GetAllBrandsHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
        }

        public async Task<IEnumerable<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _brandRepository.GetAll();

            return CatalogMapper.Mapper.Map<IEnumerable<BrandResponse>>(brandList);
        }
    }
}
