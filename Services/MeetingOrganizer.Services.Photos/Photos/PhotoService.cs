using AutoMapper;
using MeetingOrganizer.Common.Exceptions;
using MeetingOrganizer.Common.Extensions;
using MeetingOrganizer.Common.Validator;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using MeetingOrganizer.Services.Participants;
using MeetingOrganizer.Services.Settings;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.Photos;

public class PhotoService : IPhotoService
{
    private readonly IDbContextFactory<MeetingOrganizerDbContext> _dbContextFactory;
    private readonly IMapper _mapper;
    private readonly IModelValidator<CreateModel> _createModelValidator;
    private readonly MainSettings _mainSettings;
    private readonly IParticipantService _participantService;

    public PhotoService(
        IDbContextFactory<MeetingOrganizerDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateModel> createModelValidator,
        MainSettings mainSettings,
        IParticipantService participantService)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
        _createModelValidator = createModelValidator;
        _mainSettings = mainSettings;
        _participantService = participantService;
    }

    public async Task<IEnumerable<PhotoModel>> GetAll()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var photos = context
            .Photos
            .Include(p => p.Meeting);

        var result = (await photos.ToListAsync()).Select(photo => _mapper.Map<PhotoModel>(photo));

        return result;
    }

    public async Task<IEnumerable<PhotoModel>> GetByMeeting(Guid meetingId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var photos = context
            .Photos
            .Include(p => p.Meeting)
            .Where(p=>p.Meeting.Uid.Equals(meetingId));

        var result = (await photos.ToListAsync()).Select(photo => _mapper.Map<PhotoModel>(photo));

        return result;
    }

    public async Task<PhotoModel> GetById(Guid id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var photo = context
            .Photos
            .Include(p => p.Meeting)
            .FirstOrDefaultAsync(p=>p.Uid.Equals(id));

        var result = _mapper.Map<PhotoModel>(photo);

        return result;
    }

    public async Task<PhotoModel> Create(CreateModel model)
    {
        await _createModelValidator.CheckAsync(model);

        using var context = await _dbContextFactory.CreateDbContextAsync();

        var participant = await _participantService.GetByUserAndMeetingId(model.UserId, model.MeetingId);

        if (participant == null)
            throw new ProcessException($"User (ID = {model.UserId}) is not allowed to add photos.");

        var photo = _mapper.Map<Photo>(model);

        var fileName = await model.Image.SaveToFile(Path.Combine(_mainSettings.RootDirectory, _mainSettings.FileDirectory));
        photo.Url = fileName;

        await context.Photos.AddAsync(photo);
        await context.SaveChangesAsync();

        await context.Entry(photo).Reference(x => x.Meeting).LoadAsync();

        return _mapper.Map<PhotoModel>(photo);
    }

    public async Task Delete(Guid id, Guid userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var photo = await context
            .Photos
            .Include(p => p.Meeting)
            .FirstOrDefaultAsync(x => x.Uid == id);

        if (photo == null)
            throw new ProcessException($"Photo (ID = {id}) not found.");

        var participant = await _participantService.GetByUserAndMeetingId(userId, photo.Meeting.Uid);

        if (participant == null || participant.Role == "Participant")
            throw new ProcessException($"User (ID = {userId}) is not allowed to delete this photo.");

        context.Photos.Remove(photo);
        await context.SaveChangesAsync();
    }
}
