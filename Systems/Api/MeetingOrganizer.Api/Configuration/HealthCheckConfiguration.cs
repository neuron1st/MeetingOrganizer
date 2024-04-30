using MeetingOrganizer.Api.Configuration.HealthChecks;
using MeetingOrganizer.Common.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace MeetingOrganizer.Api.Configuration;

/// <summary>
/// Configuration class for health checks.
/// </summary>
public static class HealthCheckConfiguration
{
    /// <summary>
    /// Configures application health checks.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<SelfHealthCheck>("MeetingOrganizer.API");

        return services;
    }

    /// <summary>
    /// Configures health check middleware.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance.</param>
    public static void UseAppHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health");

        app.MapHealthChecks("health/detail", new HealthCheckOptions
        {
            ResponseWriter = HealthCheckHelper.WriteHealthCheckResponse,
            AllowCachingResponses = false,
        });

    }
}
