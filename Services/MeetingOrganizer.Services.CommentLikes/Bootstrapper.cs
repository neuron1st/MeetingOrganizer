using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.CommentLikes;

/// <summary>
/// Bootstrapper for adding the comment like service to the DI container.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the comment like service to the DI container.
    /// </summary>
    /// <param name="services">The collection of services.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddCommentLikeService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICommentLikeService, CommentLikeService>();
    }
}
