using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

public static class ParticipantsContextConfiguration
{
    public static void ConfigureParticipants(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Participant>().ToTable("participants");

        modelBuilder.Entity<Participant>()
            .HasOne(x => x.Meeting)
            .WithMany(x => x.Participants)
            .HasForeignKey(x => x.MeetingId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Participant>()
            .HasOne(x => x.Role)
            .WithMany(x => x.Participants)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
