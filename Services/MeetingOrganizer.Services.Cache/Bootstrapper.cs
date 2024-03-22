using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.Cache;

public static class Bootstrapper
{
    public static IServiceCollection AddCache(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICacheService, CacheService>();
    }
}
