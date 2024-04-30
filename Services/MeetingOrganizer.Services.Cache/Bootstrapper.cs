using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.Cache;

/// <summary>
/// Contains extension methods for configuring services related to caching.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the cache services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddCache(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICacheService, CacheService>();
    }
}
