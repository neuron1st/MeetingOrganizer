using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.UserAccount;

public static class Bootstrapper
{
    public static IServiceCollection AddUserAccountService(this IServiceCollection services)
    {
        services.AddScoped<IUserAccountService, UserAccountService>();

        return services;
    }
}
