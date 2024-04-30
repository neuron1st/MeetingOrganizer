namespace MeetingOrganizer.Services.UserAccount;

using AutoMapper;
using MeetingOrganizer.Common.Exceptions;
using MeetingOrganizer.Common.Validator;
using MeetingOrganizer.Context.Entities;
using MeetingOrganizer.Services.Actions;
using MeetingOrganizer.Services.EmailSender;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Service for managing user accounts.
/// </summary>
public class UserAccountService : IUserAccountService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IModelValidator<RegisterUserAccountModel> _registerUserAccountModelValidator;
    private readonly IAction _action;

    public UserAccountService(
        IMapper mapper,
        UserManager<User> userManager,
        IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator,
        IAction action)
    {
        _mapper = mapper;
        _userManager = userManager;
        _registerUserAccountModelValidator = registerUserAccountModelValidator;
        _action = action;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<bool> IsEmpty()
    {
        return !(await _userManager.Users.AnyAsync());
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<UserAccountModel> GetById(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new ProcessException("User not found");
        }
        return _mapper.Map<UserAccountModel>(user);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
    {
        _registerUserAccountModelValidator.Check(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new ProcessException("401", $"User account with email {model.Email} already exist.");

        user = new User()
        {
            Status = UserStatus.Active,
            FullName = model.Name,
            UserName = model.Email,
            Email = model.Email,
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException("401", $"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");

        // TODO: make confirmation link instead of token
        await SendConfirmationLinkAsync(model.Email);

        return _mapper.Map<UserAccountModel>(user);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task SendConfirmationLinkAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new ProcessException($"The user with email {email} was not found.");

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var emailModel = new EmailModel
        {
            Email = email,
            Subject = "Confirm your account",
            //Message = $"<a href=\"{confirmationLink}\">Click</a> to confirm email."
            Message = $"Confirmation token = {token}",
        };

        await _action.SendEmailAsync(emailModel);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task ConfirmEmail(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new ProcessException($"The user with email {email} was not found.");

        if (user.EmailConfirmed) return;

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
            throw new ProcessException("Couldn't change password.");

    }
}