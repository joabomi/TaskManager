using AutoMapper;
using TaskManager.Domain;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetWorkTaskPriorityDetails;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;

namespace TaskManager.Application.MappingProfilesh;

public class WorkTaskPriorityProfile : Profile
{
    public WorkTaskPriorityProfile()
    {
        CreateMap<WorkTaskPriorityTypeDto, WorkTaskPriorityType>().ReverseMap();
        CreateMap<WorkTaskPriorityType, WorkTaskPriorityTypeDetailsDto>();
    }
}