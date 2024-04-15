using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.Comments;

public class CreateModel
{
    public string Text { get; set; }

    public Guid UserId { get; set; }

    public Guid MeetingId { get; set; }
}

public class CreateModelProfile : Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateModel, Comment>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.MeetingId, opt => opt.Ignore())
            .AfterMap<CreateModelActions>();
    }

    public class CreateModelActions : IMappingAction<CreateModel, Comment>
    {
        private readonly IDbContextFactory<MeetingOrganizerDbContext> contextFactory;

        public CreateModelActions(IDbContextFactory<MeetingOrganizerDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateModel source, Comment destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var user = db.Users.FirstOrDefault(x => x.Id == source.UserId);

            var meeting = db.Meetings.FirstOrDefault(x => x.Uid == source.MeetingId);

            destination.UserId = user.EntryId;

            destination.MeetingId = meeting.Id;
        }
    }
}


public class CreateModelValidator : AbstractValidator<CreateModel>
{
    public CreateModelValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Comment text is required")
            .MaximumLength(500).WithMessage("Text is too long");
    }
}