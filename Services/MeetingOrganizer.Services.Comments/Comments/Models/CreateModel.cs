using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.Comments;

/// <summary>
/// Represents a model for creating a new comment.
/// </summary>
public class CreateModel
{
    public string Text { get; set; }

    public Guid UserId { get; set; }

    public Guid MeetingId { get; set; }
}

/// <summary>
/// Profile for mapping create model to comment entity.
/// </summary>
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

/// <summary>
/// Validator for the create comment model.
/// </summary>
public class CreateModelValidator : AbstractValidator<CreateModel>
{
    public CreateModelValidator(IDbContextFactory<MeetingOrganizerDbContext> contextFactory)
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Comment text is required")
            .MaximumLength(500).WithMessage("Text is too long");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User is required")
            .Must(id =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Users.Any(a => a.Id == id);
                return found;
            }).WithMessage("User not found");

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
