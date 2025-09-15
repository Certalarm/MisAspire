using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MisAspire.WebAPI.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private const string __errorOccured = "An error occurred";
        private const string __exceptionOccured = "An unhandled exception occurred";
        private const string __rfc7231reference = "https://tools.ietf.org/html/rfc7231#section-6.6.1";

        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancelToken)
        {
            _logger.LogError(exception, __exceptionOccured);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = __errorOccured,
                Type = __rfc7231reference
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancelToken);

            return true;
        }
    }
}
