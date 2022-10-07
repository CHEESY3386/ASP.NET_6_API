using Microsoft.AspNetCore.Mvc.Filters;

namespace DOMConnect_API.Attributes
{
    /// <summary>
    /// Attribute used for managing user authorization to certain routes
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Creates instance of <see cref="AuthorizeAttribute"/>.
        /// </summary>
        /// <param name="requiredPermissions">List of required permissions for a given route as parameters.</param>
        public AuthorizeAttribute()
        {
        }
        /// <summary>
        /// Called early in the filter pipeline to confirm request is authorized.
        /// </summary>
        /// <param name="context">The <see cref="Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext"/>.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }
    }
}