using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.Participants;

/// <summary>
/// Bootstrapper for configuring services related to participants.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the participant service to the service collection as a singleton.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddParticipantService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IParticipantService, ParticipantService>();
    }
}
