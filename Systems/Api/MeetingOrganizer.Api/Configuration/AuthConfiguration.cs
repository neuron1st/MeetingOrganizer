﻿using MeetingOrganizer.Common.Security;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using MeetingOrganizer.Services.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace MeetingOrganizer.Api.Configuration;

/// <summary>
/// Configuration class for authentication and authorization.
/// </summary>
public static class AuthConfiguration
{
    /// <summary>
    /// Configures application authentication and authorization.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
    /// <param name="settings">The identity settings.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddAppAuth(this IServiceCollection services, IdentitySettings settings)
    {
        IdentityModelEventSource.ShowPII = true;

        services
            .AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequiredLength = 0;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<MeetingOrganizerDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = settings.Url.StartsWith("https://");
                options.Authority = settings.Url;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Audience = "api";
            });


        services.AddAuthorization(options =>
        {
            options.AddPolicy(AppScopes.MeetingsRead, policy => policy.RequireClaim("scope", AppScopes.MeetingsRead));
            options.AddPolicy(AppScopes.MeetingsWrite, policy => policy.RequireClaim("scope", AppScopes.MeetingsWrite));
            options.AddPolicy(AppScopes.CommentsRead, policy => policy.RequireClaim("scope", AppScopes.CommentsRead));
            options.AddPolicy(AppScopes.CommentsWrite, policy => policy.RequireClaim("scope", AppScopes.CommentsWrite));
            options.AddPolicy(AppScopes.ParticipantsRead, policy => policy.RequireClaim("scope", AppScopes.ParticipantsRead));
            options.AddPolicy(AppScopes.ParticipantsWrite, policy => policy.RequireClaim("scope", AppScopes.ParticipantsWrite));
        });

        return services;
    }

    /// <summary>
    /// Configures application authentication and authorization middleware.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The modified <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseAppAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}
