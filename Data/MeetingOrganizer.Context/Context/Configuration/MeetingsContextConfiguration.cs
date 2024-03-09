using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

public static class MeetingsContextConfiguration
{
    public static void ConfigureMeetings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meeting>().ToTable("meetings");
        modelBuilder.Entity<Meeting>().Property(x => x.Title).IsRequired();
        modelBuilder.Entity<Meeting>().Property(x => x.Title).HasMaxLength(100);
        modelBuilder.Entity<Meeting>().Property(x => x.Description).HasMaxLength(2000);
    }
}
