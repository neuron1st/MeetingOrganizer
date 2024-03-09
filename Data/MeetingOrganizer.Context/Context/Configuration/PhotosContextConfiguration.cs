using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Context.Context.Configuration;

public static class PhotosContextConfiguration
{
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
