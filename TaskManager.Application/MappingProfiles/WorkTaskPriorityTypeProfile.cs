using AutoMapper;
using TaskManager.Application.Features.WorkTaskPriorityType.Commands.CreateWorkTaskPriorityType;
using TaskManager.Application.Features.WorkTaskPriorityType.Commands.UpdateWorkTaskPriorityType;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetWorkTaskPriorityDetails;
using TaskManager.Domain;

namespace TaskManager.Application.MappingProfiles;

public class WorkTaskPriorityTypeProfile : Profile
{
    public WorkTaskPriorityTypeProfile()
    {
        CreateMap<WorkTaskPriorityTypeDto, WorkTaskPriorityType>().ReverseMap();
        CreateMap<WorkTaskPriorityType, WorkTaskPriorityTypeDetailsDto>();
        CreateMap<CreateWorkTaskPriorityTypeCommand, WorkTaskPriorityType>();
        CreateMap<UpdateWorkTaskPriorityTypeCommand, WorkTaskPriorityType>();
    }
}