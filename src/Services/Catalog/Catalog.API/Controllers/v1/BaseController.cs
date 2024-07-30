namespace Catalog.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseController<T> : ControllerBase
        where T : BaseController<T>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BaseController<T>> _logger;

        protected ILogger<BaseController<T>> Logger => _logger;

        protected BaseController(IMediator mediator, ILogger<BaseController<T>> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private BadRequestObjectResult ValidateRequest<TRequest, TResponse>(TRequest request)
            where TRequest : class, IRequest<TResponse>
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse<TResponse>
                {
                    IsSuccess = false,
                    Message = "Request cannot be null."
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<TResponse>
                {
                    IsSuccess = false,
                    Message = "Invalid request."
                });
            }

            return null!;
        }

        protected async Task<IActionResult> ExecuteAsync<TRequest, TResponse>(TRequest request, HttpStatusCode statusCode = HttpStatusCode.OK)
            where TRequest : class, IRequest<TResponse>
        {
            var validationResult = ValidateRequest<TRequest, TResponse>(request);

            if (validationResult != null)
            {
                return validationResult;
            }

            try
            {
                var response = await _mediator.Send(request);

                return StatusCode((int)statusCode, new ApiResponse<TResponse>
                {
                    IsSuccess = true,
                    Data = response,
                    Message = "Request processed successfully."
                });
            }
            catch (Exception ex)
            {
                return HandleError<TResponse>(ex);
            }
        }

        private ObjectResult HandleError<TResponse>(Exception ex)
        {
            // Log the exception if needed
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An error occurred while processing the request";

            _logger.LogError(ex, message);

            if (ex is CatalogException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            var errorResponse = new ApiResponse<TResponse>
            {
                IsSuccess = false,
                Message = message,
                Details = ex.Message
            };

            return StatusCode((int)statusCode, errorResponse);
        }
    }
}