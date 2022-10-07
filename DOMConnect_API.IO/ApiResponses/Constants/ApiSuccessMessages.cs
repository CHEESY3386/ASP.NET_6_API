using System.Net;

namespace DOMConnect_API.IO.ApiResponses.Constants
{
    /// <summary>
    /// Enumerator used as key for retrieving values of a DOMConnect_API success response.
    /// </summary>
    public enum SuccessKey
    {
        OK,
        CREATED,
        ACCEPTED,
        NO_CONTENT
    }

    internal static class ApiSuccessMessages
    {
        internal static readonly Dictionary<SuccessKey, int> SuccessCodes = new()
        {
            { SuccessKey.OK, (int)HttpStatusCode.OK },
            { SuccessKey.CREATED, (int)HttpStatusCode.Created },
            { SuccessKey.ACCEPTED, (int)HttpStatusCode.Accepted },
            { SuccessKey.NO_CONTENT, (int)HttpStatusCode.NoContent }
        };
    }
}
