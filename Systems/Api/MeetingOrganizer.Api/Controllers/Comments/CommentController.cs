using Asp.Versioning;
using AutoMapper;
using MeetingOrganizer.Common.Security;
using MeetingOrganizer.Services.CommentLikes;
using MeetingOrganizer.Services.Comments;
using MeetingOrganizer.Services.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingOrganizer.Api.Controllers.Comments;

/// <summary>
/// API controller for managing comments.
/// </summary>
[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAppLogger _logger;
    private readonly ICommentService _commentService;
    private readonly ICommentLikeService _commentLikeService;

    public CommentController(IMapper mapper, IAppLogger logger, ICommentService commentService, ICommentLikeService commentLikeService)
    {
        _mapper = mapper;
        _logger = logger;
        _commentService = commentService;
        _commentLikeService = commentLikeService;
    }

    /// <summary>
    /// Gets all comments for a meeting.
    /// </summary>
    [HttpGet("meeting/{meetingId:Guid}")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    public async Task<IEnumerable<CommentResponse>> GetAllByMeetingId([FromRoute] Guid meetingId, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        var comments = await _commentService.GetAllByMeetingId(meetingId, offset, limit);

        var commentResponses = new List<CommentResponse>();
        foreach (var comment in comments)
        {
            var commentResponse = _mapper.Map<CommentResponse>(comment);
            commentResponse.IsLiked = await CheckLike(comment.Id, userId);
            commentResponses.Add(commentResponse);
        }

        return commentResponses;
    }

    /// <summary>
    /// Gets all comments by a user.
    /// </summary>
    [HttpGet("user/{userId:Guid}")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    public async Task<IEnumerable<CommentResponse>> GetAllByUserId([FromRoute] Guid userId, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid currentUserId);

        var comments = await _commentService.GetAllByUserId(userId, offset, limit);

        var commentResponses = new List<CommentResponse>();
        foreach (var comment in comments)
        {
            var commentResponse = _mapper.Map<CommentResponse>(comment);
            commentResponse.IsLiked = await CheckLike(comment.Id, currentUserId);
            commentResponses.Add(commentResponse);
        }

        return commentResponses;
    }

    /// <summary>
    /// Gets a comment by its ID.
    /// </summary>
    [HttpGet("{id:Guid}")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var comment = await _commentService.GetById(id);

        if (comment == null)
            return NotFound();

        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        var commentResponse = _mapper.Map<CommentResponse>(comment);
        commentResponse.IsLiked = await CheckLike(comment.Id, userId);

        return Ok(commentResponse);
    }

    /// <summary>
    /// Creates a new comment.
    /// </summary>
    [HttpPost("")]
    [Authorize(Policy = AppScopes.CommentsWrite)]
    public async Task<CommentResponse> Create(CreateRequest request)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        var model = _mapper.Map<CreateModel>(request);
        model.UserId = userId;

        var comment = await _commentService.Create(model);

        var result = _mapper.Map<CommentResponse>(comment);

        return result;
    }

    /// <summary>
    /// Updates an existing comment.
    /// </summary>
    [HttpPut("{id:Guid}")]
    [Authorize(Policy = AppScopes.CommentsWrite)]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateModel request)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        var comment = await _commentService.GetById(id);

        if (comment == null)
            return NotFound();

        if (comment.UserId != userId)
            return Forbid();

        var model = _mapper.Map<UpdateModel>(request);

        await _commentService.Update(id, model);

        return Ok();
    }

    /// <summary>
    /// Deletes a comment.
    /// </summary>
    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = AppScopes.CommentsWrite)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        await _commentService.Delete(id, userId);

        return Ok();
    }

    private async Task<bool> CheckLike(Guid commentId, Guid userId)
    {
        return await _commentLikeService.CheckLike(commentId, userId);
    }
}
