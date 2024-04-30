using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

/// <summary>
/// Contains configuration for the Meetings entity in the database context.
/// </summary>
public static class MeetingsContextConfiguration
{
    /// <summary>
    /// Configures the CommentLikes entity in the database context.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to construct the database context.</param>
    public static void ConfigureMeetings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meeting>().ToTable("meetings");
        modelBuilder.Entity<Meeting>().Property(x => x.Title).IsRequired();
        modelBuilder.Entity<Meeting>().Property(x => x.Title).HasMaxLength(100);
        modelBuilder.Entity<Meeting>().Property(x => x.Description).HasMaxLength(2000);
    }
}
