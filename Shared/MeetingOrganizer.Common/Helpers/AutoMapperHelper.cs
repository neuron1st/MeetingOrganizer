using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Common.Helpers;

public static class AutoMappersRegisterHelper
{
    public static void Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("meetingorganizer."));

        services.AddAutoMapper(assemblies);
    }
}