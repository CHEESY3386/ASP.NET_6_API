using System.Text.Json.Serialization;

namespace DOMConnect_API.Services.Token.Entities
{
    /// <summary>
    /// Json object for a JWT payload. Used for parsing DOM JWTs
    /// </summary>
    public class DomAccessToken
    {
        /// <summary>
        /// Username stored in token.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// List of the users permissions as a list of integers.
        /// </summary>
        [JsonPropertyName("permissions")]
        public IEnumerable<int> Permissions { get; set; }

        /// <summary>
        /// Indicator for the activness of the user.
        /// </summary>
        [JsonPropertyName("active")]
        public bool Active { get; set; }

        /// <summary>
        /// Issue time as Unix time in seconds.
        /// </summary>
        [JsonPropertyName("iat")]
        public long Iat { get; set; }

        /// <summary>
        /// Expiration time as Unix time in seconds.
        /// </summary>
        [JsonPropertyName("exp")]
        public long Exp { get; set; }
    }
}
