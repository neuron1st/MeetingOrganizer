using Asp.Versioning;
using AutoMapper;
using MeetingOrganizer.Common.Security;
using MeetingOrganizer.Services.Logger;
using MeetingOrganizer.Services.Participants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [HttpGet("meeting/{meetingId:Guid}/participants")]
    [Authorize(Policy = AppScopes.ParticipantsRead)]
    public async Task<IEnumerable<ParticipantResponse>> GetAllByMeetingId([FromRoute] Guid meetingId, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var participants = await _participantService.GetAllByMeetingId(meetingId, offset, limit);

        var result = _mapper.Map<IEnumerable<ParticipantResponse>>(participants);

        return result;
    }

    [HttpGet("user/{userId:Guid}/participants")]
    [Authorize(Policy = AppScopes.ParticipantsRead)]
    public async Task<IEnumerable<ParticipantResponse>> GetAllByUserId([FromRoute] Guid userId, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var participants = await _participantService.GetAllByUserId(userId, offset, limit);

        var result = _mapper.Map<IEnumerable<ParticipantResponse>>(participants);

        return result;
    }

    [HttpGet("{id:Guid}")]
    [Authorize(Policy = AppScopes.ParticipantsRead)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await _participantService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    [Authorize(Policy = AppScopes.ParticipantsWrite)]
    public async Task<ParticipantResponse> Create(CreateRequest request)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        var model = _mapper.Map<CreateModel>(request);
        model.UserId = userId;

        var participant = await _participantService.Create(model);

        var result = _mapper.Map<ParticipantResponse>(participant);

        return result;
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Policy = AppScopes.ParticipantsWrite)]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateModel request)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        var model = _mapper.Map<UpdateModel>(request);
        model.userId = userId;

        await _participantService.Update(id, model);

        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = AppScopes.ParticipantsWrite)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _participantService.Delete(id);

        return Ok();
    }
}
