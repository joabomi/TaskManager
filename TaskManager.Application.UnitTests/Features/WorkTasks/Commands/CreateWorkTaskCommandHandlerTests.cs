using AutoMapper;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Email;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTask.Commands.CreateWorkTask;
using TaskManager.Application.MappingProfiles;
using TaskManager.Application.Models.Email;
using TaskManager.Application.UnitTests.Mocks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TaskManager.Application.UnitTests.Features.WorkTasks.Commands;

public class CreateWorkTaskCommandHandlerTests
{
    private readonly Mock<IWorkTaskRepository> _workTasksMockRepo;
    private readonly Mock<IWorkTaskStatusTypeRepository> _workTaskStatusTypesMockRepo;
    private readonly Mock<IWorkTaskPriorityTypeRepository> _workTaskPriorityTypesMockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<CreateWorkTaskCommandHandler>> _mockAppLogger;
    private readonly Mock<IUserService> _userServiceMockRepo;
    private readonly Mock<IEmailSender> _emailSenderMock;

    public CreateWorkTaskCommandHandlerTests(ITestOutputHelper outputHelper)
    {
        _workTasksMockRepo = MockWorkTaskRepository.GetMockWorkTaskRepository();
        _workTaskStatusTypesMockRepo = MockWorkTaskStatusTypeRepository.GetMockWorkTaskStatusTypeRepository();
        _workTaskPriorityTypesMockRepo = MockWorkTaskPriorityTypeRepository.GetMockWorkTaskPriorityTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskProfile>();
            c.AddProfile<WorkTaskStatusTypeProfile>();
            c.AddProfile<WorkTaskPriorityTypeProfile>();
            c.AddProfile<WorkTaskUsersProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _userServiceMockRepo = MockUserServiceRepository.GetMockUserServiceRepository();
        _mockAppLogger = new Mock<IAppLogger<CreateWorkTaskCommandHandler>>();
        _emailSenderMock = MockEmailService.GetMockEmailService();
    }

    [Fact]
    public async Task CreateWorkTaskCommand()
    {
        var handler = new CreateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _userServiceMockRepo.Object, _mockAppLogger.Object, _emailSenderMock.Object);

        var inintialItemsCount = _workTasksMockRepo.Object.GetAsync().Result.Count;
        CreateWorkTaskCommand newItem = new CreateWorkTaskCommand
        {
            Name = "Test_Item",
            Description = "Test_Description7",
            StatusId = 5,
            PriorityId = 5,
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2024, 7, 10), 
            AssignedPersonId = "2"
        };

        var result = await handler.Handle(newItem, CancellationToken.None);

        result.ShouldBe(5);
        _workTasksMockRepo.Object.GetAsync().Result.Count.ShouldBe(inintialItemsCount + 1);

        //added item validation
        var addedItem = _workTasksMockRepo.Object.GetAsync().Result.Last();
        addedItem.Id.ShouldBe(5);
        addedItem.Name.ShouldBe(newItem.Name);
        addedItem.Description.ShouldBe(newItem.Description);
        addedItem.StatusId.ShouldBe(newItem.StatusId);
        addedItem.PriorityId.ShouldBe(newItem.PriorityId);
        addedItem.StartDate.ShouldBe(newItem.StartDate);
        addedItem.EndDate.ShouldBe(newItem.EndDate);
        addedItem.AssignedPersonId.ShouldBe(newItem.AssignedPersonId);

        _emailSenderMock.Verify(s => s.SendEmail(It.Is<Email>(email =>
            email.To == "jane@localhost.com"
            )), Times.Once);

    }

    [Fact]
    public async Task CreateWorkTaskCommand_BadName()
    {
        var handler = new CreateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _userServiceMockRepo.Object, _mockAppLogger.Object, _emailSenderMock.Object);
        var command = new CreateWorkTaskCommand
        {
            Description = "Description7",
            StatusId = 5,
            PriorityId = 5,
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2024, 7, 10),
            AssignedPersonId = "2"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        _emailSenderMock.Verify(s => s.SendEmail(It.IsAny<Email>()), Times.Never);

        command.Name = "";
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        _emailSenderMock.Verify(s => s.SendEmail(It.IsAny<Email>()), Times.Never);

        for (int i = 0; i < 51; i++)
        {
            command.Name += "--";
        }
        command.Name.Length.ShouldBeGreaterThan(100);
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        _emailSenderMock.Verify(s => s.SendEmail(It.IsAny<Email>()), Times.Never);
    }

    [Fact]
    public async Task CreateWorkTaskCommand_BadStatus()
    {
        var handler = new CreateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _userServiceMockRepo.Object, _mockAppLogger.Object, _emailSenderMock.Object);
        var command = new CreateWorkTaskCommand
        {
            Name = "WorkTask7",
            Description = "Description7",
            PriorityId = 5,
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2024, 7, 10),
            AssignedPersonId = "1"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        _emailSenderMock.Verify(s => s.SendEmail(It.IsAny<Email>()), Times.Never);

        command.StatusId = 99;
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        _emailSenderMock.Verify(s => s.SendEmail(It.IsAny<Email>()), Times.Never);
    }

    [Fact]
    public async Task CreateWorkTaskCommand_BadPriority()
    {
        var handler = new CreateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _userServiceMockRepo.Object, _mockAppLogger.Object, _emailSenderMock.Object);
        var command = new CreateWorkTaskCommand
        {
            Name = "WorkTask7",
            Description = "Description7",
            StatusId = 5,
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2024, 7, 10),
            AssignedPersonId = "1"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        _emailSenderMock.Verify(s => s.SendEmail(It.IsAny<Email>()), Times.Never);

        command.PriorityId = 99;
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        _emailSenderMock.Verify(s => s.SendEmail(It.IsAny<Email>()), Times.Never);
    }

    [Fact]
    public async Task CreateWorkTaskCommand_BadStartDate()
    {
        var handler = new CreateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _userServiceMockRepo.Object, _mockAppLogger.Object, _emailSenderMock.Object);
        var command = new CreateWorkTaskCommand
        {
            Name = "WorkTask7",
            Description = "Description7",
            StatusId = 5,
            PriorityId = 5,
            EndDate = new DateTime(2024, 7, 10),
            AssignedPersonId = "2"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        _emailSenderMock.Verify(s => s.SendEmail(It.IsAny<Email>()), Times.Never);

        command.StartDate = new DateTime();
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        _emailSenderMock.Verify(s => s.SendEmail(It.IsAny<Email>()), Times.Never);
    }

    [Fact]
    public async Task CreateWorkTaskCommand_BadEndDate()
    {
        var handler = new CreateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _userServiceMockRepo.Object, _mockAppLogger.Object, _emailSenderMock.Object);
        var command = new CreateWorkTaskCommand
        {
            Name = "WorkTask7",
            Description = "Description7",
            StatusId = 5,
            PriorityId = 5,
            StartDate = new DateTime(2024, 7, 1),
            AssignedPersonId = "2"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        _emailSenderMock.Verify(s => s.SendEmail(It.IsAny<Email>()), Times.Never);

        command.EndDate = new DateTime();
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        _emailSenderMock.Verify(s => s.SendEmail(It.IsAny<Email>()), Times.Never);
    }

    [Fact]
    public async Task CreateWorkTaskCommand_EndBeforeStart()
    {
        var handler = new CreateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _userServiceMockRepo.Object, _mockAppLogger.Object, _emailSenderMock.Object);
        var command = new CreateWorkTaskCommand
        {
            Name = "WorkTask7",
            Description = "Description7",
            StatusId = 5,
            PriorityId = 5,
            StartDate = new DateTime(2024, 7, 10),
            EndDate = new DateTime(2024, 7, 1),
            AssignedPersonId = "1"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        _emailSenderMock.Verify(s => s.SendEmail(It.IsAny<Email>()), Times.Never);
    }
}
