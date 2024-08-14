namespace Ordering.API.Controllers.v1
{
    public class OrdersController : BaseController<OrdersController>
    {
        public OrdersController(IMediator mediator, ILogger<BaseController<OrdersController>> logger)
            : base(mediator, logger)
        {
        }

        [HttpGet]
        [Route("user/{userName}")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<OrderResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrdersByUserName(string userName)
        {
            var query = new GetOrdersByUserQuery(userName);

            Logger.LogInformation($"Executing: {nameof(GetOrdersByUserQuery)}");
            Logger.LogDebug("Executing: {method} with {userName}", nameof(GetOrdersByUserQuery), userName);

            return await ExecuteAsync<GetOrdersByUserQuery, IEnumerable<OrderResponse>>(query);
        }

        [HttpPost]
        [Route("checkout")]
        [ProducesResponseType(typeof(ApiResponse<long>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            Logger.LogInformation($"Executing: {nameof(CheckoutOrderCommand)}");

            return await ExecuteAsync<CheckoutOrderCommand, long>(command, HttpStatusCode.Created);
        }

        [HttpPut]
        [Route("{id:long}")]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command, long id)
        {
            Logger.LogInformation($"Executing: {nameof(UpdateOrderCommand)}");
            Logger.LogDebug("Executing: {method} with {id}", nameof(UpdateOrderCommand), id);

            if (command.Id != id)
            {
                var errorResponse = new ApiResponse<OrderResponse>
                {
                    IsSuccess = false,
                    Message = "The provided ID does not match the resource ID.",
                    Details = "The ID provided in the request body does not match the ID specified in the URL."
                };

                return BadRequest(errorResponse);
            }

            return await ExecuteAsync<UpdateOrderCommand, Unit>(command);
        }

        [HttpDelete]
        [Route("{id:long}")]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            var command = new DeleteOrderCommand(id);

            Logger.LogInformation($"Executing: {nameof(DeleteOrderCommand)}");
            Logger.LogDebug("Executing: {method} with {id}", nameof(DeleteOrderCommand), id);

            return await ExecuteAsync<DeleteOrderCommand, Unit>(command);
        }
    }
}