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
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetWorkTaskStatusTypeDetails;

namespace TaskManager.Application.Features.WorkTask.Queries.GetWorkTaskDetails;

public class GetWorkTaskDetailsQueryHandler : IRequestHandler<GetWorkTaskDetailsQuery, WorkTaskDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IAppLogger<GetWorkTaskDetailsQueryHandler> _logger;

    public GetWorkTaskDetailsQueryHandler(IMapper mapper,
        IWorkTaskRepository workTaskRepository,
        IAppLogger<GetWorkTaskDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _workTaskRepository = workTaskRepository;
        _logger = logger;
    }

    public async Task<WorkTaskDetailsDto> Handle(GetWorkTaskDetailsQuery request, CancellationToken cancellationToken)
    {
        //query the database
        if (request.Id < 0)
            throw new BadRequestException("Id not provided");

        var workTaskDetails = await _workTaskRepository.GetByIdAsync(request.Id);
        if (workTaskDetails == null)
            throw new NotFoundException(nameof(workTaskDetails), request.Id);

        //convert data to DTO
        var ret_val = _mapper.Map<WorkTaskDetailsDto>(workTaskDetails);
        //return list DTO objects
        _logger.LogInformation("WorkTask successfully retrieved");
        return ret_val;
    }
}
