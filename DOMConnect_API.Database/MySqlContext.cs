using DOMConnect_API.Utilities.Configuration.APIConfigSections.MySql;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DOMConnect_API.Database
{
    /// <summary>
    /// Mysql database context
    /// </summary>
    public class MySqlContext : DbContext
    {
        private readonly MySqlConfig _mySqlConfig;

        /// <summary>
        /// Creates instance of <see cref="MySqlContext"/>.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions"/>.</param>
        /// <param name="configuration">The application configuration.</param>
        public MySqlContext(
            DbContextOptions<MySqlContext> options,
            IConfigurationRoot configuration) : base(options)
        {
            _mySqlConfig = new MySqlConfig(configuration);
        }


        /// <summary>
        /// Configures database context on instantiation.
        /// </summary>
        /// <param name="optionsBuilder">The <see cref="DbContextOptionsBuilder"/>.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Connection options. If an entry in mysql has a datetime at 0 it can
            // cause a convertion error. This options corrects that error.
            string connectionOptions = "Convert Zero Datetime=true";

            // all SQL request are available in logs under DEBUG level
            optionsBuilder.ConfigureWarnings(configBuilder =>
            {
                configBuilder.Log((RelationalEventId.CommandExecuting, LogLevel.Debug));
                configBuilder.Log((RelationalEventId.CommandExecuted, LogLevel.Debug));
                configBuilder.Throw(RelationalEventId.ConnectionError);
            });

            optionsBuilder.EnableDetailedErrors();

            optionsBuilder.UseMySQL($"server={_mySqlConfig.Server};" +
                $"database={_mySqlConfig.DataBase};" +
                $"user={_mySqlConfig.User};" +
                $"password={_mySqlConfig.Password};" +
                $"{connectionOptions}");

        }

        /// <summary>
        /// Links database models to tables on creation.
        /// </summary>
        /// <param name="modelBuilder">The <see cref="ModelBuilder"/>.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
