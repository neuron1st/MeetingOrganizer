using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.MeetingLikes;

/// <summary>
/// Model for a meeting like.
/// </summary>
public class MeetingLikeModel
{
    public Guid UserId { get; set; }
    public Guid MeetingId { get; set; }
}

/// <summary>
/// AutoMapper profile for mapping MeetingLikeModel to MeetingLike entity.
/// </summary>
public class MeetingLikeModelProfile : Profile
{
    public MeetingLikeModelProfile()
    {
        CreateMap<MeetingLikeModel, MeetingLike>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.MeetingId, opt => opt.Ignore())
            .AfterMap<CreateModelActions>();
    }

    /// <summary>
    /// Mapping actions for additional processing after mapping MeetingLikeModel to MeetingLike.
    /// </summary>
    public class CreateModelActions : IMappingAction<MeetingLikeModel, MeetingLike>
    {
        public readonly IDbContextFactory<MeetingOrganizerDbContext> contextFactory;

        public CreateModelActions(IDbContextFactory<MeetingOrganizerDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(MeetingLikeModel source, MeetingLike destination, ResolutionContext context)
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
/// FluentValidation validator for MeetingLikeModel.
/// </summary>
public class MeetingLikeModelValidator : AbstractValidator<MeetingLikeModel>
{
    public MeetingLikeModelValidator(IDbContextFactory<MeetingOrganizerDbContext> contextFactory)
    {
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
