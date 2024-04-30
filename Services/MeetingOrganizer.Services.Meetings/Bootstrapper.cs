using MeetingOrganizer.Services.Meetings;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Bootstrapper for configuring services related to meetings.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the meeting service to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddMeetingService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMeetingService, MeetingService>();
    }
}
