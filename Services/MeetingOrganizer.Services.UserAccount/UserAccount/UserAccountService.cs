namespace MeetingOrganizer.Services.UserAccount;

using AutoMapper;
using MeetingOrganizer.Common.Exceptions;
using MeetingOrganizer.Common.Responses;
using MeetingOrganizer.Common.Validator;
using MeetingOrganizer.Context.Entities;
using MeetingOrganizer.Services.Actions;
using MeetingOrganizer.Services.EmailSender;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

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

    public async Task<bool> IsEmpty()
    {
        return !(await _userManager.Users.AnyAsync());
    }

    public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
    {
        _registerUserAccountModelValidator.Check(model);

        // Find user by email
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new ProcessException("401", $"User account with email {model.Email} already exist.");

        // Create user account
        user = new User()
        {
            Status = UserStatus.Active,
            FullName = model.Name,
            UserName = model.Email, // login = email
            Email = model.Email,
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException("401", $"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");

        // TODO: make confirmation link instead of token
        await SendConfirmationLinkAsync(model.Email);

        // Returning the created user
        return _mapper.Map<UserAccountModel>(user);
    }

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