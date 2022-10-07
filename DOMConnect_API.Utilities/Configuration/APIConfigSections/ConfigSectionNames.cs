namespace DOMConnect_API.Utilities.Configuration.APIConfigSections
{
    public static class ConfigSectionNames
    {
        public const string Logging = "Logging";

        public const string DomboxConfigPath = "ConfigPath";

        //API Secret
        public const string Tokens = "Tokens";

        // MySQL
        public const string MySql = "MySql";
        public const string MySqlServer = "Server";
        public const string MySqlDatabse = "Databse";
        public const string MySqlUser = "User";
        public const string MySqlPassword = "Password";

        // Redis
        public const string Redis = "Redis";
        public const string RedisHost = "Host";
        public const string RedisPort = "Port";

        // Serilog
        public const string Serilog = "Serilog";
        public const string SerilogLevelSwitches = "LevelSwitches";
        public const string SerilogLevelSwitchConsole = "consoleSwitch";
        public const string SerilogLevelSwitchFile = "fileSwitch";
        public const string SerilogUsing = "Using";
        public const string SerilogMinimumLevel = "MinimumLevel";
        public const string SerilogWriteLocation = "WriteTo";
        public const string SerilogWriteLocationName = "Name";
        public const string SerilogWriteLocationArgs = "Args";
        public const string SerilogWriteLocationArgsLevelSwitch = "levelSwitch";
        public const string SerilogWriteLocationArgsPath = "path";

    }
}
