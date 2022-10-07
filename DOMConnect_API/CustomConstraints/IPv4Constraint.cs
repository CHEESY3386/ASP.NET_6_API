using DOMConnect_API.IO.ApiResponses.Constants;

using System.Globalization;
using System.Text.RegularExpressions;

namespace DOMConnect_API.CustomConstraints
{
    /// <summary>
    /// Defines a route constraint for DOM device Id's
    /// </summary>
    public sealed class IPv4Constraint : IRouteConstraint
    {
        /// <summary>
        /// Definition of <see cref="IRouteConstraint.Match(HttpContext?, IRouter?, string, RouteValueDictionary, RouteDirection)"/>.
        /// Defined in this case to check the validity of a DOM device Id through the means of a regular expression.
        /// </summary>
        /// <remarks>
        /// The string inserted into the route must respect the following regular expression : ^[0-9a-fA-F]{2}\.[0-9a-fA-F]{8}$
        /// </remarks>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="route">The <see cref="IRouter"/>.</param>
        /// <param name="routeKey">The key the <see cref="RouteValueDictionary"/>.</param>
        /// <param name="values">The <see cref="RouteValueDictionary"/>.</param>
        /// <param name="direction">The <see cref="RouteDirection"/>.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Match(HttpContext context,
            IRouter route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection direction)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (route == null)
                throw new ArgumentNullException(nameof(route));

            if (routeKey == null)
                throw new ArgumentNullException(nameof(routeKey));

            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (values.TryGetValue(routeKey, out object routeValue))
            {
                var parameterValueString = Convert.ToString(routeValue, CultureInfo.InvariantCulture);
                return new Regex(ValidationRegex.Ipv4,
                                RegexOptions.CultureInvariant
                                | RegexOptions.IgnoreCase, TimeSpan.FromSeconds(10)).IsMatch(parameterValueString);
            }

            return false;
        }
    }
}
