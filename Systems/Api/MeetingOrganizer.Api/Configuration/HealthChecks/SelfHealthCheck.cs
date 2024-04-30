using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace MeetingOrganizer.Api.Configuration.HealthChecks;

/// <inheritdoc/>
public class SelfHealthCheck : IHealthCheck
{
    /// <inheritdoc/>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.Load("MeetingOrganizer.Api");
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy(description: $"Build {versionNumber}"));
    }
}
