using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;

public class GetAllWorkTaskPriorityTypesQueryHandler : IRequestHandler<GetAllWorkTaskPriorityTypesQuery, List<WorkTaskPriorityTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityRepository;
    private readonly IAppLogger<GetAllWorkTaskPriorityTypesQueryHandler> _logger;

    public GetAllWorkTaskPriorityTypesQueryHandler(IMapper mapper,
        IWorkTaskPriorityTypeRepository workTaskPriorityRepository,
        IAppLogger<GetAllWorkTaskPriorityTypesQueryHandler> logger)
    {
        _mapper = mapper;
        _workTaskPriorityRepository = workTaskPriorityRepository;
        _logger = logger;
    }

    public async Task<List<WorkTaskPriorityTypeDto>> Handle(GetAllWorkTaskPriorityTypesQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var workTaskPriorityTypes = await _workTaskPriorityRepository.GetPagedAsync(request);
        //convert data to DTO
        var ret_val = _mapper.Map<List<WorkTaskPriorityTypeDto>>(workTaskPriorityTypes.Items);
        //return list DTO objects
        _logger.LogInformation("Work Task Priority Types were retrieved successfully");
        return ret_val;
    }
}
