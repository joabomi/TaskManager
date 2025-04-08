using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskPriority.Commands.CreateWorkTaskPriorityType;

public class CreateWorkTaskPriorityTypeCommandHandler : IRequestHandler<CreateWorkTaskPriorityTypeCommand, int>
{
    private readonly Mapper _mapper;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityRepository;

    public CreateWorkTaskPriorityTypeCommandHandler(Mapper mapper, IWorkTaskPriorityTypeRepository workTaskPriorityRepository)
    {
        _mapper = mapper;
        _workTaskPriorityRepository = workTaskPriorityRepository;
    }

    public async Task<int> Handle(CreateWorkTaskPriorityTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate data incoming
        var validator = new CreateWorkTaskPriorityTypeCommandValidator(_workTaskPriorityRepository);
        var validatorResult = await validator.ValidateAsync(request);
        //Convert to domain entity object
        var workPriorityTypeToCreate = _mapper.Map<Domain.WorkTaskPriorityType>(request);
        //add to database
        await _workTaskPriorityRepository.CreateAsync(workPriorityTypeToCreate);
        //return record id
        return workPriorityTypeToCreate.Id;
    }
}
