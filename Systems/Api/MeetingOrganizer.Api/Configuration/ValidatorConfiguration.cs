using FluentValidation.AspNetCore;
using MeetingOrganizer.Common.Helpers;
using MeetingOrganizer.Common.Validator;

namespace MeetingOrganizer.Api.Configuration;

public static class ValidatorConfiguration
{
    public static IServiceCollection AddAppValidator(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(opt => { opt.DisableDataAnnotationsValidation = true; });

        ValidatorsRegisterHelper.Register(services);

        services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return services;
    }
}
