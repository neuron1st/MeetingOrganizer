using AutoMapper;
using MeetingOrganizer.Common.Exceptions;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.MeetingLikes;

/// <summary>
/// Service for managing meeting likes.
/// </summary>
public class MeetingLikeService : IMeetingLikeService
{
    private readonly IDbContextFactory<MeetingOrganizerDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public MeetingLikeService(IDbContextFactory<MeetingOrganizerDbContext> dbContextFactory, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task AddLike(MeetingLikeModel model)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meetingLike = _mapper.Map<MeetingLike>(model);
        await context.MeetingLikes.AddAsync(meetingLike);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task RemoveLike(MeetingLikeModel model)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meetingLike = await context
            .MeetingLikes
            .Where(x => x.User.Id == model.UserId && x.Meeting.Uid == model.MeetingId)
            .FirstOrDefaultAsync();

        if (meetingLike == null)
            throw new ProcessException($"Meeting like not found.");

        context.MeetingLikes.Remove(meetingLike);

        await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<bool> CheckLike(Guid meetingId, Guid userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meetingLike = await context
            .MeetingLikes
            .Where(x => x.User.Id == userId && x.Meeting.Uid == meetingId)
            .FirstOrDefaultAsync();

        return meetingLike != null;
    }
}
