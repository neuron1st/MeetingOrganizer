using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Common.Files;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.Photos;

public class CreateModel
{
    public FileData Image { get; set; }

    public Guid MeetingId { get; set; }

    public Guid UserId { get; set; }
}

public class CreateModelProfile : Profile
{
    public CreateModelProfile() 
    {
        CreateMap<CreateModel, Photo>()
            .ForMember(dest => dest.MeetingId, opt => opt.Ignore())
            .AfterMap<CreateModelActions>();
    }

    public class CreateModelActions : IMappingAction<CreateModel, Photo>
    {
        private readonly IDbContextFactory<MeetingOrganizerDbContext> contextFactory;

        public CreateModelActions(IDbContextFactory<MeetingOrganizerDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateModel source,Photo destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var meeting = db.Meetings.FirstOrDefault(x => x.Uid == source.MeetingId);

            destination.MeetingId = meeting.Id;

            destination.Meeting = meeting;
        }

    }
}


public class CreateModelValidator : AbstractValidator<CreateModel>
{
    public CreateModelValidator(IDbContextFactory<MeetingOrganizerDbContext> contextFactory)
    {
        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Photo is required");

        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("Meeting is required")
            .Must(id =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Meetings.Any(a => a.Uid == id);
                return found;
            }).WithMessage("Meeting not found");
    }
}