using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskPriority.Queries.GetWorkTaskPriorityDetails;
using TaskManager.Domain;

namespace TaskManager.Application.Features.WorkTaskStatus.Queries.GetAllWorkTaskStatusTypes;

public class GetWorkTaskPriorityTypeDetailsHandler : IRequestHandler<GetWorkTaskPriorityTypeDetailsQuery, WorkTaskPriorityTypeDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityTypeRepository;

    public GetWorkTaskPriorityTypeDetailsHandler(IMapper mapper, IWorkTaskPriorityTypeRepository workTaskPriorityTypeRepository)
    {
        _mapper = mapper;
        _workTaskPriorityTypeRepository = workTaskPriorityTypeRepository;
    }

    public async Task<WorkTaskPriorityTypeDetailsDto> Handle(GetWorkTaskPriorityTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var workTaskPriorityTypeDetails = await _workTaskPriorityTypeRepository.GetByIdAsync(request.id);
        //convert data to DTO
        var ret_val = _mapper.Map<WorkTaskPriorityTypeDetailsDto>(workTaskPriorityTypeDetails);
        //return list DTO objects
        return ret_val;
    }
}
