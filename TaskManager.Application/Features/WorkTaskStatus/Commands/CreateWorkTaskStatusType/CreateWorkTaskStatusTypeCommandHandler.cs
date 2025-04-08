using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskStatus.Commands.CreateWorkTaskStatusType;

public class CreateWorkTaskStatusTypeCommandHandler : IRequestHandler<CreateWorkTaskStatusTypeCommand, int>
{
    private readonly Mapper _mapper;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusRepository;

    public CreateWorkTaskStatusTypeCommandHandler(Mapper mapper, IWorkTaskStatusTypeRepository workTaskStatusRepository)
    {
        _mapper = mapper;
        _workTaskStatusRepository = workTaskStatusRepository;
    }

    public async Task<int> Handle(CreateWorkTaskStatusTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate data incoming
        var validator = new CreateWorkTaskStatusTypeCommandValidator(_workTaskStatusRepository);
        var validatorResult = await validator.ValidateAsync(request);
        //Convert to domain entity object
        var workStatusTypeToCreate = _mapper.Map<Domain.WorkTaskStatusType>(request);
        //add to database
        await _workTaskStatusRepository.CreateAsync(workStatusTypeToCreate);
        //return record id
        return workStatusTypeToCreate.Id;
    }
}
