using DOMConnect_API.IO.ApiResponses.Constants;

namespace DOMConnect_API.IO.ApiResponses
{
    /// <summary>
    /// DOMConnect_API error response object. This object is used to be set 
    /// to <see cref="ApiResponse.Data"/> if an error occures.
    /// </summary>
    public struct ApiErrorResponse
    {
        /// <summary>
        /// Gets and sets the error type.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets and sets the error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets and sets the error details object.
        /// </summary>
        public object Details { get; set; }
    }

    /// <summary>
    /// Outbut object for every response in DOMConnect_API.
    /// </summary>
    /// <remarks>
    /// In order to keep a consistant response body for every api call, DOMConnect_API will and must
    /// always send back a <see cref="ApiResponse"/> object.<br/>This allows a much easier parsing of
    /// the response body and a user can always expect three main elements in the body :<br/><br/>
    /// 
    /// - success : A <see cref="bool"/> indicating if the request was successfull. <br/>
    /// - status : The HTTP status code. <br/>
    /// - data : The requested data or error if one occures. (Can be <see langword="null"/> if there is no extra information to send back) <br/><br/>
    /// 
    /// If an error occures, "success" will be set to <see cref="false"/>, "status" will have the approriate HTTP code and 
    /// the data will always contain a <see cref="ApiErrorResponse"/> with three elements :<br/><br/>
    /// 
    /// - error : The type of error. <br/>
    /// - message : The message of the error. <br/>
    /// - details : Details of the error like the object that provoked it or the stacktrace of an excpetion.
    /// </remarks>
    public class ApiResponse
    {
        /// <summary>
        /// Gets and privately sets the api return data.
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// Gets and privately sets the success indicator.
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets and privately sets the HTTP status code.
        /// </summary>
        public int Status { get; private set; }

        /// <summary>
        /// Creates instance of <see cref="ApiResponse"/> for a given <see cref="ErrorKey"/>
        /// </summary>
        /// <remarks>
        /// This constructor sets <see cref="ApiResponse.Data"/> to an <see cref="ApiErrorResponse"/> with
        /// property values depending on the <see cref="ErrorKey"/>.
        /// </remarks>
        /// <param name="key">The <see cref="ErrorKey"/>.</param>
        public ApiResponse(ErrorKey key)
        {
            Data = new ApiErrorResponse
            {
                Error = ApiErrorMessages.ErrorInfos[key].Error,
                Message = ApiErrorMessages.ErrorInfos[key].ErrorMessage
            };
            Success = false;
            Status = ApiErrorMessages.ErrorInfos[key].Status;
        }

        /// <summary>
        /// Creates instance of <see cref="ApiResponse"/> for a given <see cref="ErrorKey"/> and <see cref="object"/>
        /// </summary>
        /// <remarks>
        /// This constructor sets <see cref="ApiResponse.Data"/> to an <see cref="ApiErrorResponse"/> with
        /// property values depending on the <see cref="ErrorKey"/>.
        /// </remarks>
        /// <param name="key">The <see cref="ErrorKey"/>.</param>
        /// <param name="details">The object to be set to <see cref="ApiErrorResponse.Details"/>.</param>
        public ApiResponse(ErrorKey key, object details)
        {
            Data = new ApiErrorResponse
            {
                Error = ApiErrorMessages.ErrorInfos[key].Error,
                Message = ApiErrorMessages.ErrorInfos[key].ErrorMessage,
                Details = details
            };
            Success = false;
            Status = ApiErrorMessages.ErrorInfos[key].Status;
        }

        /// <summary>
        /// Creates instance of <see cref="ApiResponse"/> for a given <see cref="SuccessKey"/>
        /// </summary>
        /// <param name="key">The <see cref="SuccessKey"/>.</param>
        public ApiResponse(SuccessKey key)
        {
            Success = true;
            Status = ApiSuccessMessages.SuccessCodes[key];
        }

        /// <summary>
        /// Creates instance of <see cref="ApiResponse"/> for a given <see cref="SuccessKey"/> and <see cref="object"/>
        /// </summary>
        /// <param name="key">The <see cref="SuccessKey"/>.</param>
        /// <param name="data">The return data.</param>
        public ApiResponse(SuccessKey key, object data)
        {
            Data = data;
            Success = true;
            Status = ApiSuccessMessages.SuccessCodes[key];
        }
    }
}
