using AutoMapper;
using TaskManager.Domain;
using TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;
using TaskManager.Application.Features.WorkTask.Queries.GetWorkTaskDetails;
using TaskManager.Application.Features.WorkTask.Commands.CreateWorkTask;
using TaskManager.Application.Features.WorkTask.Commands.UpdateWorkTask;
using TaskManager.Application.Features.WorkTask.Commands.DeleteWorkTask;

namespace TaskManager.Application.MappingProfilesh;

public class WorkTaskProfile : Profile
{
    public WorkTaskProfile()
    {
        CreateMap<WorkTaskDto, WorkTask>().ReverseMap();
        CreateMap<WorkTask, WorkTaskDetailsDto>();
        CreateMap<CreateWorkTaskCommand, WorkTask>();
        CreateMap<UpdateWorkTaskCommand, WorkTask>();
    }
}
