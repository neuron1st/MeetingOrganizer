using AutoMapper;
using MeetingOrganizer.Common.Exceptions;
using MeetingOrganizer.Common.Validator;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using MeetingOrganizer.Services.Actions;
using MeetingOrganizer.Services.EmailSender;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.Participants;

public class ParticipantService : IParticipantService
{
    private readonly IDbContextFactory<MeetingOrganizerDbContext> _dbContextFactory;
    private readonly IMapper _mapper;
    private readonly IModelValidator<CreateModel> _createModelValidator;
    private readonly IModelValidator<UpdateModel> _updateModelValidator;
    private readonly IAction _action;

    public ParticipantService(
        IDbContextFactory<MeetingOrganizerDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateModel> createModelValidator,
        IModelValidator<UpdateModel> updateModelValidator)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
        _createModelValidator = createModelValidator;
        _updateModelValidator = updateModelValidator;
    }

    public async Task<IEnumerable<ParticipantModel>> GetAllByMeetingId(Guid meetingId, int offset = 0, int limit = 10)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var participants = context
            .Participants
            .Include(x => x.Meeting)
            .Include(x => x.User)
            .Where(x => x.Meeting.Uid == meetingId)
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var result = (await participants.ToListAsync()).Select(participant => _mapper.Map<ParticipantModel>(participant));

        return result;
    }

    public async Task<IEnumerable<ParticipantModel>> GetAllByUserId(Guid userId, int offset = 0, int limit = 10)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var participants = context
            .Participants
            .Include(x => x.Meeting)
            .Include(x => x.User)
            .Where(x => x.User.Id == userId)
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var result = (await participants.ToListAsync()).Select(participant => _mapper.Map<ParticipantModel>(participant));

        return result;
    }

    public async Task<ParticipantModel> GetByUserAndMeetingId(Guid userId, Guid meetingId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var participant = await context
            .Participants
            .Include(x => x.Meeting)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.User.Id == userId && x.Meeting.Uid == meetingId);

        var result = _mapper.Map<ParticipantModel>(participant);

        return result;
    }

    public async Task<ParticipantModel> GetById(Guid id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var participant = await context
            .Participants
            .Include(x => x.Meeting)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Uid.Equals(id));

        var result = _mapper.Map<ParticipantModel>(participant);

        return result;
    }

    public async Task<ParticipantModel> Create(CreateModel model)
    {
        await _createModelValidator.CheckAsync(model);

        using var context = await _dbContextFactory.CreateDbContextAsync();

        var participant = _mapper.Map<Participant>(model);
        await context.Participants.AddAsync(participant);
        await context.SaveChangesAsync();

        await context.Entry(participant).Reference(x => x.Meeting).LoadAsync();
        await context.Entry(participant).Reference(x => x.User).LoadAsync();

        return _mapper.Map<ParticipantModel>(participant);
    }

    public async Task Update(Guid id, UpdateModel model)
    {
        await _updateModelValidator.CheckAsync(model);

        using var context = await _dbContextFactory.CreateDbContextAsync();

        var participant = await context.Participants.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (participant == null)
            throw new ProcessException($"Participant (ID = {id}) not found.");

        var user = await context
            .Participants
            .Include(x => x.Meeting)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.User.Id == model.userId && x.Meeting.Uid == participant.Meeting.Uid);

        if (participant == null || (user.Role != Role.Admin && user.User.Id != participant.User.Id))
            throw new ProcessException($"User (ID = {model.userId}) is not allowed to update this participant.");

        participant = _mapper.Map(model, participant);

        context.Participants.Update(participant);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var participant = await context.Participants.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (participant == null)
            throw new ProcessException($"Participant (ID = {id}) not found.");

        context.Participants.Remove(participant);

        await context.SaveChangesAsync();
    }

    public async Task NotifyAllParticipants(Guid meetingId, string message)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var participants = await context
            .Participants
            .Where(x => x.Meeting.Uid == meetingId)
            .ToListAsync();

        if (!participants.Any())
            return;

        foreach (var email in participants
            .Select(p => new EmailModel
            {
                Email = p.User.Email,
                Subject = $"{p.Meeting.Title} notification",
                Message = message

            }))
        {
            await _action.SendEmailAsync(email);
        }
    }
}