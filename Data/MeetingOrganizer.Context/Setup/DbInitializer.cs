using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Context.Setup;

/// <summary>
/// Class for initializing the database.
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Executes the database initialization.
    /// </summary>
    /// <param name="serviceProvider">The service provider used to access required services.</param>
    public static void Execute(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
        ArgumentNullException.ThrowIfNull(scope);

        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MeetingOrganizerDbContext>>();
        using var context = dbContextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}
