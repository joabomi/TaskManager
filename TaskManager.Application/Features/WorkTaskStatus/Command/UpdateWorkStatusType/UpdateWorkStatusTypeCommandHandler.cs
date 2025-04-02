using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskStatus.Command.UpdateWorkStatusType
{
    internal class UpdateWorkStatusTypeCommandHandler: IRequestHandler<UpdateWorkStatusTypeCommand, Unit>
    {
        private readonly Mapper _mapper;
        private readonly IWorkTaskStatusRepository _workTaskStatusRepository;

        public UpdateWorkStatusTypeCommandHandler(Mapper mapper, IWorkTaskStatusRepository workTaskStatusRepository)
        {
            _mapper = mapper;
            _workTaskStatusRepository = workTaskStatusRepository;
        }

        public async Task<Unit> Handle(UpdateWorkStatusTypeCommand request, CancellationToken cancellationToken)
        {
            //Validate data incoming

            //Convert to domain entity object
            var workStatusTypeToUpdate = _mapper.Map<Domain.WorkTaskStatusType>(request);
            //add to database
            await _workTaskStatusRepository.UpdateAsync(workStatusTypeToUpdate);
            //return record id
            return Unit.Value;
        }
    }
}
