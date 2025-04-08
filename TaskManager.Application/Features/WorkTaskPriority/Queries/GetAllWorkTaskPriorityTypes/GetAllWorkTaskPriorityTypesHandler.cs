using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskPriority.Queries.GetAllWorkTaskPriorityTypes;

public class GetAllWorkTaskPriorityTypesHandler : IRequestHandler<GetAllWorkTaskPriorityTypesQuery, List<WorkTaskPriorityTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityRepository;

    public GetAllWorkTaskPriorityTypesHandler(IMapper mapper, IWorkTaskPriorityTypeRepository workTaskPriorityRepository)
    {
        _mapper = mapper;
        _workTaskPriorityRepository = workTaskPriorityRepository;
    }

    public async Task<List<WorkTaskPriorityTypeDto>> Handle(GetAllWorkTaskPriorityTypesQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var workTaskPriorityTypes = await _workTaskPriorityRepository.GetAsync();
        //convert data to DTO
        var ret_val = _mapper.Map<List<WorkTaskPriorityTypeDto>>(workTaskPriorityTypes);
        //return list DTO objects
        return ret_val;
    }
}
