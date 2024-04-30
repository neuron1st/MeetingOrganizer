using Asp.Versioning;
using MeetingOrganizer.Services.CommentLikes;
using MeetingOrganizer.Services.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingOrganizer.Api.Controllers.CommentLikes
{
    /// <summary>
    /// API controller for managing likes on comments.
    /// </summary>
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

        /// <summary>
        /// Likes a comment.
        /// </summary>
        [HttpPost("{commentId:Guid}/like")]
        public async Task<IActionResult> LikeComment([FromRoute] Guid commentId)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return BadRequest("Invalid user ID format");
            }

            await _commentLikeService.AddLike(new CommentLikeModel { CommentId = commentId, UserId = userId });

            return Ok();
        }

        /// <summary>
        /// Unlikes a comment.
        /// </summary>
        [HttpDelete("{commentId:Guid}/unlike")]
        public async Task<IActionResult> UnlikeComment([FromRoute] Guid commentId)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return BadRequest("Invalid user ID format");
            }

            await _commentLikeService.RemoveLike(new CommentLikeModel { CommentId = commentId, UserId = userId });

            return Ok();
        }
    }
}
