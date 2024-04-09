using MeetingOrganizer.Services.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.Logger;

public static class Bootstrapper
{
    public static IServiceCollection AddAppLogger(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAppLogger, AppLogger>();
    }
}
