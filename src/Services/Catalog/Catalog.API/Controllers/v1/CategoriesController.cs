namespace Catalog.API.Controllers.v1
{
    public class CategoriesController : BaseController<CategoriesController>
    {
        public CategoriesController(IMediator mediator, ILogger<CategoriesController> logger)
            : base(mediator, logger)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCategoriesQuery();

            Logger.LogInformation($"Executing: {nameof(GetAllCategoriesQuery)}");

            return await ExecuteAsync<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>(query);
        }
    }
}
