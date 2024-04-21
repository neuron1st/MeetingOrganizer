using AutoMapper;
using FluentValidation;
using MeetingOrganizer.Common.Helpers;
using MeetingOrganizer.Context.Entities;

namespace MeetingOrganizer.Services.Participants;

public class UpdateModel
{
    public string Role { get; set; }
    public Guid userId { get; set; }
}

public class UpdateModelProfile : Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateModel, Participant>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<Role>(src.Role)));
    }
}

public class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator()
    {
        RuleFor(x => x.Role).Must(EnumValidatorHelper.IsValidEnum<Role>).WithMessage("Invalid role.");
    }
}