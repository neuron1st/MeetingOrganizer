using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Services.Photos;

public static class Bootstrapper
{
    public static IServiceCollection AddPhotoService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPhotoService, PhotoService>();
    }
}
