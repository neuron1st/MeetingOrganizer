using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.Actions;

/// <summary>
/// Contains extension methods for configuring services related to actions.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the action services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddActions(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAction, Action>();
    }
}
