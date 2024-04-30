using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Common.Helpers;
using MeetingOrganizer.Context.Entities;

namespace MeetingOrganizer.Services.Participants;

/// <summary>
/// Represents a model for updating a participant.
/// </summary>
public class UpdateModel
{
    /// <summary>
    /// The role of the participant.
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// The user ID of the participant.
    /// </summary>
    public Guid UserId { get; set; }
}

/// <summary>
/// Profile for AutoMapper mapping of UpdateModel to Participant.
/// </summary>
public class UpdateModelProfile : Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateModel, Participant>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<Role>(src.Role)));
    }
}

/// <summary>
/// Validator for UpdateModel.
/// </summary>
public class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator()
    {
        RuleFor(x => x.Role).Must(EnumValidatorHelper.IsValidEnum<Role>).WithMessage("Invalid role.");
    }
}
