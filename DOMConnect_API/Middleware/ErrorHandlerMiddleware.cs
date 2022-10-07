using DOMConnect_API.IO;
using DOMConnect_API.IO.ApiResponses;
using DOMConnect_API.IO.ApiResponses.Constants;

using System.Net;
using System.Text.Json;

namespace DOMConnect_API.Middleware
{
    /// <summary>
    /// Middleware class for catching api errors
    /// </summary>
    public sealed class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        /// <summary>
        /// Creates instance of <see cref="ErrorHandlerMiddleware"/>.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/>.</param>
        /// <param name="logger">The logger.</param>
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// The method called when invoking this middleware.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var error = new ApiErrorResponse
                {
                    Error = ex.GetType().ToString(),
                    Message = ex.Message,
                    Details = ex.StackTrace
                };
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = new CamelCaseNamingPolicy()
                };

                var apiResponse = new ApiResponse(ErrorKey.INTERNAL_ERROR, error);
                var result = JsonSerializer.Serialize(apiResponse, options);

                _logger.LogError("{Message}\n{StackTrace}", ex.Message, ex.StackTrace);

                await response.WriteAsync(result);
            }
        }
    }
}
