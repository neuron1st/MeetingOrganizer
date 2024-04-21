using Asp.Versioning;
using AutoMapper;
using MeetingOrganizer.Common.Security;
using MeetingOrganizer.Services.Logger;
using MeetingOrganizer.Services.Meetings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingOrganizer.Api.Controllers.Meetings;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MeetingController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAppLogger _logger;
    private readonly IMeetingService _meetingService;

    public MeetingController(IAppLogger logger, IMeetingService meetingService, IMapper mapper)
    {
        _logger = logger;
        _meetingService = meetingService;
        _mapper = mapper;
    }

    [HttpGet("")]
    [Authorize(Policy = AppScopes.MeetingsRead)]
    public async Task<IEnumerable<MeetingResponse>> GetAll([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var meetings = await _meetingService.GetAll(offset, limit);

        var result = _mapper.Map<IEnumerable<MeetingResponse>>(meetings);

        return result;
    }

    [HttpGet("{id:Guid}")]
    [Authorize(Policy = AppScopes.MeetingsRead)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var meeting = await _meetingService.GetById(id);

        if (meeting == null)
            return NotFound();

        var result = _mapper.Map<MeetingResponse>(meeting);

        return Ok(result);
    }

    [HttpPost("")]
    [Authorize(Policy = AppScopes.MeetingsWrite)]
    public async Task<MeetingResponse> Create(CreateRequest request)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        var model = _mapper.Map<CreateModel>(request);

        model.UserId = userId;

        var meeting = await _meetingService.Create(model);

        var result = _mapper.Map<MeetingResponse>(meeting);

        return result;
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Policy = AppScopes.MeetingsWrite)]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateModel request)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        var model = _mapper.Map<UpdateModel>(request);
        model.UserId = userId;

        await _meetingService.Update(id, model);

        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = AppScopes.MeetingsWrite)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        await _meetingService.Delete(id, userId);

        return Ok();
    }
}
