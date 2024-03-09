using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

public static class CommentLikesContextConfiguration
{
    public static void ConfigureCommentLikes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommentLike>().ToTable("comment_likes");

        //modelBuilder.Entity<CommentLike>()
        //    .HasOne(x => x.User)
        //    .WithMany(x => x.CommentLikes)
        //    .HasForeignKey(x => x.UserId)
        //    .HasPrincipalKey(x => x.EntryId);

        modelBuilder.Entity<CommentLike>()
            .HasOne(x => x.Comment)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.CommentId);
    }
}
