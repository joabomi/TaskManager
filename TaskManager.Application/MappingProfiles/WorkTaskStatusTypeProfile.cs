using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Domain;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetWorkTaskStatusTypeDetails;
using System.Runtime.CompilerServices;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.CreateWorkTaskStatusType;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.UpdateWorkTaskStatusType;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.DeleteWorkTaskStatusType;

namespace TaskManager.Application.MappingProfilesh;

public class WorkTaskStatusTypeProfile : Profile
{
    public WorkTaskStatusTypeProfile()
    {
        CreateMap<WorkTaskStatusTypeDto, WorkTaskStatusType>().ReverseMap();
        CreateMap<WorkTaskStatusType, WorkTaskStatusTypeDetailsDto>();
        CreateMap<CreateWorkTaskStatusTypeCommand, WorkTaskStatusType>();
        CreateMap<UpdateWorkTaskStatusTypeCommand, WorkTaskStatusType>();
        CreateMap<DeleteWorkTaskStatusTypeCommand, WorkTaskStatusType>();
    }
}
