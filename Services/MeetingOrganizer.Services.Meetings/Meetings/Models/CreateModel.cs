using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Common.Files;
using MeetingOrganizer.Context.Entities;

namespace MeetingOrganizer.Services.Meetings;

public class CreateModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public FileData? Image { get; set; }

    public Guid UserId { get; set; }
}

public class CreateModelProfile : Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateModel, Meeting>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToUniversalTime()));
    }
}

public class CreateModelValidator : AbstractValidator<CreateModel>
{
    public CreateModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Meeting title is required")
            .MaximumLength(128).WithMessage("Title is too long");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description is too long");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User is required");
    }
}