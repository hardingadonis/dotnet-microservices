using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;

        public UpdateProductHandler(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository
            )
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentException(nameof(categoryRepository));
            _brandRepository = brandRepository ?? throw new ArgumentException(nameof(brandRepository));
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.Id.IsValidHex24Id())
            {
                throw new InvalidIdException(request.Id);
            }

            var brandTask = _brandRepository.GetById(request.BrandId);
            var categoryTask = _categoryRepository.GetById(request.CategoryId);
            var productTask = _productRepository.GetById(request.Id);

            await Task.WhenAll(brandTask, categoryTask, productTask);

            _ = await productTask ?? throw new ProductNotFoundException($"id '{request.Id}'");

            var brand = await brandTask ?? throw new BrandNotFoundException(request.BrandId);
            var category = await categoryTask ?? throw new CategoryNotFoundException(request.CategoryId);

            var product = CatalogMapper.Mapper.Map<Product>(request);
            product.Category = category;
            product.Brand = brand;

            return await _productRepository.Update(product);
        }
    }
}
