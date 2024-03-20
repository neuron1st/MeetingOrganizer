using Asp.Versioning;
using AutoMapper;
using MeetingOrganizer.Common.Security;
using MeetingOrganizer.Services.Logger.Logger;
using MeetingOrganizer.Services.Meetings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingOrganizer.Api.Controllers.Meetings;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/meetings")]
[Produces("application/json")]
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
    [Authorize(AppScopes.MeetingsRead)]
    public async Task<IEnumerable<MeetingResponse>> GetAll([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var meetings = await _meetingService.GetAll(offset, limit);

        var result = _mapper.Map<IEnumerable<MeetingResponse>>(meetings);

        return result;
    }

    [HttpGet("{id:Guid}")]
    [Authorize(AppScopes.MeetingsRead)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await _meetingService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    [Authorize(AppScopes.MeetingsWrite)]
    public async Task<MeetingResponse> Create(CreateRequest request)
    {
        var model = _mapper.Map<CreateModel>(request);

        var meeting = await _meetingService.Create(model);

        var result = _mapper.Map<MeetingResponse>(meeting);

        return result;
    }

    [HttpPut("{id:Guid}")]
    [Authorize(AppScopes.MeetingsWrite)]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateModel request)
    {
        var model = _mapper.Map<UpdateModel>(request);

        await _meetingService.Update(id, model);

        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(AppScopes.MeetingsWrite)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _meetingService.Delete(id);

        return Ok();
    }
}
