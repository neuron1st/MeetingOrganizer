namespace MeetingOrganizer.Api;

using MeetingOrganizer.Services.Settings;
using MeetingOrganizer.Services.Logger;
using MeetingOrganizer.Services.UserAccount;
using MeetingOrganizer.Context.Seeder;

/// <summary>
/// API services bootstrapper
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Main to register app services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddLogSettings()
            .AddSwaggerSettings()
            .AddIdentitySettings()
            .AddAppLogger()
            .AddDbSeeder()
            //.AddUserAccountService()
            ;

        return services;
    }
}
