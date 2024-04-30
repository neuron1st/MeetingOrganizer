using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Context.Entities;

namespace MeetingOrganizer.Services.Comments;

/// <summary>
/// Represents a model for updating a comment.
/// </summary>
public class UpdateModel
{
    public string Text { get; set; }
}

/// <summary>
/// Profile for mapping update model to comment entity.
/// </summary>
public class UpdateModelProfile : Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateModel, Comment>();
    }
}

/// <summary>
/// Validator for the update comment model.
/// </summary>
public class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Text is required")
            .MaximumLength(2000).WithMessage("Text is too long");
    }
}
