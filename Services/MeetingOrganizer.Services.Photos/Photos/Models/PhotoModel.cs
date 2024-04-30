using AutoMapper;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using MeetingOrganizer.Services.Settings;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.Photos;

public class PhotoModel
{
    public Guid Id { get; set; }

    public string Image { get; set; }

    public Guid MeetingId { get; set; }
}

public class PhotoModelProfile : Profile
{
    public PhotoModelProfile()
    {
        CreateMap<Photo, PhotoModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest=>dest.MeetingId, opt=>opt.MapFrom(src => src.Meeting.Uid))
            .ForMember(dest => dest.Image, opt => opt.Ignore())
            .AfterMap<PhotoModelActions>();
    }

    public class PhotoModelActions : IMappingAction<Photo, PhotoModel>
    {
        private readonly IDbContextFactory<MeetingOrganizerDbContext> dbContextFactory;
        private readonly MainSettings mainSettings;

        public PhotoModelActions(IDbContextFactory<MeetingOrganizerDbContext> dbContextFactory, MainSettings mainSettings)
        {
            this.dbContextFactory = dbContextFactory;
            this.mainSettings = mainSettings;
        }

        public void Process(Photo source, PhotoModel destination, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Url))
                destination.Image = Path.Combine(mainSettings.FileDirectory, source.Url);
        }
    }
}