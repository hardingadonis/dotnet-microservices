﻿using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Domain.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetProductsQuery(catalogSpecParams);

            return await ExecuteAsync<GetProductsQuery, Pagination<ProductResponse>>(query);
        }

        [HttpGet]
        [Route("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetProductByIdQuery(id);

            return await ExecuteAsync<GetProductByIdQuery, ProductResponse>(query);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var query = new GetProductByNameQuery(name);

            return await ExecuteAsync<GetProductByNameQuery, IEnumerable<ProductResponse>>(query);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            return await ExecuteAsync<CreateProductCommand, ProductResponse>(command);
        }

        [HttpPut]
        [Route("{id:length(24)}")]
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
        public async Task<IActionResult> DeleteById(string id)
        {
            var command = new DeleteProductByIdCommand(id);

            return await ExecuteAsync<DeleteProductByIdCommand, bool>(command);
        }
    }
}
