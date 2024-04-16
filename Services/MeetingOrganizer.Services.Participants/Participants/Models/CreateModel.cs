using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Common.Helpers;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.Participants;

public class CreateModel
{
    public Guid UserId { get; set; }
    public Guid MeetingId { get; set; }
    public string Role { get; set; }
}

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