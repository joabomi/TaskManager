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
using TaskManager.Application.Features.WorkTaskStatusType.Commands.DeleteWorkTaskStatusType;

namespace TaskManager.Application.Features.WorkTaskPriorityType.Commands.DeleteWorkTaskPriorityType;

public class DeleteWorkTaskPriorityTypeCommandHandler : IRequestHandler<DeleteWorkTaskPriorityTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityRepository;
    private readonly IAppLogger<DeleteWorkTaskPriorityTypeCommandHandler> _logger;

    public DeleteWorkTaskPriorityTypeCommandHandler(IMapper mapper,
        IWorkTaskPriorityTypeRepository workTaskPriorityRepository,
        IAppLogger<DeleteWorkTaskPriorityTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _workTaskPriorityRepository = workTaskPriorityRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteWorkTaskPriorityTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate data incoming

        if (request.Id < 0)
            throw new BadRequestException("Id not provided");

        var workTaskPriorityType_exists = await _workTaskPriorityRepository.GetByIdAsync(request.Id);
        if (workTaskPriorityType_exists == null)
            throw new NotFoundException(nameof(workTaskPriorityType_exists), request.Id);

        //Convert to domain entity object
        var workPriorityTypeToDelete = _mapper.Map<Domain.WorkTaskPriorityType>(request);
        //add to database
        await _workTaskPriorityRepository.DeleteAsync(workPriorityTypeToDelete);
        //return record id
        _logger.LogInformation("WorkTask Priority Type successfully deleted (ID: {0})", request.Id);
        return Unit.Value;
    }
}
