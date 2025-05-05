using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;

public class GetAllWorkTasksQueryHandler : IRequestHandler<GetAllWorkTasksQuery, List<WorkTaskDto>>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IAppLogger<GetAllWorkTasksQueryHandler> _logger;
    private readonly IUserService _userService;

    public GetAllWorkTasksQueryHandler(IMapper mapper,
        IWorkTaskRepository workTaskRepository,
        IAppLogger<GetAllWorkTasksQueryHandler> logger,
        IUserService userService)
    {
        _mapper = mapper;
        _workTaskRepository = workTaskRepository;
        _logger = logger;
        _userService = userService;
    }

    public async Task<List<WorkTaskDto>> Handle(GetAllWorkTasksQuery request, CancellationToken cancellationToken)
    {
        var ret_val = new List<WorkTaskDto>();
        var workTasks = await _workTaskRepository.GetWorkTasksWithDetails();
        List<Domain.WorkTask> targetWorkTasks = new List<Domain.WorkTask>();
        //query the database
        if (request.IsLoggedUser)
        {
            targetWorkTasks = workTasks.Where(x => x.AssignedPersonId == _userService.UserId).ToList();
        }
        else if (request.IsLoggedAdmin)
        {
            targetWorkTasks = workTasks;
        }
        //convert data to DTO
        ret_val = _mapper.Map<List<WorkTaskDto>>(targetWorkTasks);
        //return list DTO objects
        _logger.LogInformation("Work Tasks were retrieved successfully");
        return ret_val;
    }
}
