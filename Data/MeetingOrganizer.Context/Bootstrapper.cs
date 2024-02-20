using MeetingOrganizer.Context.Context;
using MeetingOrganizer.Context.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Context;

public static class Bootstrapper
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var settings = MeetingOrganizer.Settings.Settings.Load<Settings.DbSettings>("Database", configuration);
        services.AddSingleton(settings);

        var dbInitOptionsDelegate = DbContextOptionsFactory.Configure(settings.ConnectionString);

        services.AddDbContextFactory<MeetingOrganizerDbContext>(dbInitOptionsDelegate);

        return services;
    }
}
