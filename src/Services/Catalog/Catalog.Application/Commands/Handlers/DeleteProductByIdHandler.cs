using Catalog.Application.Exceptions;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Handlers
{
    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id);

            if (product == null)
            {
                throw new ProductNotFoundException($"id: {request.Id}");
            }

            return await _productRepository.DeleteById(product.Id);
        }
    }
}
