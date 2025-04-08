using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskPriority.Commands.UpdateWorkTaskPriorityType
{
    internal class UpdateWorkTaskPriorityTypeCommandHandler: IRequestHandler<UpdateWorkTaskPriorityTypeCommand, Unit>
    {
        private readonly Mapper _mapper;
        private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityRepository;

        public UpdateWorkTaskPriorityTypeCommandHandler(Mapper mapper, IWorkTaskPriorityTypeRepository workTaskPriorityRepository)
        {
            _mapper = mapper;
            _workTaskPriorityRepository = workTaskPriorityRepository;
        }

        public async Task<Unit> Handle(UpdateWorkTaskPriorityTypeCommand request, CancellationToken cancellationToken)
        {
            //Validate data incoming

            //Convert to domain entity object
            var workPriorityTypeToUpdate = _mapper.Map<Domain.WorkTaskPriorityType>(request);
            //add to database
            await _workTaskPriorityRepository.UpdateAsync(workPriorityTypeToUpdate);
            //return record id
            return Unit.Value;
        }
    }
}
