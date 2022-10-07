using DOMConnect_API.IO.DTO;
using DOMConnect_API.IO.DTO.Constants;
using DOMConnect_API.Services.Config.Entities;

namespace DOMConnect_API.Mapping.Config
{
    /// <summary>
    /// Extension methods for object mapping
    /// </summary>
    public static class LogConfigEntityMapper
    {
        /// <summary>
        /// Converts a <see cref="LogConfigEntity"/> to a <see cref="LogConfig"/>.
        /// </summary>
        /// <remarks>
        /// Cretain properties can be set to <see cref="default"/> if there is a parsing error.
        /// </remarks>
        /// <param name="logConfigEntity">The <see cref="LogConfigEntity"/>.</param>
        /// <returns>
        /// A <see cref="LogConfig"/> on success. <br/>
        /// Otherwise <see langword="null"/>.
        /// </returns>
        public static LogConfig ToDTO(this LogConfigEntity logConfigEntity)
        {
            if (logConfigEntity == null)
                return null;

            return new LogConfig
            {
                ApiLogs = Enum.TryParse(typeof(ApiLogLevel), logConfigEntity.ApiLogs, out object apiLogLevel) ? (ApiLogLevel?)apiLogLevel : default
            };
        }

        /// <summary>
        /// Converts a <see cref="LogConfig"/> to a <see cref="LogConfigEntity"/>.
        /// </summary>
        /// <remarks>
        /// Cretain properties can be set to <see cref="default"/> if there is a parsing error.
        /// </remarks>
        /// <param name="logConfig">The <see cref="LogConfig"/>.</param>
        /// <returns>
        /// A <see cref="LogConfigEntity"/> on success. <br/>
        /// Otherwise <see langword="null"/>.
        /// </returns>
        public static LogConfigEntity ToEntity(this LogConfig logConfig)
        {
            if (logConfig == null)
                return null;

            return new LogConfigEntity
            {
                ApiLogs = logConfig.ApiLogs != null ? logConfig.ApiLogs.ToString() : default
            };
        }
    }
}
