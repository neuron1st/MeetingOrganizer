using Asp.Versioning;
using AutoMapper;
using MeetingOrganizer.Common.Security;
using MeetingOrganizer.Services.Logger;
using MeetingOrganizer.Services.Meetings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingOrganizer.Api.Controllers.Meetings;

/// <summary>
/// API controller for managing meetings.
/// </summary>
[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Produces("application/json")]
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

    /// <summary>
    /// Gets all meetings.
    /// </summary>
    [HttpGet("")]
    [Authorize(Policy = AppScopes.MeetingsRead)]
    public async Task<IEnumerable<MeetingResponse>> GetAll([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var meetings = await _meetingService.GetAll(offset, limit);

        var result = _mapper.Map<IEnumerable<MeetingResponse>>(meetings);

        return result;
    }

    /// <summary>
    /// Gets a meeting by its ID.
    /// </summary>
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

    /// <summary>
    /// Creates a new meeting.
    /// </summary>
    [HttpPost("")]
    [Authorize(Policy = AppScopes.MeetingsWrite)]
    public async Task<MeetingResponse> Create([FromBody] CreateRequest request/*, [FromQuery] string path*/)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        //// temporary solution there is no frontend yet
        //byte[] fileContent;
        //using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        //{
        //    fileContent = new byte[fs.Length];
        //    await fs.ReadAsync(fileContent, 0, (int)fs.Length);
        //}
        //string base64Content = Convert.ToBase64String(fileContent);
        //request.Image.Content = base64Content;

        var model = _mapper.Map<CreateModel>(request);

        model.UserId = userId;

        var meeting = await _meetingService.Create(model);

        var result = _mapper.Map<MeetingResponse>(meeting);

        return result;
    }

    /// <summary>
    /// Updates an existing meeting.
    /// </summary>
    [HttpPut("{id:Guid}")]
    [Authorize(Policy = AppScopes.MeetingsWrite)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateModel request)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        var model = _mapper.Map<UpdateModel>(request);
        model.UserId = userId;

        await _meetingService.Update(id, model);

        return Ok();
    }

    /// <summary>
    /// Deletes a meeting.
    /// </summary>
    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = AppScopes.MeetingsWrite)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        await _meetingService.Delete(id, userId);

        return Ok();
    }
}
