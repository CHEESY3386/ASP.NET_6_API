using System.Net;

namespace DOMConnect_API.IO.ApiResponses.Constants
{
    /// <summary>
    /// Enumerator used as key for retrieving values of a DOMConnect_API error response.
    /// </summary>
    public enum ErrorKey
    {
        // *********** Base Http codes ***********
        BAD_REQUEST,
        UNAUTHORIZED,
        UNPROCESSABLE_ENTITY,
        NOT_IMPLEMENTED,
        INTERNAL_ERROR,

        // *********** Tokens ***********
        INVALID_ACCESS_TOKEN_ERROR,
        REVOKED_ACCESS_TOKEN_ERROR,
        INVALID_REFRESH_TOKEN_ERROR,
        EXPIRED_REFRESH_TOKEN_ERROR,

        // *********** Config ***********

        CONFIG_RETRIEVAL_ERROR,

        // *********** Redis ***********
        REDIS_SET_ERROR
    }

    internal static class ApiErrorMessages
    {
        internal struct ErrorInfo
        {
            public int Status;
            public string Error;
            public string ErrorMessage;
        }

        internal static Dictionary<ErrorKey, ErrorInfo> ErrorInfos = new()
        {
            {
                ErrorKey.BAD_REQUEST, new()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Error = ErrorKey.BAD_REQUEST.ToString(),
                    ErrorMessage = "Bad request received"
                }
            },
            {
                ErrorKey.UNAUTHORIZED, new()
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Error = ErrorKey.UNAUTHORIZED.ToString(),
                    ErrorMessage = "Unauthorized"
                }
            },
            {
                ErrorKey.UNPROCESSABLE_ENTITY, new()
                {
                    Status = (int)HttpStatusCode.UnprocessableEntity,
                    Error = ErrorKey.UNPROCESSABLE_ENTITY.ToString(),
                    ErrorMessage = "Unprocessable entity received"
                }
            },
            {
                ErrorKey.INTERNAL_ERROR, new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Error = ErrorKey.INTERNAL_ERROR.ToString(),
                    ErrorMessage = "Internal server error"
                }
            },
            {
                ErrorKey.NOT_IMPLEMENTED, new()
                {
                    Status = (int)HttpStatusCode.NotImplemented,
                    Error = ErrorKey.NOT_IMPLEMENTED.ToString(),
                    ErrorMessage = "This api service is not yet implemented."
                }
            },
            {
                ErrorKey.REDIS_SET_ERROR, new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Error = ErrorKey.REDIS_SET_ERROR.ToString(),
                    ErrorMessage = "Something went wrong while setting value to a redis key."
                }
            },
            {
                ErrorKey.CONFIG_RETRIEVAL_ERROR,
                new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Error = ErrorKey.CONFIG_RETRIEVAL_ERROR.ToString(),
                    ErrorMessage = "Error while retrieving configurations. Check api error logs for details."
                }
            },
            {
                ErrorKey.INVALID_ACCESS_TOKEN_ERROR,
                new()
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Error = ErrorKey.INVALID_ACCESS_TOKEN_ERROR.ToString(),
                    ErrorMessage = "Invalid access token. Check details."
                }
            },
            {
                ErrorKey.REVOKED_ACCESS_TOKEN_ERROR,
                new()
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Error = ErrorKey.REVOKED_ACCESS_TOKEN_ERROR.ToString(),
                    ErrorMessage = "This access token was revoked. Call POST /oauth2/authorize."
                }
            },
            {
                ErrorKey.INVALID_REFRESH_TOKEN_ERROR,
                new()
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Error = ErrorKey.INVALID_REFRESH_TOKEN_ERROR.ToString(),
                    ErrorMessage = "Invalid refresh token."
                }
            },
            {
                ErrorKey.EXPIRED_REFRESH_TOKEN_ERROR,
                new()
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Error = ErrorKey.EXPIRED_REFRESH_TOKEN_ERROR.ToString(),
                    ErrorMessage = "This refresh token is expired. Call POST /oauth2/authorize."
                }
            }
        };
    }
}
