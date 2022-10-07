namespace DOMConnect_API.IO.DTO
{
    /// <summary>
    /// Data transfert object used for sending a access token refresh request.
    /// </summary>
    public class Refresh
    {
        /// <summary>
        /// Gets or sets the login of the user asking for a refresh.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
