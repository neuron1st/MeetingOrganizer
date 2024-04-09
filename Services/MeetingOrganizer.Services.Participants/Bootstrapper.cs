using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.Participants;

public static class Bootstrapper
{
    public static IServiceCollection AddParticipantService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IParticipantService, ParticipantService>();
    }
}
