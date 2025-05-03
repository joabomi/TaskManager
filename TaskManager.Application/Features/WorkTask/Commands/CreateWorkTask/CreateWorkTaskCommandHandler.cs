using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Email;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Models.Email;

namespace TaskManager.Application.Features.WorkTask.Commands.CreateWorkTask;

public class CreateWorkTaskCommandHandler : IRequestHandler<CreateWorkTaskCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityTypeRepository;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusTypeRepository;
    private readonly IUserService _userService;
    private readonly IAppLogger<CreateWorkTaskCommandHandler> _logger;
    private readonly IEmailSender _emailSender;

    public CreateWorkTaskCommandHandler(IMapper mapper,
        IWorkTaskRepository workTaskRepository,
        IWorkTaskPriorityTypeRepository workTaskPriorityTypeRepository,
        IWorkTaskStatusTypeRepository workTaskStatusTypeRepository,
        IUserService userService,
        IAppLogger<CreateWorkTaskCommandHandler> logger,
        IEmailSender emailSender)
    {
        _mapper = mapper;
        _workTaskRepository = workTaskRepository;
        _workTaskPriorityTypeRepository = workTaskPriorityTypeRepository;
        _workTaskStatusTypeRepository = workTaskStatusTypeRepository;
        _userService = userService;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task<int> Handle(CreateWorkTaskCommand request, CancellationToken cancellationToken)
    {
        //Validate data
        var validator = new CreateWorkTaskCommandValidator(_workTaskPriorityTypeRepository, _workTaskStatusTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0}", nameof(WorkTask));
            throw new BadRequestException("Invalid WorkTask request", validationResult);
        }

        //Convert to domain entity object
        var workTaskToCreate = _mapper.Map<Domain.WorkTask>(request);
        //add to database
        var result = await _workTaskRepository.CreateAsync(workTaskToCreate);
        //return record id
        if (result != 0)
        {
            _logger.LogInformation("WorkTask successfully created (ID: {0})", workTaskToCreate.Id);
            try
            {
                var assignedUser = await _userService.GetUser(workTaskToCreate.AssignedPersonId);
                await _emailSender.SendEmail(new Email
                {
                    To = assignedUser.Email,
                    Subject = $"New WorkTask Assigned - ",
                    Body = $"A new WorkTask has been assigned to you. Task Name: {workTaskToCreate.Name}"
                });
                _logger.LogInformation("Email sent to assigned user {0}", assignedUser.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error, unexpected exception was thrown trying to send email to assigned user {0}", workTaskToCreate.AssignedPersonId, ex.Message);
            }
        }
        return workTaskToCreate.Id;
    }
}
