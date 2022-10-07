using DOMConnect_API.Services.Config;
using DOMConnect_API.Redis;
using DOMConnect_API.Services.Token.Entities;

using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;


namespace DOMConnect_API.Services.Token
{
    /// <summary>
    /// Service used for generating, authenticating and parsing JWTs and refresh tokens.
    /// </summary>
    public class TokenService
    {
        private readonly RedisContext _redis;
        private readonly ConfigurationService _configurationService;
        private readonly TokenOptions _options;
        private readonly TokenValidationParameters _tokenValidationParameters;

        /// <summary>
        /// Creates new instance of <see cref="TokenService"/>.
        /// </summary>
        /// <param name="redis">The <see cref="RedisContext"/>.</param>
        /// <param name="configurationService">The <see cref="ConfigurationService"/>.</param>
        public TokenService(RedisContext redis, ConfigurationService configurationService)
        {
            _redis = redis;
            _configurationService = configurationService;

            _options = new TokenOptions(
                _configurationService.GetAccessTokenExirationTime(),
                _configurationService.GetAccessTokenKey(),
                _configurationService.GetTokenSignatureAlgorithm()
                );

            _tokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = _options.SecurityKey,
                ValidAlgorithms = new List<string>() { _options.Algorithm },
                ValidateAudience = false,
                ValidateIssuer = false
            };
        }

        /// <summary>
        /// Returns a JWT with the given parameters in it's payload
        /// </summary>
        /// <param name="username">The user's login name.</param>
        /// <param name="now">The time of creation of this token (placed as parameter to be synchronized with other instances)</param>
        /// <returns>
        /// A JWT as <see cref="string"/> if no errors occure. <br/>
        /// Otherwise <see langword="null"/>.
        /// </returns>
        public string CreateAccessToken(string username, DateTime now)
        {
            var claims = new List<Claim>();

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.AccessTokenExp),
                signingCredentials: _options.SigningCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// Generates a random array of 64 bytes and returns it as base64 encoded string.
        /// </summary>
        /// <returns>A base64 encoded <see cref="string"/> to be used as a refresh token.</returns>
        public string GenerateDomRefreshToken()
        {
            var randomNumber = new byte[64];

            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Attempts to validate and decode a JWT.
        /// </summary>
        /// <param name="accessToken">The JWT.</param>
        /// <param name="ex">The output exception in case of error.</param>
        /// <returns>
        /// A <see cref="DomAccessToken"/> object if validated and parsed correctly. <br/>
        /// Otherwise <see langword="null"/> and <paramref name="ex"/> is set to an instance of <see cref="Exception"/>.
        /// </returns>
        public DomAccessToken TryDecodeAccessToken(string accessToken, out Exception ex)
        {
            try
            {
                ex = null;
                new JwtSecurityTokenHandler().ValidateToken(accessToken, _tokenValidationParameters, out var validatedToken);

                return JsonSerializer.Deserialize<DomAccessToken>(validatedToken.ToString().Split('.')[1]);
            }
            catch (Exception e)
            {
                ex = e;
                return null;
            }
        }
    }
}
