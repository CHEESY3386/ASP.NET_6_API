namespace DOMConnect_API.Services.Token.Constants
{
    /// <summary>
    /// Contains the key names used in the payload of the DOM JWT.
    /// </summary>
    public static class DomTokenPayloadKeys
    {
        public const string Username = "name";
        public const string Permissions = "permissions";
        public const string IsActive = "active";
    }
}
