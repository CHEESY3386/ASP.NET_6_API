using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace DOMConnect_API.Utilities.Extensions
{
    public class WritableJsonConfigurationSource : JsonConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            this.EnsureDefaults(builder);
            return (IConfigurationProvider)new WritableJsonConfigurationProvider(this);
        }
    }
}
