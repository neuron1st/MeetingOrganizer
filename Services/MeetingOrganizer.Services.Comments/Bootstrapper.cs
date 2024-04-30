using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.Comments;

/// <summary>
/// Bootstrapper for adding the comment service to the DI container.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the comment service to the DI container.
    /// </summary>
    /// <param name="services">The collection of services.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddCommentService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICommentService, CommentService>();
    }
}
