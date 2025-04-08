using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTask.Commands.UpdateWorkTask;

namespace TaskManager.Application.Features.WorkTask.Commands.CreateWorkTask;

public class CreateWorkTaskCommandHandler : IRequestHandler<CreateWorkTaskCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityTypeRepository;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusTypeRepository;

    public CreateWorkTaskCommandHandler(IMapper mapper, IWorkTaskRepository workTaskRepository,
        IWorkTaskPriorityTypeRepository workTaskPriorityTypeRepository, IWorkTaskStatusTypeRepository workTaskStatusTypeRepository)
    {
        _mapper = mapper;
        _workTaskRepository = workTaskRepository;
        _workTaskPriorityTypeRepository = workTaskPriorityTypeRepository;
        _workTaskStatusTypeRepository = workTaskStatusTypeRepository;
    }

    public async Task<int> Handle(CreateWorkTaskCommand request, CancellationToken cancellationToken)
    {
        //Validate data
        var validator = new CreateWorkTaskCommandValidator(_workTaskPriorityTypeRepository, _workTaskStatusTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid WorkTask request", validationResult);

        //Convert to domain entity object
        var workTaskToCreate = _mapper.Map<Domain.WorkTask>(request);
        //add to database
        await _workTaskRepository.CreateAsync(workTaskToCreate);
        //return record id
        return workTaskToCreate.Id;
    }
}
