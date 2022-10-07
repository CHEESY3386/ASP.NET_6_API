using System.Reflection;

using DOMConnect_API.Utilities.Attributes;

namespace DOMConnect_API.Utilities.Extensions
{
    public static class TypeExtensions
    {
        public static PropertyInfo[] GetFilteredProperties(this Type type)
        {
            return type.GetProperties().Where(pi => pi.GetCustomAttributes(typeof(SkipPropertyAttribute), true).Length == 0).ToArray();
        }
    }

}
