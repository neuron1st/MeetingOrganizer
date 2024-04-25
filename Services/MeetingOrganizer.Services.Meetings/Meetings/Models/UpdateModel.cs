using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Common.Files;
using MeetingOrganizer.Context.Entities;

namespace MeetingOrganizer.Services.Meetings;

public class UpdateModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public FileData Image { get; set; }

    public Guid UserId { get; set; }
}

public class UpdateModelProfile : Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateModel, Meeting>();
    }
}

public class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Meeting title is required")
            .MaximumLength(128).WithMessage("Title is too long");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description is too long");
    }
}
