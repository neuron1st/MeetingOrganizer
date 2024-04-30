using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

/// <summary>
/// Contains configuration for the Comment entity in the database context.
/// </summary>
public static class CommentsContextConfigure
{
    /// <summary>
    /// Configures the Comments entity in the database context.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to construct the database context.</param>
    public static void ConfigureComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>().ToTable("comments");
        modelBuilder.Entity<Comment>().Property(x => x.Text).IsRequired();
        modelBuilder.Entity<Comment>().Property(x => x.Text).HasMaxLength(500);

        modelBuilder.Entity<Comment>()
            .HasOne(x => x.Meeting)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.MeetingId);
    }
}
