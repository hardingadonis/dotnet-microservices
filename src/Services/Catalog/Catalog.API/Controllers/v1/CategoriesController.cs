using Catalog.Application.Queries;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.v1
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCategoriesQuery();

            return await ExecuteAsync<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>(query);
        }
    }
}
