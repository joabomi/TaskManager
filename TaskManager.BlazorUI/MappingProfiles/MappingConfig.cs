using AutoMapper;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<WorkTaskStatusTypeDto, WorkTaskStatusTypeVM>().ReverseMap();
        CreateMap<CreateWorkTaskStatusTypeCommand, WorkTaskStatusTypeVM>().ReverseMap();
        CreateMap<UpdateWorkTaskStatusTypeCommand, WorkTaskStatusTypeVM>().ReverseMap();
        CreateMap<DeleteWorkTaskStatusTypeCommand, WorkTaskStatusTypeVM>().ReverseMap();
    }
}
