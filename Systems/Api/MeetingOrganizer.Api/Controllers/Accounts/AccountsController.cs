using Asp.Versioning;
using AutoMapper;
using MeetingOrganizer.Api.Controllers.Accounts.Models;
using MeetingOrganizer.Services.UserAccount;
using Microsoft.AspNetCore.Mvc;

namespace MeetingOrganizer.Api.Controllers.Accounts;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/accounts")]
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
    public async Task<UserAccountResponse> Register([FromQuery] RegisterUserAccountRequest request)
    {
        var model = _mapper.Map<RegisterUserAccountModel>(request);

        var user = await _userAccountService.Create(model);

        var response = _mapper.Map<UserAccountResponse>(user);

        return response;
    }
}
