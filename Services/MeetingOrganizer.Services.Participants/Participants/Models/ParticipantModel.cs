using AutoMapper;
using MeetingOrganizer.Context.Entities;
using MeetingOrganizer.Common.Extensions;

namespace MeetingOrganizer.Services.Participants;

/// <summary>
/// Represents a model for a participant.
/// </summary>
public class ParticipantModel
{
    /// <summary>
    /// The identifier of the participant.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user ID of the participant.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The user name of the participant.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// The meeting ID of the participant.
    /// </summary>
    public Guid MeetingId { get; set; }

    /// <summary>
    /// The title of the meeting the participant belongs to.
    /// </summary>
    public string MeetingTitle { get; set; }

    /// <summary>
    /// The role of the participant.
    /// </summary>
    public string Role { get; set; }
}

/// <summary>
/// Profile for AutoMapper mapping of Participant to ParticipantModel.
/// </summary>
public class ParticipantModelProfile : Profile
{
    public ParticipantModelProfile()
    {
        CreateMap<Participant, ParticipantModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.GetDisplayName()))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.MeetingTitle, opt => opt.MapFrom(src => src.Meeting.Title))
            .ForMember(dest => dest.MeetingId, opt => opt.MapFrom(src => src.Meeting.Uid))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
    }
}
