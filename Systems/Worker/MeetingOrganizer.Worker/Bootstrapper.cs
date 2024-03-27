namespace MeetingOrganizer.Worker;

using MeetingOrganizer.Services.RabbitMq;
using MeetingOrganizer.Services.EmailSender;
using Microsoft.Extensions.DependencyInjection;
using MeetingOrganizer.Services.Settings;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddEmailSenderSettings()
            .AddRabbitMqSettings()
            .AddRabbitMq()
            .AddEmailSender()
            ;

        services.AddSingleton<ITaskExecutor, TaskExecutor>();

        return services;
    }
}
