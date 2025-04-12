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
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IAppLogger<DeleteWorkTaskCommandHandler> _logger;

    public DeleteWorkTaskCommandHandler(IWorkTaskRepository workTaskRepository,
        IAppLogger<DeleteWorkTaskCommandHandler> logger)
    {
        _workTaskRepository = workTaskRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteWorkTaskCommand request, CancellationToken cancellationToken)
    {

        //Validate data incoming
        if (request.Id < 0)
            throw new BadRequestException("Id not provided");

        var workTaskToDelete = await _workTaskRepository.GetByIdAsync(request.Id);
        if (workTaskToDelete == null)
            throw new NotFoundException(nameof(workTaskToDelete), request.Id);

        //delete from database
        await _workTaskRepository.DeleteAsync(workTaskToDelete);
        //return
        _logger.LogInformation("WorkTask successfully deleted (ID: {0})", request.Id);
        return Unit.Value;
    }
}
