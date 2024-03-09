using MeetingOrganizer.Api.Configuration;
using MeetingOrganizer.Context.Setup;
using MeetingOrganizer.Context;
using MeetingOrganizer.Services.Settings.Settings;
using MeetingOrganizer.Settings;
using MeetingOrganizer.Api;
using MeetingOrganizer.Context.Seeder.Seeds;
using MeetingOrganizer.Services.Logger;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var identitySettings = Settings.Load<IdentitySettings>("Identity");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(mainSettings, logSettings);

var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppCors();

services.AddAppHealthChecks();

services.AddAppVersioning();

services.AddAppSwagger(mainSettings, swaggerSettings);

services.AddAppAutoMappers();

services.AddAppValidator();

services.AddAppControllerAndViews();

services.RegisterAppServices();


var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();

app.UseAppSwagger();

app.UseSwaggerUI();

app.UseAppControllerAndViews();

DbInitializer.Execute(app.Services);

DbSeeder.Execute(app.Services);

app.Run();
