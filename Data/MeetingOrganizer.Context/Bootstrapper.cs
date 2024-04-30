using MeetingOrganizer.Context.Context;
using MeetingOrganizer.Context.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Context;

/// <summary>
/// Class for bootstrapping the application's database context.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the application's database context to the services collection.
    /// </summary>
    /// <param name="services">The service collection to add the context to.</param>
    /// <param name="configuration">The optional configuration to use for loading settings.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var settings = MeetingOrganizer.Settings.Settings.Load<Settings.DbSettings>("Database", configuration);
        services.AddSingleton(settings);

        var dbInitOptionsDelegate = DbContextOptionsFactory.Configure(settings.ConnectionString);

        services.AddDbContextFactory<MeetingOrganizerDbContext>(dbInitOptionsDelegate);

        return services;
    }
}
