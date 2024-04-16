using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.CommentLikes;

public static class Bootstrapper
{
    public static IServiceCollection AddCommentLikeService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICommentLikeService, CommentLikeService>();
    }
}
