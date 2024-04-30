using FluentValidation.AspNetCore;
using MeetingOrganizer.Common.Helpers;
using MeetingOrganizer.Common.Validator;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingOrganizer.Api.Configuration;

/// <summary>
/// Configuration class for validators.
/// </summary>
public static class ValidatorConfiguration
{
    /// <summary>
    /// Configures application validators.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddAppValidator(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(opt => { opt.DisableDataAnnotationsValidation = true; });

        ValidatorsRegisterHelper.Register(services);

        services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return services;
    }
}
