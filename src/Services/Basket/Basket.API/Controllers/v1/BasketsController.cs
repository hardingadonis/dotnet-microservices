namespace Basket.API.Controllers.v1
{
    public class BasketsController : BaseController<BasketsController>
    {
        public BasketsController(IMediator mediator, ILogger<BasketsController> logger)
            : base(mediator, logger)
        {
        }

        [HttpGet]
        [Route("user/{userName}")]
        [ProducesResponseType(typeof(ApiResponse<BasketResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            var query = new GetBasketByUserNameQuery(userName);

            Logger.LogInformation($"Executing: {nameof(GetBasketByUserNameQuery)}");
            Logger.LogDebug("Executing: {method} with {userName}", nameof(GetBasketByUserNameQuery), userName);

            return await ExecuteAsync<GetBasketByUserNameQuery, BasketResponse>(query);
        }

        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(ApiResponse<BasketResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateBasket([FromBody] CreateBasketCommand command)
        {
            return await ExecuteAsync<CreateBasketCommand, BasketResponse>(command, HttpStatusCode.Created);
        }

        [HttpDelete]
        [Route("user/{userName}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            var command = new DeleteBasketByUserNameCommand(userName);

            Logger.LogInformation($"Executing: {nameof(DeleteBasketByUserNameCommand)}");
            Logger.LogDebug("Executing: {method} with {userName}", nameof(DeleteBasketByUserNameCommand), userName);

            return await ExecuteAsync<DeleteBasketByUserNameCommand, bool>(command);
        }
    }
}