﻿using AutoMapper;
using MeetingOrganizer.Common.Extensions;
using MeetingOrganizer.Common.Files;
using MeetingOrganizer.Services.Meetings;

namespace MeetingOrganizer.Api.Controllers.Meetings;

public class UpdateRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Date { get; set; }
    public FilePayload? Image { get; set; }
}

public class UpdateRequestProfile : Profile
{
    public UpdateRequestProfile()
    {
        CreateMap<UpdateRequest, UpdateModel>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.ToFileData()));
    }
}
