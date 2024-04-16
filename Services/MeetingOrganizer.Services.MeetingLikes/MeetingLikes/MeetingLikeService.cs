using AutoMapper;
using MeetingOrganizer.Common.Exceptions;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingOrganizer.Services.MeetingLikes;

public class MeetingLikeService : IMeetingLikeService
{
    private readonly IDbContextFactory<MeetingOrganizerDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public MeetingLikeService(IDbContextFactory<MeetingOrganizerDbContext> dbContextFactory, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task AddLike(MeetingLikeModel model)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meetingLike = _mapper.Map<MeetingLike>(model);
        await context.MeetingLikes.AddAsync(meetingLike);
        await context.SaveChangesAsync();
    }

    public async Task RemoveLike(MeetingLikeModel model)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var meeting = await context
            .MeetingLikes
            .Where(x => x.User.Id == model.UserId && x.Meeting.Uid == model.MeetingId)
            .FirstOrDefaultAsync();

        if (meeting == null)
            throw new ProcessException($"Comment like not found.");

        context.MeetingLikes.Remove(meeting);

        await context.SaveChangesAsync();
    }
}
