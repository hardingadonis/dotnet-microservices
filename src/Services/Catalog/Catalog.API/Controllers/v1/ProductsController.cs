﻿namespace Catalog.API.Controllers.v1
{
    public class ProductsController : BaseController<ProductsController>
    {
        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
            : base(mediator, logger)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<Pagination<ProductResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetProductsQuery(catalogSpecParams);

            Logger.LogInformation($"Executing: {nameof(GetProductsQuery)}");

            return await ExecuteAsync<GetProductsQuery, Pagination<ProductResponse>>(query);
        }

        [HttpGet]
        [Route("{id:length(24)}")]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetProductByIdQuery(id);

            Logger.LogInformation($"Executing: {nameof(GetProductByIdQuery)}");
            Logger.LogDebug("Executing: {method} with id = {id}", nameof(GetProductByIdQuery), id);

            return await ExecuteAsync<GetProductByIdQuery, ProductResponse>(query);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var query = new GetProductByNameQuery(name);

            Logger.LogInformation($"Executing: {nameof(GetProductByNameQuery)}");
            Logger.LogDebug("Executing: {method} with name = {name}", nameof(GetProductByNameQuery), name);

            return await ExecuteAsync<GetProductByNameQuery, IEnumerable<ProductResponse>>(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            Logger.LogInformation($"Executing: {nameof(CreateProductCommand)}");

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

                Logger.LogError(errorResponse.Message);

                return BadRequest(errorResponse);
            }

            Logger.LogInformation($"Executing: {nameof(UpdateProductCommand)}");
            Logger.LogDebug("Executing: {method} with id = {id}", nameof(UpdateProductCommand), id);

            return await ExecuteAsync<UpdateProductCommand, bool>(command);
        }

        [HttpDelete]
        [Route("{id:length(24)}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteById(string id)
        {
            var command = new DeleteProductByIdCommand(id);

            Logger.LogInformation($"Executing: {nameof(DeleteProductByIdCommand)}");
            Logger.LogDebug("Executing: {method} with id = {id}", nameof(DeleteProductByIdCommand), id);

            return await ExecuteAsync<DeleteProductByIdCommand, bool>(command);
        }
    }
}
