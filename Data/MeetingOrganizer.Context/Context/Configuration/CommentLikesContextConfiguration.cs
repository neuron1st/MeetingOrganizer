using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

public static class CommentLikesContextConfiguration
{
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