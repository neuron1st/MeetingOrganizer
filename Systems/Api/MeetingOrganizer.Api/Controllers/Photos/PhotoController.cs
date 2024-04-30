using Asp.Versioning;
using AutoMapper;
using MeetingOrganizer.Services.Logger;
using MeetingOrganizer.Services.Photos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingOrganizer.Api.Controllers.Photos;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PhotoController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAppLogger _logger;
    private readonly IPhotoService _photoService;

    public PhotoController(IMapper mapper, IAppLogger logger, IPhotoService photoService)
    {
        _mapper = mapper;
        _logger = logger;
        _photoService = photoService;
    }

    [HttpGet("")]
    [Authorize]
    public async Task<IEnumerable<PhotoResponse>> GetAll()
    {
        var photos = await _photoService.GetAll();

        var result = _mapper.Map<IEnumerable<PhotoResponse>>(photos);

        return result;
    }

    [HttpGet("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var photo = await _photoService.GetById(id);

        if (photo == null)
        {
            return NotFound();
        }

        var result = _mapper.Map<PhotoResponse>(photo);

        return Ok(result);
    }

    [HttpGet("meeting/{meetingId}")]
    [Authorize]
    public async Task<IEnumerable<PhotoResponse>> GetAllByMeetingId([FromRoute] Guid meetingId)
    {
        var photos = await _photoService.GetByMeeting(meetingId);

        var result = _mapper.Map<IEnumerable<PhotoResponse>>(photos);

        return result;
    }

    [HttpPost("")]
    [Authorize]
    public async Task<PhotoResponse> Create([FromBody] CreateRequest request)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        var model = _mapper.Map<CreateModel>(request);

        model.UserId = userId;

        var photo = await _photoService.Create(model);

        var result = _mapper.Map<PhotoResponse>(photo);

        return result;
    }

    [HttpDelete("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        await _photoService.Delete(id, userId);

        return Ok();
    }
}
