using DOMConnect_API.IO.ApiResponses.Constants;
using DOMConnect_API.Services.Config;
using DOMConnect_API.Services.Token;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using DOMConnect_API.IO.DTO;

namespace DOMConnect_API.Controllers.Authentication
{
    /// <summary>
    /// Api controller for oauth2 routes
    /// </summary>
    public sealed class Oauth2Controller : BaseController
    {
        private readonly ConfigurationService _configurationService;
        private readonly TokenService _tokenService;

        /// <summary>
        /// Creates instance of <see cref="Oauth2Controller"/>.
        /// </summary>
        /// <param name="permissionService">The <see cref="PermissionService"/>.</param>
        /// <param name="userService">The <see cref="UserService"/>.</param>
        /// <param name="configurationService">The <see cref="ConfigurationService"/>.</param>
        /// <param name="tokenService">The <see cref="TokenService"/>.</param>
        public Oauth2Controller(
            ConfigurationService configurationService,
            TokenService tokenService)
        {
            _configurationService = configurationService;
            _tokenService = tokenService;
        }

        #region POST

        /// <summary>
        /// Route used for authorizing a user to access routes on this API.
        /// </summary>
        /// <remarks>
        /// To access routes on this API, you must first authorize yourself so that you can get an access token.
        /// Your permissions will be set to that token and you can then use it as a Bearer token under the "Authorization" header for different routes.
        /// The refresh token is used for the /token route to generate a new access token if it is expired or compromised. (To be kept safely)
        /// </remarks>
        /// <param name="authorization">The user's credentials as </param>
        /// <returns>
        /// A HTTP status OK with a <see cref="Tokens"/> object on success. <br/>
        /// Otherwise a HTTP error status with a <see cref="DOMConnect_API.IO.ApiResponses.ApiErrorResponse"/>.
        /// </returns>
        [HttpPost("authorize")]
        public async Task<ActionResult<Tokens>> PostAuthorize([FromHeader] string authorization)
        {
            bool authParsed = AuthenticationHeaderValue.TryParse(authorization, out var authHeader);

            if (authParsed == false)
                return Error(ErrorKey.UNAUTHORIZED);
            
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];

            // get user from db

            // check user

            // get permissions of user

            // create jwt
            var now = DateTime.UtcNow;
            string accessToken = _tokenService.CreateAccessToken("BobTheNewUser", now);
            string refreshToken = _tokenService.GenerateDomRefreshToken();

            // set tokens to redis

            var tokenResponse = new Tokens
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpires = now.Add(_configurationService.GetAccessTokenExirationTime()),
                RefreshTokenExpires = now.Add(_configurationService.GetRefreshTokenExirationTime())
            };

            return Success(SuccessKey.OK, tokenResponse);
        }

        /// <summary>
        /// Route used for regenerating an access token.
        /// </summary>
        /// <param name="req">The <see cref="Refresh"/> object.</param>
        /// <returns>
        /// A HTTP status OK with a <see cref="Tokens"/> object on success. <br/>
        /// Otherwise a HTTP error status with a <see cref="DOMConnect_API.IO.ApiResponses.ApiErrorResponse"/>.
        /// </returns>
        [HttpPost("token")]
        public async Task<ActionResult> PostGenerateToken(Refresh req)
        {
            string user = "BobTheNewUser";

            var now = DateTime.UtcNow;
            string newAccessToken = _tokenService.CreateAccessToken(user, now);

            var tokenResponse = new Tokens
            {
                AccessToken = newAccessToken,
                RefreshToken = null,
                AccessTokenExpires = now + _configurationService.GetAccessTokenExirationTime(),
                RefreshTokenExpires = null
            };
            return Success(SuccessKey.OK, tokenResponse);
        }
        #endregion GET
    }
}
