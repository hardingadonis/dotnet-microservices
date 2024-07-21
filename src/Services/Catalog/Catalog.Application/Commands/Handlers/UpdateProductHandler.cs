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
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // Get brand, category and product by id
            var brandTask = _brandRepository.GetById(request.BrandId);
            var categoryTask = _categoryRepository.GetById(request.CategoryId);
            var productTask = _productRepository.GetById(request.Id);

            // Wait for all tasks to complete
            await Task.WhenAll(brandTask, categoryTask, productTask);

            // If product is not found, throw exception
            _ = await productTask ?? throw new ProductNotFoundException($"id '{request.Id}'");

            // If brand or category is not found, throw exception
            var brand = await brandTask ?? throw new BrandNotFoundException(request.BrandId);
            var category = await categoryTask ?? throw new CategoryNotFoundException(request.CategoryId);

            // Map request to product
            var product = CatalogMapper.Mapper.Map<Product>(request);
            product.Category = category;
            product.Brand = brand;

            // Update product
            return await _productRepository.Update(product);
        }
    }
}
