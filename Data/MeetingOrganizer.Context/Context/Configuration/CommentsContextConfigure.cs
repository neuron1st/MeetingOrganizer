using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

public static class CommentsContextConfigure
{
    public static void ConfigureComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>().ToTable("comments");
        modelBuilder.Entity<Comment>().Property(x => x.Text).IsRequired();
        modelBuilder.Entity<Comment>().Property(x => x.Text).HasMaxLength(500);

        //modelBuilder.Entity<Comment>()
        //    .HasOne(x => x.User)
        //    .WithMany(x => x.Comments)
        //    .HasForeignKey(x => x.UserId)
        //    .HasPrincipalKey(x => x.EntryId);

        modelBuilder.Entity<Comment>()
            .HasOne(x => x.Meeting)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.MeetingId);
    }
}
