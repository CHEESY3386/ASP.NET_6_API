using DOMConnect_API.IO.DTO.Constants;
using System.ComponentModel.DataAnnotations;

namespace DOMConnect_API.IO.DTO
{
    /// <summary>
    /// Data transfert object for applying log levels to diffrent services on the dombox.
    /// </summary>
    public class LogConfig
    {
        /// <summary>
        /// Gets or sets the log level of this API.
        /// </summary>
        /// <remarks>
        /// This field is required in order to create an instance
        /// of this object.
        /// </remarks>
        [Required]
        public ApiLogLevel? ApiLogs { get; set; }
    }
}