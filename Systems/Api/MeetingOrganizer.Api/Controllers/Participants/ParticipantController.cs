using Asp.Versioning;
using AutoMapper;
using MeetingOrganizer.Common.Security;
using MeetingOrganizer.Services.Logger;
using MeetingOrganizer.Services.Participants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Security.Claims;

namespace MeetingOrganizer.Api.Controllers.Participants;

/// <summary>
/// API controller for managing participants.
/// </summary>
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

    /// <summary>
    /// Gets all participants of a meeting.
    /// </summary>
    [HttpGet("meeting/{meetingId:Guid}")]
    [Authorize(Policy = AppScopes.ParticipantsRead)]
    public async Task<IEnumerable<ParticipantResponse>> GetAllByMeetingId([FromRoute] Guid meetingId, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var participants = await _participantService.GetAllByMeetingId(meetingId, offset, limit);

        var result = _mapper.Map<IEnumerable<ParticipantResponse>>(participants);

        return result;
    }

    /// <summary>
    /// Gets all participants of a user.
    /// </summary>
    [HttpGet("user/{userId:Guid}")]
    [Authorize(Policy = AppScopes.ParticipantsRead)]
    public async Task<IEnumerable<ParticipantResponse>> GetAllByUserId([FromRoute] Guid userId, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var participants = await _participantService.GetAllByUserId(userId, offset, limit);

        var result = _mapper.Map<IEnumerable<ParticipantResponse>>(participants);

        return result;
    }

    /// <summary>
    /// Gets a participant by user and meeting IDs.
    /// </summary>
    [HttpGet("{userId:Guid}/{meetingId:Guid}")]
    [Authorize(Policy = AppScopes.ParticipantsRead)]
    public async Task<IActionResult> GetByUserAndMeetingId([FromRoute] Guid userId, [FromRoute] Guid meetingId)
    {
        var participant = await _participantService.GetByUserAndMeetingId(userId, meetingId);

        if (participant == null)
            return NotFound();

        var result = _mapper.Map<ParticipantResponse>(participant);

        return Ok(result);
    }

    /// <summary>
    /// Gets a participant by ID.
    /// </summary>
    [HttpGet("{id:Guid}")]
    [Authorize(Policy = AppScopes.ParticipantsRead)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var participant = await _participantService.GetById(id);

        if (participant == null)
            return NotFound();

        var result = _mapper.Map<ParticipantResponse>(participant);

        return Ok(result);
    }

    /// <summary>
    /// Creates a new participant.
    /// </summary>
    [HttpPost("")]
    [Authorize(Policy = AppScopes.ParticipantsWrite)]
    public async Task<IActionResult> Create(CreateRequest request)
    {
        try
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

            var model = _mapper.Map<CreateModel>(request);
            model.UserId = userId;

            var participant = await _participantService.Create(model);

            var result = _mapper.Map<ParticipantResponse>(participant);

            return Ok(result);
        }
        catch (NpgsqlException ex)
        {
            return Conflict("Participant already exists.");
        }
    }

    /// <summary>
    /// Updates an existing participant.
    /// </summary>
    [HttpPut("{id:Guid}")]
    [Authorize(Policy = AppScopes.ParticipantsWrite)]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateModel request)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        var model = _mapper.Map<UpdateModel>(request);
        model.UserId = userId;

        await _participantService.Update(id, model);

        return Ok();
    }

    /// <summary>
    /// Deletes a participant.
    /// </summary>
    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = AppScopes.ParticipantsWrite)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _participantService.Delete(id);

        return Ok();
    }
}
