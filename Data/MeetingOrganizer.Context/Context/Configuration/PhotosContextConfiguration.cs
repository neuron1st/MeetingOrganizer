using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

/// <summary>
/// Contains configuration for the Photos entity in the database context.
/// </summary>
public static class PhotosContextConfiguration
{
    /// <summary>
    /// Configures the Photos entity in the database context.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to construct the database context.</param>
    public static void ConfigurePhotos(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>().ToTable("photos");
        modelBuilder.Entity<Photo>().Property(x => x.Url).IsRequired();

        modelBuilder.Entity<Photo>()
            .HasOne(x => x.Meeting)
            .WithMany(x => x.Photos)
            .HasForeignKey(x => x.MeetingId);
    }
}
