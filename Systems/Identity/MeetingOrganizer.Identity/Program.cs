using MeetingOrganizer.Identity;
using MeetingOrganizer.Identity.Configuration;
using MeetingOrganizer.Services.Settings;
using MeetingOrganizer.Settings;
using MeetingOrganizer.Context;

var logSettings = Settings.Load<LogSettings>("Log");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(logSettings);

// Configure services
var services = builder.Services;

services.AddAppCors();

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.RegisterAppServices();

services.AddIS4();


// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseAppCors();

app.UseIS4();


// Run app

app.Run();
