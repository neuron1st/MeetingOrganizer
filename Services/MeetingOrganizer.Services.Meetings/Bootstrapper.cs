using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.Meetings;

public static class Bootstrapper
{
    public static IServiceCollection AddMeetingService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMeetingService, MeetingService>();
    }
}
