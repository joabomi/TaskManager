using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Domain.Common;

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

    public async Task<List<WorkTaskDto>> Handle(GetAllWorkTasksQuery query, CancellationToken cancellationToken)
    {
        //query the database
        PagedResult<Domain.WorkTask> workTasks = new PagedResult<Domain.WorkTask>();
        if (_userService.IsLoggedUser)
        {
            workTasks = await _workTaskRepository.GetWorkTasksWithDetails(_userService.UserId, query);
        }
        else if (_userService.IsLoggedAdmin)
        {
            workTasks = await _workTaskRepository.GetWorkTasksWithDetails(query);
        }

        //convert data to DTO
        var ret_val = _mapper.Map<List<WorkTaskDto>>(workTasks.Items);

        //return list DTO objects
        _logger.LogInformation("Work Tasks were retrieved successfully");
        return ret_val;
    }
}
