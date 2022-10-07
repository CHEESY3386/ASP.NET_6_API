using DOMConnect_API.Database;
using DOMConnect_API.IO.ApiResponses;
using DOMConnect_API.IO.ApiResponses.Constants;

using Microsoft.AspNetCore.Mvc;

namespace DOMConnect_API.Controllers
{
    /// <summary>
    /// Base controller class
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public abstract class BaseController : Controller
    // Only add functions as protected or private to this class. Swagger is generated using the public methods of the api controllers so if you don't, swagger will crash on launch.
    {

        /// <summary>
        /// Returns a <see cref="ObjectResult"/> containing an <see cref="ApiResponse"/>.
        /// </summary>
        /// <remarks>
        /// The <see cref="ErrorKey"/> used as key for retrieving values of a DOMConnect_API error response
        /// that are then set to the <see cref="ApiResponse"/>.
        /// </remarks>
        /// <param name="key">The error key.</param>
        /// <returns>
        /// An <see cref="ObjectResult"/> containing an <see cref="ApiResponse"/>. 
        /// </returns>
        protected ObjectResult Error(ErrorKey key)
        {
            var response = new ApiResponse(key);

            return base.StatusCode(response.Status, response);
        }

        /// <summary>
        /// Returns a <see cref="ObjectResult"/> containing an <see cref="ApiResponse"/>.
        /// </summary>
        /// <remarks>
        /// The <see cref="ErrorKey"/> used as key for retrieving values of a DOMConnect_API error response
        /// that are then set to the <see cref="ApiResponse"/>.
        /// </remarks>
        /// <param name="key">The error key.</param>
        /// <param name="o">The object to be set in the <see cref="ApiResponse.Data"/> property.</param>
        /// <returns>
        /// An <see cref="ObjectResult"/> containing an <see cref="ApiResponse"/>.
        /// </returns>
        protected ObjectResult Error(ErrorKey key, object o)
        {
            var response = new ApiResponse(key, o);

            return base.StatusCode(response.Status, response);
        }

        /// <summary>
        /// Returns a <see cref="ObjectResult"/> containing an <see cref="ApiResponse"/>.
        /// </summary>
        /// <remarks>
        /// The <see cref="SuccessKey"/> used as key for retrieving values of a DOMConnect_API success response
        /// that are then set to the <see cref="ApiResponse"/>.
        /// </remarks>
        /// <param name="key">The error key.</param>
        /// <returns>
        /// An <see cref="ObjectResult"/> containing an <see cref="ApiResponse"/>.
        /// </returns>
        protected ObjectResult Success(SuccessKey key)
        {
            var response = new ApiResponse(key);

            return base.StatusCode(response.Status, response);
        }

        /// <summary>
        /// Returns a <see cref="ObjectResult"/> containing an <see cref="ApiResponse"/>.
        /// </summary>
        /// <remarks>
        /// The <see cref="SuccessKey"/> used as key for retrieving values of a DOMConnect_API success response
        /// that are then set to the <see cref="ApiResponse"/>.
        /// </remarks>
        /// <param name="key">The error key.</param>
        /// <param name="o">The object to be set in the <see cref="ApiResponse.Data"/> property.</param>
        /// <returns>
        /// An <see cref="ObjectResult"/> containing an <see cref="ApiResponse"/>.
        /// </returns>
        protected ObjectResult Success(SuccessKey key, object o)
        {
            var response = new ApiResponse(key, o);

            return base.StatusCode(response.Status, response);
        }
    }
}
