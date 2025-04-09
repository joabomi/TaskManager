using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.DeleteWorkTaskStatusType;

namespace TaskManager.Application.Features.WorkTaskPriorityType.Commands.DeleteWorkTaskPriorityType;

internal class DeleteWorkTaskPriorityTypeCommandHandler : IRequestHandler<DeleteWorkTaskPriorityTypeCommand, Unit>
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
        //Convert to domain entity object
        var workPriorityTypeToDelete = _mapper.Map<Domain.WorkTaskPriorityType>(request);
        //add to database
        await _workTaskPriorityRepository.DeleteAsync(workPriorityTypeToDelete);
        //return record id
        _logger.LogInformation("WorkTask Priority Type successfully deleted (ID: {0})", request.Id);
        return Unit.Value;
    }
}
