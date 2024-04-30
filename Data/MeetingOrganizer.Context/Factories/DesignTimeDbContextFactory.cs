using MeetingOrganizer.Context.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MeetingOrganizer.Context.Factories;

/// <summary>
/// Factory for creating instances of the MeetingOrganizerDbContext for design-time scenarios.
/// </summary>
public class DesignTimeDbContextFactory
{
    private const string databaseProvider = "postgresql";
    private const string migrationsAssembly = "MeetingOrganizer.Context.MigrationsPostgreSQL";

    /// <summary>
    /// Creates a new instance of MeetingOrganizerDbContext for design-time scenarios.
    /// </summary>
    /// <param name="args">Arguments passed to the factory method.</param>
    /// <returns>A new instance of MeetingOrganizerDbContext.</returns>
    public MeetingOrganizerDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.context.json")
            .Build();

        var options = new DbContextOptionsBuilder<MeetingOrganizerDbContext>()
            .UseNpgsql(
                configuration.GetConnectionString(databaseProvider),
                npqsqlOptions => npqsqlOptions.MigrationsAssembly(migrationsAssembly))
            .Options;

        return new MeetingOrganizerDbContext(options);
    }
}
