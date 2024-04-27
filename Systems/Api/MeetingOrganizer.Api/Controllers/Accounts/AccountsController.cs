using Asp.Versioning;
using AutoMapper;
using MeetingOrganizer.Api.Controllers.Accounts.Models;
using MeetingOrganizer.Services.UserAccount;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Claims;
using static StackExchange.Redis.Role;

namespace MeetingOrganizer.Api.Controllers.Accounts;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<AccountsController> _logger;
    private readonly IUserAccountService _userAccountService;

    public AccountsController(IMapper mapper, ILogger<AccountsController> logger, IUserAccountService userAccountService)
    {
        _mapper = mapper;
        _logger = logger;
        _userAccountService = userAccountService;
    }

    [HttpPost("")]
    public async Task<UserAccountResponse> Register([FromBody] RegisterUserAccountRequest request)
    {
        var model = _mapper.Map<RegisterUserAccountModel>(request);

        var user = await _userAccountService.Create(model);

        var response = _mapper.Map<UserAccountResponse>(user);

        return response;
    }

    [HttpPost("confirmation-email")]
    public async Task<IActionResult> SendEmailConfirmationLink([FromQuery] string email)
    {
        await _userAccountService.SendConfirmationLinkAsync(email);
        return Ok();
    }

    [HttpPut("confirm-email")]
    public async Task ConfirmEmail([FromQuery] string token, [FromQuery] string email)
    {
        await _userAccountService.ConfirmEmail(token, email);
    }

    [HttpGet("user")]
    public async Task<UserAccountResponse> GetById()
    {
        var userId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = await _userAccountService.GetById(userId);
        return _mapper.Map<UserAccountResponse>(user);
    }
}
