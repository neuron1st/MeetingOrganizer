using MeetingOrganizer.Context.Context.Configuration;
using MeetingOrganizer.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context;

public class MeetingOrganizerDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CommentLike> CommentsLikes { get; set; }
    public DbSet<MeetingLike> MeetingLikes { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Photo> Photos { get; set; }


    public MeetingOrganizerDbContext(DbContextOptions<MeetingOrganizerDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureUsers();
        modelBuilder.ConfigureComments();
        modelBuilder.ConfigureCommentLikes();
        modelBuilder.ConfigureMeetingLikes();
        modelBuilder.ConfigureMeetings();
        modelBuilder.ConfigureParticipants();
        modelBuilder.ConfigurePhotos();
    }
}
