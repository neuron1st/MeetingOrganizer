using Asp.Versioning;
using AutoMapper;
using MeetingOrganizer.Common.Security;
using MeetingOrganizer.Services.Comments;
using MeetingOrganizer.Services.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingOrganizer.Api.Controllers.Comments;


[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAppLogger _logger;
    private readonly ICommentService _commentService;

    public CommentController(IMapper mapper, IAppLogger logger, ICommentService commentService)
    {
        _mapper = mapper;
        _logger = logger;
        _commentService = commentService;
    }

    [HttpGet("meeting/{meetingId:Guid}/comments")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    public async Task<IEnumerable<CommentResponse>> GetAllByMeetingId([FromRoute] Guid meetingId, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var comments = await _commentService.GetAllByMeetingId(meetingId, offset, limit);

        var result = _mapper.Map<IEnumerable<CommentResponse>>(comments);

        return result;
    }

    [HttpGet("user/{userId:Guid}/comments")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    public async Task<IEnumerable<CommentResponse>> GetAllByUserId([FromRoute] Guid userId, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var comments = await _commentService.GetAllByUserId(userId, offset, limit);

        var result = _mapper.Map<IEnumerable<CommentResponse>>(comments);

        return result;
    }

    [HttpGet("{id:Guid}")]
    [Authorize(Policy = AppScopes.CommentsRead)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var comment = await _commentService.GetById(id);

        if (comment == null)
            return NotFound();

        var result = _mapper.Map<CommentResponse>(comment);

        return Ok(result);
    }

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


    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = AppScopes.CommentsWrite)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        await _commentService.Delete(id, userId);

        return Ok();
    }
}
