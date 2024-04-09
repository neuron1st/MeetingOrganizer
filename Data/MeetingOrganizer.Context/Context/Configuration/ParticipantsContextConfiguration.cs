using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

public static class ParticipantsContextConfiguration
{
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
