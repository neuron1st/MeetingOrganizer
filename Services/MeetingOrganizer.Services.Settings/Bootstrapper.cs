using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.Settings;

public static class Bootstrapper
{
    public static IServiceCollection AddMainSettings(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var settings = MeetingOrganizer.Settings.Settings.Load<MainSettings>("Main", configuration);
        services.AddSingleton(settings);

        return services;
    }

    public static IServiceCollection AddIdentitySettings(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var settings = MeetingOrganizer.Settings.Settings.Load<IdentitySettings>("Identity", configuration);
        services.AddSingleton(settings);

        return services;
    }

    public static IServiceCollection AddLogSettings(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = MeetingOrganizer.Settings.Settings.Load<LogSettings>("Log", configuration);
        services.AddSingleton(settings);

        return services;
    }

    public static IServiceCollection AddSwaggerSettings(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var settings = MeetingOrganizer.Settings.Settings.Load<SwaggerSettings>("Swagger", configuration);
        services.AddSingleton(settings);

        return services;
    }

    public static IServiceCollection AddCacheSettings(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var settings = MeetingOrganizer.Settings.Settings.Load<CacheSettings>("Cache", configuration);
        services.AddSingleton(settings);

        return services;
    }
}
