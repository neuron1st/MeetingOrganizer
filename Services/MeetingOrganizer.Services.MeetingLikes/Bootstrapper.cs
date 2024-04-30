using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.MeetingLikes;

/// <summary>
/// Class for configuring services related to meeting likes.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the meeting like service to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddMeetingLikeService(this IServiceCollection services)
    {
        return services.AddSingleton<IMeetingLikeService, MeetingLikeService>();
    }
}
