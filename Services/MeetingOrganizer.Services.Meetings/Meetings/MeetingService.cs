using AutoMapper;
using MeetingOrganizer.Common.Exceptions;
using MeetingOrganizer.Common.Validator;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using MeetingOrganizer.Services.Cache;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.Meetings;

public class MeetingService : IMeetingService
{
    private readonly IDbContextFactory<MeetingOrganizerDbContext> _dbContextFactory;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly IModelValidator<CreateModel> _createModelValidator;
    private readonly IModelValidator<UpdateModel> _updateModelValidator;

    public MeetingService(
        IDbContextFactory<MeetingOrganizerDbContext> dbContextFactory,
        IMapper mapper,
        ICacheService cacheService,
        IModelValidator<CreateModel> createModelValidator,
        IModelValidator<UpdateModel> updateModelValidator)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
        _cacheService = cacheService;
        _createModelValidator = createModelValidator;
        _updateModelValidator = updateModelValidator;
    }

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
            .AsQueryable()
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var result = (await meetings.ToListAsync()).Select(meeting => _mapper.Map<MeetingModel>(meeting));
        await _cacheService.Put(cacheKey, result);

        return result;
    }

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

    public async Task<MeetingModel> Create(CreateModel model)
    {
        await _createModelValidator.CheckAsync(model);

        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meeting = _mapper.Map<Meeting>(model);
        await context.Meetings.AddAsync(meeting);
        await context.SaveChangesAsync();

        return _mapper.Map<MeetingModel>(meeting);
    }

    public async Task Update(Guid id, UpdateModel model)
    {
        await _updateModelValidator.CheckAsync(model);

        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meeting = await context.Meetings.Where(x => x.Uid == id).FirstOrDefaultAsync();

        meeting = _mapper.Map(model, meeting);
        
        context.Meetings.Update(meeting);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meeting = await context.Meetings.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (meeting == null)
            throw new ProcessException($"Meeting (ID = {id}) not found.");

        context.Meetings.Remove(meeting);

        await context.SaveChangesAsync();
    }
}
