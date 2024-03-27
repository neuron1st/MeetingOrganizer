using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.RabbitMq;

public static class Bootstrapper
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        return services
            .AddSingleton<IRabbitMq, RabbitMq>();
    }
}
