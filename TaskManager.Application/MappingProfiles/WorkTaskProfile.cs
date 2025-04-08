using AutoMapper;
using TaskManager.Domain;
using TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;
using TaskManager.Application.Features.WorkTask.Queries.GetWorkTaskDetails;

namespace TaskManager.Application.MappingProfilesh;

public class WorkTaskProfile : Profile
{
    public WorkTaskProfile()
    {
        CreateMap<WorkTaskDto, WorkTask>().ReverseMap();
        CreateMap<WorkTask, WorkTaskDetailsDto>();
    }
}
