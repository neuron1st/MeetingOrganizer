using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

/// <summary>
/// Contains configuration for the Participants entity in the database context.
/// </summary>
public static class ParticipantsContextConfiguration
{
    /// <summary>
    /// Configures the Participants entity in the database context.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to construct the database context.</param>
    public static void ConfigureParticipants(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Participant>().ToTable("participants");

        modelBuilder.Entity<Participant>()
            .Property(x => x.Role)
            .HasConversion(
                v => v.ToString(),
                v => (Role)Enum.Parse(typeof(Role), v));

        modelBuilder.Entity<Participant>()
            .HasIndex(p => new { p.MeetingId, p.UserId })
            .IsUnique();

        modelBuilder.Entity<Participant>()
            .HasOne(x => x.Meeting)
            .WithMany(x => x.Participants)
            .HasForeignKey(x => x.MeetingId);
    }
}
