using MeetingOrganizer.Context.Context;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Factories;

/// <summary>
/// Factory class for creating and configuring DbContextOptions for a PostgreSQL database.
/// </summary>
public static class DbContextOptionsFactory
{
    private const string migrationsAssembly = "MeetingOrganizer.Context.MigrationsPostgreSQL";

    /// <summary>
    /// Creates DbContextOptions for configuring a DbContext to use a PostgreSQL database
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns>DbContextOptions for configuring a DbContext</returns>
    public static DbContextOptions<MeetingOrganizerDbContext> Create(string connectionString)
    {
        var builder = new DbContextOptionsBuilder<MeetingOrganizerDbContext>();
        Configure(connectionString).Invoke(builder);
        return builder.Options;
    }

    /// <summary>
    /// Configures the DbContextOptionsBuilder to use a PostgreSQL database
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns>An action to configure the DbContextOptionsBuilder</returns>
    public static Action<DbContextOptionsBuilder> Configure(string connectionString)
    {
        return builder =>
        {
            builder.UseNpgsql(connectionString, options =>
            {
                options
                    .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                    .MigrationsHistoryTable("_EFMigrationsHistory", "public")
                    .MigrationsAssembly(migrationsAssembly);
            });

            builder
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        };
    }
}
