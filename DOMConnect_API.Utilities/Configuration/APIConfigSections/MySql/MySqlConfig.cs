using Microsoft.Extensions.Configuration;

namespace DOMConnect_API.Utilities.Configuration.APIConfigSections.MySql
{
    public class MySqlConfig
    {
        public MySqlConfig(IConfigurationRoot configuration)
        {
            configuration.Bind(ConfigSectionNames.MySql, this);
        }
        public string Server { get; set; }

        public string DataBase { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
