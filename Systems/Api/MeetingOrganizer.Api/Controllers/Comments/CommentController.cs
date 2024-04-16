using Asp.Versioning;
using AutoMapper;
using MeetingOrganizer.Services.Comments;
using MeetingOrganizer.Services.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("meeting/{meetingId:Guid}")]
    public async Task<IEnumerable<CommentResponse>> GetAllByMeetingId([FromRoute] Guid meetingId, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var comments = await _commentService.GetAllByMeetingId(meetingId, offset, limit);

        var result = _mapper.Map<IEnumerable<CommentResponse>>(comments);

        return result;
    }

    [HttpGet("user/{userId:Guid}")]
    public async Task<IEnumerable<CommentResponse>> GetAllByUserId([FromRoute] Guid userId, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var comments = await _commentService.GetAllByUserId(userId, offset, limit);

        var result = _mapper.Map<IEnumerable<CommentResponse>>(comments);

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var comment = await _commentService.GetById(id);

        if (comment == null)
            return NotFound();

        var result = _mapper.Map<CommentResponse>(comment);

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<CommentResponse> Create(CreateRequest request)
    {
        var model = _mapper.Map<CreateModel>(request);

        var comment = await _commentService.Create(model);

        var result = _mapper.Map<CommentResponse>(comment);

        return result;
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateModel request)
    {
        var model = _mapper.Map<UpdateModel>(request);

        await _commentService.Update(id, model);

        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _commentService.Delete(id);

        return Ok();
    }
}
