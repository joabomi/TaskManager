﻿using AutoMapper;
using TaskManager.BlazorUI.Models;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;
using TaskManager.BlazorUI.Models.WorkTasks;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<WorkTaskStatusTypeDto, WorkTaskStatusTypeVM>().ReverseMap();
        CreateMap<WorkTaskStatusTypeDetailsDto, WorkTaskStatusTypeVM>().ReverseMap();
        CreateMap<CreateWorkTaskStatusTypeCommand, WorkTaskStatusTypeVM>().ReverseMap();
        CreateMap<UpdateWorkTaskStatusTypeCommand, WorkTaskStatusTypeVM>().ReverseMap();
        CreateMap<WorkTaskPriorityTypeDto, WorkTaskPriorityTypeVM>().ReverseMap();
        CreateMap<WorkTaskPriorityTypeDetailsDto, WorkTaskPriorityTypeVM>().ReverseMap();
        CreateMap<CreateWorkTaskPriorityTypeCommand, WorkTaskPriorityTypeVM>().ReverseMap();
        CreateMap<UpdateWorkTaskPriorityTypeCommand, WorkTaskPriorityTypeVM>().ReverseMap();
        CreateMap<WorkTaskDetailsDto, WorkTaskVM>().ReverseMap();
        CreateMap<CreateWorkTaskCommand, WorkTaskVM>().ReverseMap();
        CreateMap<UpdateWorkTaskCommand, WorkTaskVM>().ReverseMap();
        CreateMap<WorkTaskUserDto, UserVM>().ReverseMap();
        CreateMap<WorkTaskUserDetailsDto, UserVM>().ReverseMap();

        CreateMap<WorkTaskDto, WorkTaskVM>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.LocalDateTime))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.LocalDateTime)).ReverseMap();
        CreateMap<WorkTaskDetailsDto, WorkTaskVM>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.LocalDateTime))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.LocalDateTime)).ReverseMap();
    }
}
