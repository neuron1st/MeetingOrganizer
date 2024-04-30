using Asp.Versioning;
using MeetingOrganizer.Services.Logger;
using MeetingOrganizer.Services.MeetingLikes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingOrganizer.Api.Controllers.MeetingLikes;

/// <summary>
/// API controller for managing likes on meetings.
/// </summary>
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

    /// <summary>
    /// Likes a meeting.
    /// </summary>
    [HttpPost("{meetingId:Guid}/like")]
    public async Task<IActionResult> LikeMeeting([FromRoute] Guid meetingId)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
        {
            return BadRequest("Invalid user ID format");
        }

        if (!await _meetingLikeService.CheckLike(meetingId, userId))
            await _meetingLikeService.AddLike(new MeetingLikeModel { MeetingId = meetingId, UserId = userId });

        return Ok();
    }

    /// <summary>
    /// Unlikes a meeting.
    /// </summary>
    [HttpDelete("{meetingId:Guid}/unlike")]
    public async Task<IActionResult> UnlikeMeeting([FromRoute] Guid meetingId)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
        {
            return BadRequest("Invalid user ID format");
        }

        if (await _meetingLikeService.CheckLike(meetingId, userId))
            await _meetingLikeService.RemoveLike(new MeetingLikeModel { MeetingId = meetingId, UserId = userId });

        return Ok();
    }
}
