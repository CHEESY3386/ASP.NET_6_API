using DOMConnect_API.Attributes;
using DOMConnect_API.IO.ApiResponses.Constants;
using DOMConnect_API.IO.DTO;
using DOMConnect_API.Mapping.Config;
using DOMConnect_API.Services.Config;

using Microsoft.AspNetCore.Mvc;

namespace DOMConnect_API.Controllers.Configs
{
    /// <summary>
    /// Api controller for application configuration management routes
    /// </summary>
    public sealed class ConfigController : BaseController
    {
        private readonly ConfigurationService _configService;

        /// <summary>
        /// Creates instance of <see cref="ConfigController"/>.
        /// </summary>
        /// <param name="configService">The <see cref="ConfigurationService"/>.</param>
        public ConfigController(ConfigurationService configService)
        {
            _configService = configService;
        }

        #region GET

        /// <summary>
        /// Gets the log configuration
        /// </summary>
        /// <returns>
        /// A HTTP status OK with a <see cref="LogConfig"/> object on success. <br/>
        /// Otherwise a HTTP error status with a <see cref="DOMConnect_API.IO.ApiResponses.ApiErrorResponse"/>.
        /// </returns>
        [Authorize]
        [HttpGet("logs")]
        public async Task<ActionResult<LogConfig>> GetLogConfig()
        {
            var logConfigEntity = await _configService.GetLogConfigsAsync();

            if (logConfigEntity == null)
            {
                return Error(ErrorKey.CONFIG_RETRIEVAL_ERROR);
            }

            return Success(SuccessKey.OK, logConfigEntity.ToDTO());
        }

        #endregion GET

        #region PUT

        /// <summary>
        /// Sets log configuration
        /// </summary>
        /// <param name="logConfig">The log configuration object.</param>
        /// <returns>
        /// A HTTP status OK with a <see cref="LogConfig"/> object on success. <br/>
        /// Otherwise a HTTP error status with a <see cref="DOMConnect_API.IO.ApiResponses.ApiErrorResponse"/>.
        /// </returns>
        [Authorize]
        [HttpPut("logs")]
        public async Task<ActionResult<LogConfig>> PutLogConfig(LogConfig logConfig)
        {
            await Task.Run(() => _configService.SetLogConfigs(logConfig.ToEntity()));

            return Success(SuccessKey.OK, logConfig);
        }

        #endregion PUT
    }
}