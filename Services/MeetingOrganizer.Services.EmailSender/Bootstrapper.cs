using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.EmailSender;

public static class Bootstrapper
{
    public static IServiceCollection AddEmailSender(this IServiceCollection services)
    {
        return services
            .AddSingleton<IEmailSender, EmailSender>();
    }
}
