using DOMConnect_API.IO.ApiResponses;
using DOMConnect_API.IO.ApiResponses.Constants;
using DOMConnect_API.IO.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DOMConnect_API
{
    /// <summary>
    /// A child class of <see cref="ObjectResult"/> instantiated with a <see cref="ModelStateDictionary"/> used for showing invalid request bodies errors.
    /// </summary>
    public sealed class ValidationFailedResult : ObjectResult
    {
        /// <summary>
        /// Creates instance of <see cref="ValidationFailedResult"/>.
        /// </summary>
        /// <param name="modelState">The model state to be converted into a <see cref="ApiResponse"/>.</param>
        public ValidationFailedResult(ModelStateDictionary modelState) : base(modelState)
        {
            Value = new ApiResponse(ErrorKey.UNPROCESSABLE_ENTITY, modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList());
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
