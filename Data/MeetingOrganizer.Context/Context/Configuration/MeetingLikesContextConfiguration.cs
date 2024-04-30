using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

/// <summary>
/// Contains configuration for the MeetingLikes entity in the database context.
/// </summary>
public static class MeetingLikesContextConfiguration
{
    /// <summary>
    /// Configures the MeeringLikes entity in the database context.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to construct the database context.</param>
    public static void ConfigureMeetingLikes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MeetingLike>().ToTable("meeting_likes");

        modelBuilder.Entity<MeetingLike>()
            .HasKey(p => new { p.MeetingId, p.UserId });

        modelBuilder.Entity<MeetingLike>()
            .HasIndex(p => new { p.MeetingId, p.UserId });

        modelBuilder.Entity<MeetingLike>()
            .HasOne(x => x.Meeting)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.MeetingId);
    }
}
