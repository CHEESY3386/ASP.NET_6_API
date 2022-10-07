using System.Text.Json;
using System.Text.Json.Serialization;

namespace DOMConnect_API.IO.JSONConverters
{
    internal class DateTimeUTCConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            return new DateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-ddThh:mm:ssZ"));
        }
    }
}
