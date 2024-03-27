using MeetingOrganizer.Worker;
using MeetingOrganizer.Worker.Configuration;
using MeetingOrganizer.Context;
using MeetingOrganizer.Services.Settings;
using MeetingOrganizer.Settings;

var logSettings = Settings.Load<LogSettings>("Log");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddAppLogger(logSettings);

// Configure services
var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();

services.RegisterAppServices();


// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseAppHealthChecks();


// Start task executor

app.Services.GetRequiredService<ITaskExecutor>().Start();


// Run app

app.Run();