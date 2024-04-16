using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

public static class MeetingLikesContextConfiguration
{
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
