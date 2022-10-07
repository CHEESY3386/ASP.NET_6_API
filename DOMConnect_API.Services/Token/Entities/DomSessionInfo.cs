using System.Text.Json.Serialization;

namespace DOMConnect_API.Services.Token.Entities
{
    /// <summary>
    /// Json object for session informations.
    /// </summary>
    public class DomSessionInfo
    {
        /// <summary>
        /// The Jwt access token.
        /// </summary>
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        /// <summary>
        /// The refresh token.
        /// </summary>
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// The expiration time of the refresh token.
        /// </summary>
        [JsonPropertyName("refreshTokenExp")]
        public DateTime RefreshTokenExp { get; set; }
    }
}