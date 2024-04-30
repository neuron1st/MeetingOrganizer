namespace MeetingOrganizer.Api.Configuration;

using MeetingOrganizer.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;

/// <summary>
/// Configuration class for controllers and views.
/// </summary>
public static class ControllerAndViewsConfiguration
{
    /// <summary>
    /// Configures controllers and views.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddAppControllerAndViews(this IServiceCollection services)
    {
        services
            .AddRazorPages()
            .AddNewtonsoftJson(options => options.SerializerSettings.SetDefaultSettings())
            ;

        services
            .AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.SetDefaultSettings())
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                    new BadRequestObjectResult(context.ModelState.ToErrorResponse());
            });

        return services;
    }

    /// <summary>
    /// Configures controller and view middleware.
    /// </summary>
    /// <param name="app">The <see cref="IEndpointRouteBuilder"/> instance.</param>
    /// <returns>The modified <see cref="IEndpointRouteBuilder"/> instance.</returns>
    public static IEndpointRouteBuilder UseAppControllerAndViews(this IEndpointRouteBuilder app)
    {
        app.MapRazorPages();
        app.MapControllers();

        return app;
    }
}
