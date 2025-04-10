using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Domain;

namespace TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetWorkTaskPriorityDetails;

public class GetWorkTaskPriorityTypeDetailsQueryHandler : IRequestHandler<GetWorkTaskPriorityTypeDetailsQuery, WorkTaskPriorityTypeDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityTypeRepository;
    private readonly IAppLogger<GetWorkTaskPriorityTypeDetailsQueryHandler> _logger;

    public GetWorkTaskPriorityTypeDetailsQueryHandler(IMapper mapper,
        IWorkTaskPriorityTypeRepository workTaskPriorityTypeRepository,
        IAppLogger<GetWorkTaskPriorityTypeDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _workTaskPriorityTypeRepository = workTaskPriorityTypeRepository;
        _logger = logger;
    }

    public async Task<WorkTaskPriorityTypeDetailsDto> Handle(GetWorkTaskPriorityTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var workTaskPriorityTypeDetails = await _workTaskPriorityTypeRepository.GetByIdAsync(request.Id);
        if (workTaskPriorityTypeDetails == null)
            throw new NotFoundException(nameof(workTaskPriorityTypeDetails), request.Id);
        //convert data to DTO
        var ret_val = _mapper.Map<WorkTaskPriorityTypeDetailsDto>(workTaskPriorityTypeDetails);
        //return list DTO objects
        _logger.LogInformation("WorkTask Priority Type Details successfully retrieved");
        return ret_val;
    }
}
