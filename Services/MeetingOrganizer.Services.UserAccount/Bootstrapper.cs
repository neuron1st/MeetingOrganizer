using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.UserAccount;

/// <summary>
/// Bootstrapper for configuring services related to user accounts.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the user account service to the service collection as a scoped service.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddUserAccountService(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserAccountService, UserAccountService>();
    }
}
