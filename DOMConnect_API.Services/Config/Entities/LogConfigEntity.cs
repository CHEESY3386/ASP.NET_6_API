namespace DOMConnect_API.Services.Config.Entities
{
    /// <summary>
    /// Entity for applying log levels to diffrent services on the dombox.
    /// </summary>
    public class LogConfigEntity
    {
        /// <summary>
        /// Gets or sets the log level of this API.
        /// </summary>
        public string ApiLogs { get; set; }

        /// <summary>
        /// Gets or sets the log level of the DOM network manager.
        /// </summary>
        public ushort? NetManagerLogs { get; set; }
    }
}
