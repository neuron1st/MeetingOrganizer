using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.MeetingLikes;

public static class Bootstrapper
{
    public static IServiceCollection AddMeetingLikeService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMeetingLikeService, MeetingLikeService>();
    }
}