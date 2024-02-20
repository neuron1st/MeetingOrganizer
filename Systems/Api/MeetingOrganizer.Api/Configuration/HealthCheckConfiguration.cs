using MeetingOrganizer.Api.Configuration.HealthChecks;
using MeetingOrganizer.Common.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace MeetingOrganizer.Api.Configuration;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<SelfHealthCheck>("YourProjectCollection.API");

        return services;
    }

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
