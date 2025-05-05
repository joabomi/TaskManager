using AutoMapper;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.CreateWorkTaskStatusType;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.UpdateWorkTaskStatusType;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetWorkTaskStatusTypeDetails;
using TaskManager.Domain;

namespace TaskManager.Application.MappingProfiles;

public class WorkTaskStatusTypeProfile : Profile
{
    public WorkTaskStatusTypeProfile()
    {
        CreateMap<WorkTaskStatusTypeDto, WorkTaskStatusType>().ReverseMap();
        CreateMap<WorkTaskStatusType, WorkTaskStatusTypeDetailsDto>();
        CreateMap<CreateWorkTaskStatusTypeCommand, WorkTaskStatusType>();
        CreateMap<UpdateWorkTaskStatusTypeCommand, WorkTaskStatusType>();
    }
}
