using MeetingOrganizer.Context.Context;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Factories;

/// <summary>
/// Factory class for creating instances of MeetingOrganizerDbContext
/// </summary>
public class DbContextFactory
{
    private readonly DbContextOptions<MeetingOrganizerDbContext> _options;

    /// <summary>
    /// Initializes a new instance of the DbContextFactory class with the specified DbContextOptions
    /// </summary>
    /// <param name="options"></param>
    public DbContextFactory(DbContextOptions<MeetingOrganizerDbContext> options)
    {
        _options = options;
    }

    /// <summary>
    /// Creates a new instance of MeetingOrganizerDbContext using the DbContextOptions provided in the constructor
    /// </summary>
    /// <returns></returns>
    public MeetingOrganizerDbContext Create()
    {
        return new MeetingOrganizerDbContext(_options);
    }
}
