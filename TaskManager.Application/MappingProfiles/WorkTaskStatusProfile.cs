using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Domain;
using TaskManager.Application.Features.WorkTaskStatus.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Application.Features.WorkTaskStatus.Queries.GetWorkTaskStatusTypeDetails;

namespace TaskManager.Application.MappingProfilesh;

public class WorkTaskStatusProfile : Profile
{
    public WorkTaskStatusProfile()
    {
        CreateMap<WorkTaskStatusTypeDto, WorkTaskStatusType>().ReverseMap();
        CreateMap<WorkTaskStatusType, WorkTaskStatusTypeDetailsDto>();
    }
}
