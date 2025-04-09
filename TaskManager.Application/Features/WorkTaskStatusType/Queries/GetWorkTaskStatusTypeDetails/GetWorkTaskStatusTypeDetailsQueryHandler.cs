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

namespace TaskManager.Application.Features.WorkTaskStatusType.Queries.GetWorkTaskStatusTypeDetails;

public class GetWorkTaskStatusTypeDetailsQueryHandler : IRequestHandler<GetWorkTaskStatusTypeDetailsQuery, WorkTaskStatusTypeDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskPriorityTypeRepository _workTaskStatusRepository;
    private readonly IAppLogger<GetWorkTaskStatusTypeDetailsQueryHandler> _logger;

    public GetWorkTaskStatusTypeDetailsQueryHandler(IMapper mapper,
        IWorkTaskPriorityTypeRepository workTaskStatusRepository,
        IAppLogger<GetWorkTaskStatusTypeDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _workTaskStatusRepository = workTaskStatusRepository;
        _logger = logger;
    }

    public async Task<WorkTaskStatusTypeDetailsDto> Handle(GetWorkTaskStatusTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var workTaskStatusTypeDetails = await _workTaskStatusRepository.GetByIdAsync(request.Id);
        //convert data to DTO
        var ret_val = _mapper.Map<WorkTaskStatusTypeDetailsDto>(workTaskStatusTypeDetails);
        //return list DTO objects
        _logger.LogInformation("WorkTask Status Type Details successfully retrieved");
        return ret_val;
    }
}
