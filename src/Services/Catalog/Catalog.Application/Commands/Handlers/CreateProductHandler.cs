using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;

        public CreateProductHandler(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository
            )
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Get category and brand by id
            var categoryTask = _categoryRepository.GetById(request.CategoryId);
            var brandTask = _brandRepository.GetById(request.BrandId);

            // Wait for all tasks to complete
            await Task.WhenAll(categoryTask, brandTask);

            // If category or brand is not found, throw exception
            var category = await categoryTask ?? throw new CategoryNotFoundException(request.CategoryId);
            var brand = await brandTask ?? throw new BrandNotFoundException(request.BrandId);

            // Map request to product
            var product = CatalogMapper.Mapper.Map<Product>(request);
            product.Category = category;
            product.Brand = brand;

            // Create new product
            var newProduct = await _productRepository.Create(product);

            return CatalogMapper.Mapper.Map<ProductResponse>(newProduct);
        }
    }
}
