using Microsoft.Extensions.Configuration;

namespace DOMConnect_API.Utilities.Configuration.APIConfigSections.Tokens
{
    public class TokensConfig
    {
        public TokensConfig(IConfigurationRoot configuration)
        {
            configuration.Bind(ConfigSectionNames.Tokens, this);
        }
        public string AccessTokenKey { get; set; }
        public string Algorithm { get; set; }
        public long AccessTokenExpTime { get; set; }
        public long RefreshTokenExpTime { get; set; }
    }
}
