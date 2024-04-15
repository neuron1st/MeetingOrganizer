using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Context.Entities;

namespace MeetingOrganizer.Services.Comments;

public class UpdateModel
{
    public string Text { get; set; }
}

public class UpdateModelProfile : Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateModel, Comment>();
    }
}

public class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Text is required")
            .MaximumLength(2000).WithMessage("Text is too long");
    }
}
