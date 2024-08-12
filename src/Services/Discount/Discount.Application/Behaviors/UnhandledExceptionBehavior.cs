using Discount.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Behaviours
{
    public class UnhandledExceptionBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> _logger;

        public UnhandledExceptionBehavior(ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogError(ex, "Unhandled Exception Occurred with Request Name: {RequestName}, Request: {Request}", requestName, request);

                throw new RequestHandlingException(requestName, $"An error occurred while handling the request: {requestName}", ex);
            }
        }
    }
}