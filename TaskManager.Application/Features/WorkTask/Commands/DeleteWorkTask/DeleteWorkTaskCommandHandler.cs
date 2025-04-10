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

namespace TaskManager.Application.Features.WorkTask.Commands.DeleteWorkTask;

public class DeleteWorkTaskCommandHandler : IRequestHandler<DeleteWorkTaskCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IAppLogger<DeleteWorkTaskCommandHandler> _logger;

    public DeleteWorkTaskCommandHandler(IMapper mapper,
        IWorkTaskRepository workTaskRepository,
        IAppLogger<DeleteWorkTaskCommandHandler> logger)
    {
        _mapper = mapper;
        _workTaskRepository = workTaskRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteWorkTaskCommand request, CancellationToken cancellationToken)
    {

        //Validate data incoming
        if (request.Id < 0)
            throw new BadRequestException("Id not provided");

        var workTask_exists = await _workTaskRepository.GetByIdAsync(request.Id);
        if (workTask_exists == null)
            throw new NotFoundException(nameof(workTask_exists), request.Id);

        //Convert to domain entity object
        var workTaskToDelete = _mapper.Map<Domain.WorkTask>(request);
        //add to database
        await _workTaskRepository.DeleteAsync(workTaskToDelete);
        //return
        _logger.LogInformation("WorkTask successfully deleted (ID: {0})", request.Id);
        return Unit.Value;
    }
}
