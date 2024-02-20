using MeetingOrganizer.Context.Context.Configuration;
using MeetingOrganizer.Context.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context;

public class MeetingOrganizerDbContext : IdentityDbContext<User, UserRole, Guid>
{
    public MeetingOrganizerDbContext(DbContextOptions<MeetingOrganizerDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureUsers();
    }
}
