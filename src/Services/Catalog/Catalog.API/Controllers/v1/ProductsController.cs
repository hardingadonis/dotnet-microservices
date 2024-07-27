using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Domain.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers.v1
{
    public class ProductsController : BaseController
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<Pagination<ProductResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetProductsQuery(catalogSpecParams);

            return await ExecuteAsync<GetProductsQuery, Pagination<ProductResponse>>(query);
        }

        [HttpGet]
        [Route("{id:length(24)}")]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetProductByIdQuery(id);

            return await ExecuteAsync<GetProductByIdQuery, ProductResponse>(query);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var query = new GetProductByNameQuery(name);

            return await ExecuteAsync<GetProductByNameQuery, IEnumerable<ProductResponse>>(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            return await ExecuteAsync<CreateProductCommand, ProductResponse>(command, HttpStatusCode.Created);
        }

        [HttpPut]
        [Route("{id:length(24)}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command, string id)
        {
            if (command.Id != id)
            {
                var errorResponse = new ApiResponse<ProductResponse>
                {
                    IsSuccess = false,
                    Message = "The provided Id does not match the resource Id.",
                    Details = "The Id provided in the request body does not match the Id specified in the URL."
                };

                return BadRequest(errorResponse);
            }

            return await ExecuteAsync<UpdateProductCommand, bool>(command);
        }

        [HttpDelete]
        [Route("{id:length(24)}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteById(string id)
        {
            var command = new DeleteProductByIdCommand(id);

            return await ExecuteAsync<DeleteProductByIdCommand, bool>(command);
        }
    }
}
