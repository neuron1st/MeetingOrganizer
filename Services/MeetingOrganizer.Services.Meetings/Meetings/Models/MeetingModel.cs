using AutoMapper;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using MeetingOrganizer.Services.Settings;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.Meetings;

public class MeetingModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date {  get; set; }
    public string Image {  get; set; }

    public int ParticipantsNumber { get; set; }
    public int LikesNumber { get; set; }
    public int CommentsNumber { get; set; }
}

public class MeetingModelProfile : Profile
{
    public MeetingModelProfile()
    {
        CreateMap<Meeting, MeetingModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Image, opt => opt.Ignore())
            .ForMember(dest => dest.ParticipantsNumber, opt => opt.MapFrom(src => src.Participants.Count))
            .ForMember(dest => dest.LikesNumber, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.CommentsNumber, opt => opt.MapFrom(src => src.Comments.Count))
            .AfterMap<MeetingModelActions>();
    }

    public class MeetingModelActions : IMappingAction<Meeting, MeetingModel>
    {
        public readonly IDbContextFactory<MeetingOrganizerDbContext> dbContextFactory;
        private readonly MainSettings mainSettings;

        public MeetingModelActions(IDbContextFactory<MeetingOrganizerDbContext> dbContextFactory, MainSettings mainSettings)
        {
            this.dbContextFactory = dbContextFactory;
            this.mainSettings = mainSettings;
        }

        public void Process(Meeting source, MeetingModel destination, ResolutionContext context)
        {
            using var db = dbContextFactory.CreateDbContext();

            var model = db.Meetings.FirstOrDefault(x => x.Id == source.Id);

            if(!string.IsNullOrEmpty(source.Image))
                destination.Image = Path.Combine(mainSettings.FileDirectory, source.Image);
        }
    }
}
