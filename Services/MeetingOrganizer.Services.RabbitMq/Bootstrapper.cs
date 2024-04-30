using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.RabbitMq;

/// <summary>
/// Bootstrapper for configuring RabbitMQ related services.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the RabbitMQ service to the service collection as a singleton.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        return services
            .AddSingleton<IRabbitMq, RabbitMq>();
    }
}
