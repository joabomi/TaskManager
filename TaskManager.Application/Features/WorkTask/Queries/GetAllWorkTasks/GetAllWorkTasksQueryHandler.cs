using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;

namespace TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;

public class GetAllWorkTasksQueryHandler : IRequestHandler<GetAllWorkTasksQuery, List<WorkTaskDto>>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IAppLogger<GetAllWorkTasksQueryHandler> _logger;

    public GetAllWorkTasksQueryHandler(IMapper mapper,
        IWorkTaskRepository workTaskRepository,
        IAppLogger<GetAllWorkTasksQueryHandler> logger)
    {
        _mapper = mapper;
        _workTaskRepository = workTaskRepository;
        _logger = logger;
    }

    public async Task<List<WorkTaskDto>> Handle(GetAllWorkTasksQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var workTasks = await _workTaskRepository.GetWorkTasksWithDetails();
        //convert data to DTO
        var ret_val = _mapper.Map<List<WorkTaskDto>>(workTasks);
        //return list DTO objects
        _logger.LogInformation("WorkTasks were retrieved successfully");
        return ret_val;
    }
}
