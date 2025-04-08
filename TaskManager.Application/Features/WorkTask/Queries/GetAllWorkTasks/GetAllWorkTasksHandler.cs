using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskStatus.Queries.GetAllWorkTaskStatusTypes;

namespace TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;

public class GetAllWorkTasksHandler : IRequestHandler<GetAllWorkTasksQuery, List<WorkTaskDto>>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskRepository _workTaskRepository;

    public GetAllWorkTasksHandler(IMapper mapper, IWorkTaskRepository workTaskRepository)
    {
        _mapper = mapper;
        _workTaskRepository = workTaskRepository;
    }

    public async Task<List<WorkTaskDto>> Handle(GetAllWorkTasksQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var workTasks = await _workTaskRepository.GetWorkTasksWithDetails();
        //convert data to DTO
        var ret_val = _mapper.Map<List<WorkTaskDto>>(workTasks);
        //return list DTO objects
        return ret_val;
    }
}
