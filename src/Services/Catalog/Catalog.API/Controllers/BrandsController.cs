using Catalog.Application.Queries;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    public class BrandsController : BaseController
    {
        public BrandsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllBrandsQuery();

            return await ExecuteAsync<GetAllBrandsQuery, IEnumerable<BrandResponse>>(query);
        }
    }
}
