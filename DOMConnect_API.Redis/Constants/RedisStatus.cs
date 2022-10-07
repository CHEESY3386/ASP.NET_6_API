namespace DOMConnect_API.Redis.Constants
{
    /// <summary>
    /// Defines the different status responses of redis when executing commands like "Set".
    /// </summary>
    public static class RedisStatus
    {
        public const string Ok = "OK";
        public const string Null = "(nil)";
    }
}
