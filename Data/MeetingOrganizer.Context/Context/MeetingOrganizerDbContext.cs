using MeetingOrganizer.Context.Context.Configuration;
using MeetingOrganizer.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context;

/// <summary>
/// Represents the database context for the MeetingOrganizer application.
/// </summary>
public class MeetingOrganizerDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    /// <summary>
    /// Gets or sets the DbSet of comments.
    /// </summary>
    public DbSet<Comment> Comments { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of comment likes.
    /// </summary>
    public DbSet<CommentLike> CommentLikes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of meeting likes.
    /// </summary>
    public DbSet<MeetingLike> MeetingLikes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of meetings.
    /// </summary>
    public DbSet<Meeting> Meetings { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of participants.
    /// </summary>
    public DbSet<Participant> Participants { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of photos.
    /// </summary>
    public DbSet<Photo> Photos { get; set; }

    /// <summary>
    /// Initializes a new instance of the MeetingOrganizerDbContext class.
    /// </summary>
    /// <param name="options">The DbContextOptions to be used by the context.</param>
    public MeetingOrganizerDbContext(DbContextOptions<MeetingOrganizerDbContext> options) : base(options) { }

    /// <summary>
    /// Configures the database schema for the model.
    /// </summary>
    /// <param name="modelBuilder">The model builder to be used for configuration.</param>
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
