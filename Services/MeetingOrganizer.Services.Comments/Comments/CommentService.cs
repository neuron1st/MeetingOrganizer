using AutoMapper;
using MeetingOrganizer.Common.Exceptions;
using MeetingOrganizer.Common.Validator;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.Comments;

public class CommentService : ICommentService
{
    private readonly IDbContextFactory<MeetingOrganizerDbContext> _dbContextFactory;
    private readonly IMapper _mapper;
    private readonly IModelValidator<CreateModel> _createModelValidator;
    private readonly IModelValidator<UpdateModel> _updateModelValidator;

    public CommentService(
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

    public async Task<IEnumerable<CommentModel>> GetAllByMeetingId(Guid meetingId, int offset = 0, int limit = 10)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var comments = context
            .Comments
            .Include(x => x.Meeting)
            .Include(x => x.User)
            .AsQueryable()
            .Where(x => x.Meeting.Uid == meetingId)
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var result = (await comments.ToListAsync()).Select(comment => _mapper.Map<CommentModel>(comment));

        return result;
    }

    public async Task<IEnumerable<CommentModel>> GetAllByUserId(Guid userId, int offset = 0, int limit = 10)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var comments = context
            .Comments
            .Include(x => x.Meeting)
            .Include(x => x.User)
            .AsQueryable()
            .Where(x => x.User.Id == userId)
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var result = (await comments.ToListAsync()).Select(comment => _mapper.Map<CommentModel>(comment));

        return result;
    }

    public async Task<CommentModel> GetById(Guid id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var comment = await context
            .Comments
            .Include(x => x.Meeting)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Uid.Equals(id));

        var result = _mapper.Map<CommentModel>(comment);

        return result;
    }

    public async Task<CommentModel> Create(CreateModel model)
    {
        await _createModelValidator.CheckAsync(model);

        using var context = await _dbContextFactory.CreateDbContextAsync();

        var comment = _mapper.Map<Comment>(model);
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();

        await context.Entry(comment).Reference(x => x.Meeting).LoadAsync();
        await context.Entry(comment).Reference(x => x.User).LoadAsync();

        return _mapper.Map<CommentModel>(comment);
    }

    public async Task Update(Guid id, UpdateModel model)
    {
        await _updateModelValidator.CheckAsync(model);

        using var context = await _dbContextFactory.CreateDbContextAsync();

        var comment = await context.Comments.Where(x => x.Uid == id).FirstOrDefaultAsync();

        comment = _mapper.Map(model, comment);

        context.Comments.Update(comment);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var comment = await context.Comments.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (comment == null)
            throw new ProcessException($"Comment (ID = {id}) not found.");

        context.Comments.Remove(comment);

        await context.SaveChangesAsync();
    }
}
