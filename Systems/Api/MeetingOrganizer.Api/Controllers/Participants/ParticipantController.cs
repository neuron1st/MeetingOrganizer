using Asp.Versioning;
using AutoMapper;
using MeetingOrganizer.Services.Logger;
using MeetingOrganizer.Services.Participants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingOrganizer.Api.Controllers.Participants;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ParticipantController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAppLogger _logger;
    private readonly IParticipantService _participantService;

    public ParticipantController(IMapper mapper, IAppLogger logger, IParticipantService participantService)
    {
        _mapper = mapper;
        _logger = logger;
        _participantService = participantService;
    }

    [HttpGet("meeting/{meetingId:Guid}")]
    public async Task<IEnumerable<ParticipantResponse>> GetAllByMeetingId([FromRoute] Guid meetingId, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var participants = await _participantService.GetAllByMeetingId(meetingId, offset, limit);

        var result = _mapper.Map<IEnumerable<ParticipantResponse>>(participants);

        return result;
    }

    [HttpGet("user/{userId:Guid}")]
    public async Task<IEnumerable<ParticipantResponse>> GetAllByUserId([FromRoute] Guid userId, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var participants = await _participantService.GetAllByUserId(userId, offset, limit);

        var result = _mapper.Map<IEnumerable<ParticipantResponse>>(participants);

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await _participantService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<ParticipantResponse> Create(CreateRequest request)
    {
        var model = _mapper.Map<CreateModel>(request);

        var participant = await _participantService.Create(model);

        var result = _mapper.Map<ParticipantResponse>(participant);

        return result;
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateModel request)
    {
        var model = _mapper.Map<UpdateModel>(request);

        await _participantService.Update(id, model);

        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _participantService.Delete(id);

        return Ok();
    }
}
