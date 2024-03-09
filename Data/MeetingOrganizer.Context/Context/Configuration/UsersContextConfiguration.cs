using MeetingOrganizer.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

public static class UsersContextConfiguration
{
    public static void ConfigureUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("user_roles");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_role_owners");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("user_role_claims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");

        modelBuilder.Entity<User>()
            .HasMany(x=>x.Participants)
            .WithOne(x=>x.User)
            .HasForeignKey(x=>x.UserId)
            .HasPrincipalKey(x=>x.EntryId);

        modelBuilder.Entity<User>()
            .HasMany(x => x.Comments)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.EntryId);

        modelBuilder.Entity<User>()
            .HasMany(x => x.CommentLikes)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.EntryId);

        modelBuilder.Entity<User>()
            .HasMany(x => x.MeetingLikes)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.EntryId);
    }
}
