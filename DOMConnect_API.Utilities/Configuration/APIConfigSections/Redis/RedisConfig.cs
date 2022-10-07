using Microsoft.Extensions.Configuration;

namespace DOMConnect_API.Utilities.Configuration.APIConfigSections.Redis
{
    public class RedisConfig
    {
        public RedisConfig(IConfigurationRoot configuration)
        {
            configuration.Bind(ConfigSectionNames.Redis, this);
        }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
