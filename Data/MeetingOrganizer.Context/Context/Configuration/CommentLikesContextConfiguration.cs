using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

/// <summary>
/// Contains configuration for the CommentLikes entity in the database context.
/// </summary>
public static class CommentLikesContextConfiguration
{
    /// <summary>
    /// Configures the CommentLikes entity in the database context.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to construct the database context.</param>
    public static void ConfigureCommentLikes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommentLike>().ToTable("comment_likes");

        modelBuilder.Entity<CommentLike>()
            .HasKey(x => new { x.UserId, x.CommentId });

        modelBuilder.Entity<CommentLike>()
            .HasIndex(x => new { x.UserId, x.CommentId });

        modelBuilder.Entity<CommentLike>()
            .HasOne(x => x.Comment)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.CommentId);
    }
}