using AutoMapper;
using MeetingOrganizer.Common.Exceptions;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.CommentLikes;

internal class CommentLikeService : ICommentLikeService
{
    private readonly IDbContextFactory<MeetingOrganizerDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public CommentLikeService(IDbContextFactory<MeetingOrganizerDbContext> dbContextFactory, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task AddLike(CommentLikeModel model)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var commentLike = _mapper.Map<CommentLike>(model);
        await context.CommentLikes.AddAsync(commentLike);
        await context.SaveChangesAsync();
    }

    public async Task RemoveLike(CommentLikeModel model)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var commentLike = await context
            .CommentLikes
            .Where(x => x.User.Id == model.UserId && x.Comment.Uid == model.CommentId)
            .FirstOrDefaultAsync();

        if (commentLike == null)
            throw new ProcessException($"Comment like not found.");

        context.CommentLikes.Remove(commentLike);

        await context.SaveChangesAsync();
    }

    public async Task<bool> CheckLike(Guid commentId, Guid userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var commentLike = await context
            .CommentLikes
            .Where(x => x.User.Id == userId && x.Comment.Uid == commentId)
            .FirstOrDefaultAsync();

        return commentLike != null;
    }
}
