using Asp.Versioning;
using MeetingOrganizer.Services.CommentLikes;
using MeetingOrganizer.Services.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingOrganizer.Api.Controllers.CommentLikes;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CommentLikeController : ControllerBase
{
    private readonly IAppLogger _logger;
    private readonly ICommentLikeService _commentLikeService;

    public CommentLikeController(IAppLogger logger, ICommentLikeService commentLikeService)
    {
        _logger = logger;
        _commentLikeService = commentLikeService;
    }

    [HttpPost("user")]
    public async Task<IActionResult> LikeComment(Guid commentId)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
        {
            return BadRequest("Invalid user ID format");
        }

        await _commentLikeService.AddLike(new CommentLikeModel { CommentId = commentId, UserId = userId });

        return Ok();
    }

    [HttpDelete("user")]
    public async Task<IActionResult> UnlikeComment(Guid commentId)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
        {
            return BadRequest("Invalid user ID format");
        }

        await _commentLikeService.RemoveLike(new CommentLikeModel { CommentId = commentId, UserId = userId });

        return Ok();
    }
}
