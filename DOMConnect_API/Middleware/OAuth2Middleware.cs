using DOMConnect_API.Services.Token;
using DOMConnect_API.IO.ApiResponses;
using DOMConnect_API.IO.ApiResponses.Constants;

using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;


namespace DOMConnect_API.Middleware
{
    /// <summary>
    /// Extension methods for <see cref="OAuth2Middleware"/>.
    /// </summary>
    public static class OAuth2MiddlewareExtensions
    {
        /// <summary>
        /// Method used for excluding this middleware from certain routes (see Program.cs)
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseOAuth2Middleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<OAuth2Middleware>();

            return app;
        }
    }

    /// <summary>
    /// Middleware class for oauth2 authentication.
    /// </summary>
    public sealed class OAuth2Middleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Creates instance of <see cref="OAuth2Middleware"/>.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/>.</param>
        public OAuth2Middleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// The method called when invoking this middleware.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="tokenService">The <see cref="ITokenService"/>.</param>
        /// <param name="userService"> The <see cref="IUserService"/></param>
        public async Task Invoke(
            HttpContext context,
            TokenService tokenService)
        {
            string authHeader = context.Request.Headers[HeaderNames.Authorization];
            string accessToken = authHeader?.Replace("Bearer ", "");
            var deserializedToken = tokenService.TryDecodeAccessToken(accessToken, out var ex);

            if (ex != null)
            {
                Error(context, ErrorKey.INVALID_ACCESS_TOKEN_ERROR, ex.Message);
                return;
            }

            await _next(context);
        }

        private async void Error(HttpContext context, ErrorKey errorKey, string details)
        {
            var response = new ApiResponse(errorKey, details);

            context.Response.StatusCode = response.Status;
            await context.Response.WriteAsJsonAsync(new JsonResult(response).Value);
        }

        private async void Error(HttpContext context, ErrorKey errorKey)
        {
            var response = new ApiResponse(errorKey);

            context.Response.StatusCode = response.Status;
            await context.Response.WriteAsJsonAsync(new JsonResult(response).Value);
        }
    }
}
