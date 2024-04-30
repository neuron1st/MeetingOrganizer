using MeetingOrganizer.Common.Helpers;

namespace MeetingOrganizer.Api.Configuration;

/// <summary>
/// Configuration class for AutoMapper.
/// </summary>
public static class AutoMapperConfiguration
{
    /// <summary>
    /// Configures AutoMapper mappings.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddAppAutoMappers(this IServiceCollection services)
    {
        AutoMappersRegisterHelper.Register(services);

        return services;
    }
}
