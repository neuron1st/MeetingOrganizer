using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Context;
using MeetingOrganizer.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingOrganizer.Services.CommentLikes;

public class CommentLikeModel
{
    public Guid UserId { get; set; }
    public Guid CommentId { get; set; }
}

public class CommentLikeModelProfile : Profile
{
    public CommentLikeModelProfile()
    {
        CreateMap<CommentLikeModel, CommentLike>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CommentId, opt => opt.Ignore())
            .AfterMap<CreateModelActions>();
    }

    public class CreateModelActions : IMappingAction<CommentLikeModel, CommentLike>
    {
        private readonly IDbContextFactory<MeetingOrganizerDbContext> contextFactory;

        public CreateModelActions(IDbContextFactory<MeetingOrganizerDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CommentLikeModel source, CommentLike destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var user = db.Users.FirstOrDefault(x => x.Id == source.UserId);

            var comment = db.Comments.FirstOrDefault(x => x.Uid == source.CommentId);

            destination.UserId = user.EntryId;

            destination.CommentId = comment.Id;
        }
    }
}

public class CommentLikeModelValidator : AbstractValidator<CommentLikeModel>
{
    public CommentLikeModelValidator(IDbContextFactory<MeetingOrganizerDbContext> contextFactory)
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User is required")
            .Must(id =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Users.Any(a => a.Id == id);
                return found;
            }).WithMessage("User not found");

        RuleFor(x => x.CommentId)
            .NotEmpty().WithMessage("Comment is required")
            .Must(id =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Comments.Any(a => a.Uid == id);
                return found;
            }).WithMessage("Comment not found");
    }
}
