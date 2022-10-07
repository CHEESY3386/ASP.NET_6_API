using System.Text.Json.Serialization;

namespace DOMConnect_API.IO.DTO.Constants
{
    /// <summary>
    /// Determines the logging level of serilog
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ApiLogLevel
    {
        /// <summary>
        /// Verbose is the noisiest level, rarely (if ever) enabled for a production app.
        /// </summary>
        Verbose = 0,

        /// <summary>
        /// Debug is used for internal system events that are not necessarily observable from the outside,
        /// but useful when determining how something happened.
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Information events describe things happening in the system that correspond 
        /// to its responsibilities and functions. Generally these are the observable actions the system can perform.
        /// </summary>
        Information = 2,

        /// <summary>
        /// When service is degraded, endangered, or may be behaving outside of its expected parameters,
        /// Warning level events are used.
        /// </summary>
        Warning = 3,

        /// <summary>
        /// When functionality is unavailable or expectations broken, an Error event is used.
        /// </summary>
        Error = 4,

        /// <summary>
        /// The most critical level, Fatal events demand immediate attention.
        /// </summary>
        Fatal = 5
    }
}
