using AutoMapper;
using MeetingOrganizer.Services.Participants;

namespace MeetingOrganizer.Api.Controllers.Participants;

public class ParticipantResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public string UserName { get; set; }

    public Guid MeetingId { get; set; }
    public string MeetingTitle { get; set; }

    public string Role { get; set; }
}

public class ParticipantResponseProfile : Profile
{
    public ParticipantResponseProfile()
    {
        CreateMap<ParticipantModel, ParticipantResponse>();
    }
}