using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;

public class GetAllWorkTaskStatusTypesQueryHandler : IRequestHandler<GetAllWorkTaskStatusTypesQuery, List<WorkTaskStatusTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusRepository;
    private readonly IAppLogger<GetAllWorkTaskStatusTypesQueryHandler> _logger;

    public GetAllWorkTaskStatusTypesQueryHandler(IMapper mapper,
        IWorkTaskStatusTypeRepository workTaskStatusRepository,
        IAppLogger<GetAllWorkTaskStatusTypesQueryHandler> logger)
    {
        _mapper = mapper;
        _workTaskStatusRepository = workTaskStatusRepository;
        _logger = logger;
    }

    public async Task<List<WorkTaskStatusTypeDto>> Handle(GetAllWorkTaskStatusTypesQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var workTaskStatusTypes = await _workTaskStatusRepository.GetPagedAsync(request);
        //convert data to DTO
        var ret_val = _mapper.Map<List<WorkTaskStatusTypeDto>>(workTaskStatusTypes.Items);
        //return list DTO objects
        _logger.LogInformation("WorkTask Status Types were retrieved successfully");
        return ret_val;
    }
}
