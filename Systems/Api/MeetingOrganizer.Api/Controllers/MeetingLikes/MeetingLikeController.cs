using Asp.Versioning;
using MeetingOrganizer.Services.Logger;
using MeetingOrganizer.Services.MeetingLikes;
using MeetingOrganizer.Services.Meetings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingOrganizer.Api.Controllers.MeetingLikes;


[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MeetingLikeController : ControllerBase
{
    private readonly IAppLogger _logger;
    private readonly IMeetingLikeService _meetingLikeService;

    public MeetingLikeController(IAppLogger logger, IMeetingLikeService meetingLikeService)
    {
        _logger = logger;
        _meetingLikeService = meetingLikeService;
    }

    [HttpPost("user")]
    public async Task<IActionResult> LikeMeeting(Guid meetingId)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
        {
            return BadRequest("Invalid user ID format");
        }

        await _meetingLikeService.AddLike(new MeetingLikeModel { MeetingId = meetingId, UserId = userId });

        return Ok();
    }

    [HttpDelete("user")]
    public async Task<IActionResult> UnlikeMeeting(Guid meetingId)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
        {
            return BadRequest("Invalid user ID format");
        }

        await _meetingLikeService.RemoveLike(new MeetingLikeModel { MeetingId = meetingId, UserId = userId });

        return Ok();
    }
}
