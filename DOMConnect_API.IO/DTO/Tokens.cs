using DOMConnect_API.IO.JSONConverters;

using System.Text.Json.Serialization;

namespace DOMConnect_API.IO.DTO
{
    /// <summary>
    /// Data transfert object as response for requests concerning access and refresh tokens.
    /// </summary>
    public class Tokens
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the access token.
        /// </summary>
        [JsonConverter(typeof(DateTimeUTCConverter))]
        public DateTime? AccessTokenExpires { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the refresh token.
        /// </summary>
        [JsonConverter(typeof(DateTimeUTCConverter))]
        public DateTime? RefreshTokenExpires { get; set; }
    }
}
