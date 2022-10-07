using Microsoft.Extensions.Configuration;

namespace DOMConnect_API.Utilities.Configuration.APIConfigSections.Serilog
{
    public class SerilogConfig
    {
        public SerilogConfig(IConfigurationRoot configuration)
        {
            configuration.Bind(ConfigSectionNames.Serilog, this);
        }

        public LevelSwitchesConfig LevelSwitches { get; set; }

        public List<string> Using { get; set; }

        public string MinimumLevel { get; set; }

        public List<WriteLocationConfig> WriteTo { get; set; }
    }
}
