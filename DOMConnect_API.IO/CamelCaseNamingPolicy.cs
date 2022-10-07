using System.Text.Json;

namespace DOMConnect_API.IO
{
    /// <summary>
    /// Used for converting PascalCase strings to camelCase
    /// </summary>
    public class CamelCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) =>
            char.ToLowerInvariant(name[0]) + name[1..];
    }
}
