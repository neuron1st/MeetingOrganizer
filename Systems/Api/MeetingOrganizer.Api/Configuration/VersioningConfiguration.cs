using Asp.Versioning;

namespace MeetingOrganizer.Api.Configuration
{
    /// <summary>
    /// Configuration class for API versioning.
    /// </summary>
    public static class VersioningConfiguration
    {
        /// <summary>
        /// Configures application API versioning.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddAppVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;

                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            })
                .AddMvc()
                .AddApiExplorer(options =>
                {
                    options.SubstituteApiVersionInUrl = true;
                    options.GroupNameFormat = "'v'VVV";
                    options.FormatGroupName = (group, version) => $"{group} - {version}";
                });

            return services;
        }

    }
}
