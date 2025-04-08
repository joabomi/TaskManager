using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTask.Commands.DeleteWorkTask;

public class DeleteWorkTaskCommandHandler : IRequestHandler<DeleteWorkTaskCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskRepository _workTaskRepository;

    public DeleteWorkTaskCommandHandler(IMapper mapper, IWorkTaskRepository workTaskRepository)
    {
        _mapper = mapper;
        _workTaskRepository = workTaskRepository;
    }

    public async Task<Unit> Handle(DeleteWorkTaskCommand request, CancellationToken cancellationToken)
    {

        //Validate data incoming

        //Convert to domain entity object
        var workTaskToDelete = _mapper.Map<Domain.WorkTask>(request);
        //add to database
        await _workTaskRepository.DeleteAsync(workTaskToDelete);
        //return record id
        return Unit.Value;
    }
}
