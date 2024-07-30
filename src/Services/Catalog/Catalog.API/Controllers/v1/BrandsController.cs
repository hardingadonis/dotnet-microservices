namespace Catalog.API.Controllers.v1
{
    public class BrandsController : BaseController<BrandsController>
    {
        public BrandsController(IMediator mediator, ILogger<BrandsController> logger)
            : base(mediator, logger)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BrandResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllBrandsQuery();

            Logger.LogInformation($"Executing: {nameof(GetAllBrandsQuery)}");

            return await ExecuteAsync<GetAllBrandsQuery, IEnumerable<BrandResponse>>(query);
        }
    }
}
