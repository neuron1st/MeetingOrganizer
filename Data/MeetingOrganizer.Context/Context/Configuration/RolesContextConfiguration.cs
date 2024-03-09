using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

public static class RolesContextConfiguration
{
    public static void ConfigureRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().ToTable("roles");
        modelBuilder.Entity<Role>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Role>().Property(x => x.Name).HasMaxLength(20);
    }
}
