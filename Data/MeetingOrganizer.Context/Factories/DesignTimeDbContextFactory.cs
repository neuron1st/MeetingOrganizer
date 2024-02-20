using MeetingOrganizer.Context.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MeetingOrganizer.Context.Factories;

public class DesignTimeDbContextFactory
{
    private const string databaseProvider = "postgresql";
    private const string migrationsAssembly = "MeetingOrganizer.Context.MigrationsPostgreSQL";

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

        return new DbContextFactory(options).Create();
    }
}
