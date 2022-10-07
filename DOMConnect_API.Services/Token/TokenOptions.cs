using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DOMConnect_API.Services.Token
{
    /// <summary>
    /// Class for setting options used for Microsoft.IdentityModel.Tokens.<see cref="TokenValidationParameters"/>.
    /// </summary>
    public sealed class TokenOptions
    {
        private readonly TimeSpan _accessTokenExp;
        private readonly string _algorithm;
        private readonly SecurityKey _key;
        private readonly SigningCredentials _signingCredentials;

        /// <summary>
        /// Creates instance of <see cref="TokenOptions"/>.
        /// </summary>
        /// <remarks>
        /// For list of supported algorithms check list under "Symmetric" at <see href="https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/wiki/Supported-Algorithms"/>
        /// </remarks>
        /// <param name="accessTokenExp">The expiration time of the Jwt.</param>
        /// <param name="secret">The secret key used for signing the Jwt.</param>
        /// <param name="algorithm"> The algorithm used for the Jwt signature. </param>
        public TokenOptions(
            TimeSpan accessTokenExp,
            string secret,
            string algorithm)
        {
            _accessTokenExp = accessTokenExp;
            _algorithm = algorithm;
            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            _signingCredentials = new SigningCredentials(_key, _algorithm);
        }

        /// <summary>
        /// Issuer address.
        /// </summary>
        public string Issuer => null;

        /// <summary>
        /// Audience address.
        /// </summary>
        public string Audience => null;

        /// <summary>
        /// The expiration time of the Jwt.
        /// </summary>
        public TimeSpan AccessTokenExp { get { return _accessTokenExp; } }

        /// <summary>
        /// The algorithm used for the Jwt signature.
        /// </summary>
        public string Algorithm { get { return _algorithm; } }

        /// <summary>
        /// The <see cref="Microsoft.IdentityModel.Tokens.SecurityKey"/> object used for signing the Jwt.
        /// </summary>
        public SecurityKey SecurityKey { get { return _key; } }

        /// <summary>
        /// The <see cref="Microsoft.IdentityModel.Tokens.SigningCredentials"/> object used for signing the Jwt.
        /// </summary>
        public SigningCredentials SigningCredentials { get { return _signingCredentials; } }
    }
}
