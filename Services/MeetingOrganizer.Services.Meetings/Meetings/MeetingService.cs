﻿using AutoMapper;
using MeetingOrganizer.Common.Exceptions;
using MeetingOrganizer.Common.Extensions;
using MeetingOrganizer.Common.Validator;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using MeetingOrganizer.Services.Cache;
using MeetingOrganizer.Services.Participants;
using MeetingOrganizer.Services.Settings;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.Meetings;

/// <summary>
/// Service for managing meetings.
/// </summary>
public class MeetingService : IMeetingService
{
    private readonly IDbContextFactory<MeetingOrganizerDbContext> _dbContextFactory;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly IModelValidator<CreateModel> _createModelValidator;
    private readonly IModelValidator<UpdateModel> _updateModelValidator;
    private readonly IParticipantService _participantService;
    private readonly MainSettings _mainSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="MeetingService"/> class.
    /// </summary>
    /// <param name="dbContextFactory">The database context factory.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="cacheService">The cache service.</param>
    /// <param name="createModelValidator">The create model validator.</param>
    /// <param name="updateModelValidator">The update model validator.</param>
    /// <param name="participantService">The participant service.</param>
    /// <param name="mainSettings">The main settings.</param>
    public MeetingService(
        IDbContextFactory<MeetingOrganizerDbContext> dbContextFactory,
        IMapper mapper,
        ICacheService cacheService,
        IModelValidator<CreateModel> createModelValidator,
        IModelValidator<UpdateModel> updateModelValidator,
        IParticipantService participantService,
        MainSettings mainSettings)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
        _cacheService = cacheService;
        _createModelValidator = createModelValidator;
        _updateModelValidator = updateModelValidator;
        _participantService = participantService;
        _mainSettings = mainSettings;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<MeetingModel>> GetAll(int offset = 0, int limit = 10)
    {
        var cacheKey = $"Meetings_GetAll_{offset}_{limit}";
        var cachedData = await _cacheService.Get<IEnumerable<MeetingModel>>(cacheKey);

        if (cachedData != null)
            return cachedData;

        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meetings = context
            .Meetings
            .Include(m => m.Participants)
            .Include(m => m.Likes)
            .Include(m => m.Comments)
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var result = (await meetings.ToListAsync()).Select(meeting => _mapper.Map<MeetingModel>(meeting));
        await _cacheService.Put(cacheKey, result);

        return result;
    }

    /// <inheritdoc/>
    public async Task<MeetingModel> GetById(Guid id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meeting = await context
            .Meetings
            .Include(m => m.Participants)
            .Include(m => m.Likes)
            .Include(m => m.Comments)
            .FirstOrDefaultAsync(x => x.Uid.Equals(id));

        var result = _mapper.Map<MeetingModel>(meeting);

        return result;
    }

    /// <inheritdoc/>
    public async Task<MeetingModel> Create(CreateModel model)
    {
        await _createModelValidator.CheckAsync(model);

        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meeting = _mapper.Map<Meeting>(model);

        var fileName = await model.Image.SaveToFile(Path.Combine(_mainSettings.RootDirectory, _mainSettings.FileDirectory));
        meeting.Image = fileName;

        await context.Meetings.AddAsync(meeting);
        await context.SaveChangesAsync();

        await _participantService.Create(new Participants.CreateModel
        {
            MeetingId = meeting.Uid,
            UserId = model.UserId,
            Role = "Admin",
        });

        await context.Entry(meeting).Collection(x => x.Participants).LoadAsync();
        await context.Entry(meeting).Collection(x => x.Likes).LoadAsync();
        await context.Entry(meeting).Collection(x => x.Comments).LoadAsync();

        return _mapper.Map<MeetingModel>(meeting);
    }

    /// <inheritdoc/>
    public async Task Update(Guid id, UpdateModel model)
    {
        await _updateModelValidator.CheckAsync(model);

        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meeting = await context.Meetings.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (meeting == null)
            throw new ProcessException($"Meeting (ID = {id}) not found.");

        var participant = await _participantService.GetByUserAndMeetingId(model.UserId, meeting.Uid);

        if (participant == null || participant.Role != "Admin")
            throw new ProcessException($"User (ID = {model.UserId}) is not allowed to delete this meeting.");

        if (meeting.Date != model.Date)
        {
            if (meeting.Date == null)
                await _participantService.NotifyAllParticipants(meeting.Uid, $"The date of the meeting has been set to {model.Date}");
            else
                await _participantService.NotifyAllParticipants(meeting.Uid, $"The date of the meeting has been changed to {model.Date}");
        }

        var oldImage = meeting.Image;

        meeting = _mapper.Map(model, meeting);

        if (model.Image != null)
        {
            var fileName = await model.Image.SaveToFile(Path.Combine(_mainSettings.RootDirectory, _mainSettings.FileDirectory));
            meeting.Image = fileName;
        }
        else
        {
            meeting.Image = oldImage;
        }

        context.Meetings.Update(meeting);

        await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Delete(Guid id, Guid userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meeting = await context.Meetings.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (meeting == null)
            throw new ProcessException($"Meeting (ID = {id}) not found.");

        var participant = await _participantService.GetByUserAndMeetingId(userId, meeting.Uid);

        if (participant == null || participant.Role != "Admin")
            throw new ProcessException($"User (ID = {userId}) is not allowed to delete this meeting.");

        context.Meetings.Remove(meeting);

        await context.SaveChangesAsync();
    }
}
