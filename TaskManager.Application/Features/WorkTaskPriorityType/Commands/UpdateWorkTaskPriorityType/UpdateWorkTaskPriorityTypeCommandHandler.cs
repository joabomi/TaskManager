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
using TaskManager.Application.Features.WorkTaskStatusType.Commands.UpdateWorkStatusType;

namespace TaskManager.Application.Features.WorkTaskPriorityType.Commands.UpdateWorkTaskPriorityType
{
    internal class UpdateWorkTaskPriorityTypeCommandHandler: IRequestHandler<UpdateWorkTaskPriorityTypeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityRepository;
        private readonly IAppLogger<UpdateWorkTaskPriorityTypeCommandHandler> _logger;

        public UpdateWorkTaskPriorityTypeCommandHandler(IMapper mapper,
            IWorkTaskPriorityTypeRepository workTaskPriorityRepository,
            IAppLogger<UpdateWorkTaskPriorityTypeCommandHandler> logger)
        {
            _mapper = mapper;
            _workTaskPriorityRepository = workTaskPriorityRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateWorkTaskPriorityTypeCommand request, CancellationToken cancellationToken)
        {
            //Validate data incoming

            var validator = new UpdateWorkTaskPriorityTypeCommandValidator(_workTaskPriorityRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(WorkTaskPriorityType), request.Id);
                throw new BadRequestException("Invalid WorkTask Priority Type", validationResult);
            }

            //Convert to domain entity object
            var workPriorityTypeToUpdate = _mapper.Map<Domain.WorkTaskPriorityType>(request);
            //add to database
            await _workTaskPriorityRepository.UpdateAsync(workPriorityTypeToUpdate);
            //return record id
            return Unit.Value;
        }
    }
}
