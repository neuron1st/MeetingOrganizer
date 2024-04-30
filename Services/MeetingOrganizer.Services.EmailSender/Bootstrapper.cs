using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.EmailSender;

/// <summary>
/// Contains extension methods to register the email sender service.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Registers the email sender service in the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddEmailSender(this IServiceCollection services)
    {
        return services
            .AddSingleton<IEmailSender, EmailSender>();
    }
}
