using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.Comments;

public static class Bootstrapper
{
    public static IServiceCollection AddCommentService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICommentService, CommentService>();
    }
}
