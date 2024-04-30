using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Common.Helpers;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.Participants;

/// <summary>
/// Represents a model for creating a participant.
/// </summary>
public class CreateModel
{
    /// <summary>
    /// The identifier of the user who is a participant.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The identifier of the meeting the participant belongs to.
    /// </summary>
    public Guid MeetingId { get; set; }

    /// <summary>
    /// User role in current meeting.
    /// </summary>
    public string Role { get; set; }
}

/// <summary>
/// Profile for AutoMapper mapping of CreateModel to Participant.
/// </summary>
public class CreateModelProfile : Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateModel, Participant>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<Role>(src.Role)))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.MeetingId, opt => opt.Ignore())
            .AfterMap<CreateModelActions>();
    }

    /// <summary>
    /// Actions to be performed after mapping CreateModel to Participant.
    /// </summary>
    public class CreateModelActions : IMappingAction<CreateModel, Participant>
    {
        private readonly IDbContextFactory<MeetingOrganizerDbContext> contextFactory;

        public CreateModelActions(IDbContextFactory<MeetingOrganizerDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateModel source, Participant destination, ResolutionContext context)
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
/// Validator for CreateModel.
/// </summary>
public class CreateModelValidator : AbstractValidator<CreateModel>
{
    public CreateModelValidator(IDbContextFactory<MeetingOrganizerDbContext> contextFactory)
    {
        RuleFor(x => x.Role)
            .Must(EnumValidatorHelper.IsValidEnum<Role>)
            .WithMessage("Invalid role.");

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
