using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;


namespace DOMConnect_API.Utilities.Extensions
{
    public class WritableJsonConfigurationProvider : JsonConfigurationProvider
    {
        public WritableJsonConfigurationProvider(JsonConfigurationSource source) : base(source)
        {
        }

        public override void Set(string keys, string value)
        {
            base.Set(keys, value);

            var fileFullPath = base.Source.FileProvider.GetFileInfo(base.Source.Path).PhysicalPath;
            string json = File.ReadAllText(fileFullPath);
            List<string> splitedKeys = keys.Split(":").ToList();
            dynamic jsonObj = JsonConvert.DeserializeObject(json);

            SetStringToDynamicObject(ref jsonObj, splitedKeys, value);

            File.WriteAllText(fileFullPath, JsonConvert.SerializeObject(jsonObj, Formatting.Indented));
            return;

            void SetStringToDynamicObject(ref dynamic obj, List<string> keys, string value)
            {
                if (keys.Count == 1)
                {
                    obj[keys.First()] = value;
                }
                else if (keys.Count > 1)
                {
                    if (obj[keys.First()] != null)
                    {
                        var newobj = obj[keys.First()];
                        keys.RemoveAt(0);
                        SetStringToDynamicObject(ref newobj, keys, value);
                    }
                }
            }
        }
    }
}
