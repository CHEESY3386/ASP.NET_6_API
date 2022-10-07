using DOMConnect_API.Utilities.Configuration.APIConfigSections;
using DOMConnect_API.Utilities.Configuration.APIConfigSections.Serilog;
using DOMConnect_API.Utilities.Configuration;
using DOMConnect_API.Services.Config.Entities;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DOMConnect_API.Utilities.Configuration.APIConfigSections.Tokens;

namespace DOMConnect_API.Services.Config
{
    /// <summary>
    /// API configuration service.
    /// </summary>
    public class ConfigurationService
    {
        private readonly IConfigurationRoot _configuration;
        private readonly ILogger<ConfigurationService> _logger;
        private readonly SerilogConfig _serilogConfig;
        private readonly TokensConfig _tokensConfig;

        /// <summary>
        /// Creates instance of <see cref="ConfigurationService"/>.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="logger">The logger.</param>
        public ConfigurationService(
            IConfigurationRoot configuration,
            ILogger<ConfigurationService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _serilogConfig = new SerilogConfig(_configuration);
            _tokensConfig = new TokensConfig(_configuration);
        }

        /// <summary>
        /// Gets log configurations for each service on the dombox. 
        /// </summary>
        /// <returns>
        /// A <see cref="LogConfigEntity"/>.
        /// </returns>
        public async Task<LogConfigEntity> GetLogConfigsAsync()
        {
            var seriLogConfig = new SerilogConfig(_configuration);

            return new LogConfigEntity
            {
                ApiLogs = seriLogConfig.LevelSwitches.FileSwitch
            };
        }

        /// <summary>
        /// Gets the access token signing key from the API configuration.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> containing the secret key.
        /// </returns>
        public string GetAccessTokenKey()
        {
            return _tokensConfig.AccessTokenKey;
        }

        /// <summary>
        /// Gets the signing algorithm name from the API configuration.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> containing the name of the signing algorithm.
        /// </returns>
        public string GetTokenSignatureAlgorithm()
        {
            return _tokensConfig.Algorithm;
        }

        /// <summary>
        /// Gets the access token expiration time from the API configuration.
        /// </summary>
        /// <returns>
        /// The expiration <see cref="TimeSpan"/> of the access token.
        /// </returns>
        public TimeSpan GetAccessTokenExirationTime()
        {
            long? ticks = _tokensConfig.AccessTokenExpTime;

            return TimeSpan.FromSeconds((long)ticks);
        }

        /// <summary>
        /// Gets the refresh token expiration time from the API configuration.
        /// </summary>
        /// <returns>
        /// The expiration <see cref="TimeSpan"/> of the refresh token.
        /// </returns>
        public TimeSpan GetRefreshTokenExirationTime()
        {
            long? ticks = _tokensConfig.RefreshTokenExpTime;

            return TimeSpan.FromSeconds((long)ticks);
        }

        /// <summary>
        /// Sets the log configuration depending on the given <see cref="LogConfigEntity"/>.
        /// </summary>
        /// <param name="logConfig">The <see cref="LogConfigEntity"/>.</param>
        public void SetLogConfigs(LogConfigEntity logConfig)
        {
            if (logConfig.ApiLogs != null)
            {
                _configuration.GetSection(ConfigSectionNames.Serilog)
                    .GetSection(ConfigSectionNames.SerilogLevelSwitches)
                    .GetSection(ConfigSectionNames.SerilogLevelSwitchFile).Value = logConfig.ApiLogs;
                _configuration.GetSection(ConfigSectionNames.Serilog)
                    .GetSection(ConfigSectionNames.SerilogLevelSwitches)
                    .GetSection(ConfigSectionNames.SerilogLevelSwitchConsole).Value = logConfig.ApiLogs;
            }
        }
    }
}
