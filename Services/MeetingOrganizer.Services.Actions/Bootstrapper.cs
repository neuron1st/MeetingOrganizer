using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.Actions;

public static class Bootstrapper
{
    public static IServiceCollection AddActions(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAction, Action>();
    }
}
