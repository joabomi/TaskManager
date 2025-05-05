using AutoMapper;
using MediatR;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTask.Commands.UpdateWorkTask;
using TaskManager.Application.MappingProfiles;
using TaskManager.Application.UnitTests.Mocks;

namespace TaskManager.Application.UnitTests.Features.WorkTasks.Commands;

public class UpdateWorkTaskCommandHandlerTests
{
    private readonly Mock<IWorkTaskRepository> _workTasksMockRepo;
    private readonly Mock<IWorkTaskStatusTypeRepository> _workTaskStatusTypesMockRepo;
    private readonly Mock<IWorkTaskPriorityTypeRepository> _workTaskPriorityTypesMockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<UpdateWorkTaskCommandHandler>> _mockAppLogger;

    public UpdateWorkTaskCommandHandlerTests()
    {
        _workTasksMockRepo = MockWorkTaskRepository.GetMockWorkTaskRepository();
        _workTaskStatusTypesMockRepo = MockWorkTaskStatusTypeRepository.GetMockWorkTaskStatusTypeRepository();
        _workTaskPriorityTypesMockRepo = MockWorkTaskPriorityTypeRepository.GetMockWorkTaskPriorityTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskProfile>();
            c.AddProfile<WorkTaskStatusTypeProfile>();
            c.AddProfile<WorkTaskProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<UpdateWorkTaskCommandHandler>>();
    }

    [Fact]
    public async Task UpdateWorkTaskCommand()
    {
        var handler = new UpdateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _mockAppLogger.Object);

        var inintialItemsCount = _workTasksMockRepo.Object.GetAsync().Result.Count;
        UpdateWorkTaskCommand editedItem = new UpdateWorkTaskCommand
        {
            Id = 3,
            Name = "WorkTask7",
            Description = "Description7",
            StatusId = 5,
            PriorityId = 5,
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2024, 7, 10),
            AssignedPersonId = "User7"
        };
        var result = await handler.Handle(editedItem, CancellationToken.None);

        result.ShouldBe(Unit.Value);
        _workTasksMockRepo.Object.GetAsync().Result.Count.ShouldBe(inintialItemsCount);
        _workTasksMockRepo.Object.GetByIdAsync(3).Result.Name.ShouldBe(editedItem.Name);
        ;
    }

    [Fact]
    public async Task UpdateWorkTaskCommand_TargetNotExists()
    {
        var handler = new UpdateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _mockAppLogger.Object);

        var inintialItemsCount = _workTasksMockRepo.Object.GetAsync().Result.Count;
        UpdateWorkTaskCommand editedItem = new UpdateWorkTaskCommand
        {
            Id = 99,
            Name = "WorkTask7",
            Description = "Description7",
            StatusId = 5,
            PriorityId = 5,
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2024, 7, 10),
            AssignedPersonId = "User7"
        };

        await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(editedItem, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateWorkTaskCommand_NoIndex()
    {
        var handler = new UpdateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _mockAppLogger.Object);
        var command = new UpdateWorkTaskCommand
        {
            Name = "WorkTask7",
            Description = "Description7",
            StatusId = 5,
            PriorityId = 5,
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2024, 7, 10),
            AssignedPersonId = "User7"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(new UpdateWorkTaskCommand { Name = " Created_Modified" }, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateWorkTaskCommand_BadName()
    {
        var handler = new UpdateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _mockAppLogger.Object);
        var command = new UpdateWorkTaskCommand
        {
            Id = 2,
            Description = "Description7",
            StatusId = 5,
            PriorityId = 5,
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2024, 7, 10),
            AssignedPersonId = "User7"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));

        command.Name = "";
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));

        for (int i = 0; i < 51; i++)
        {
            command.Name += "--";
        }
        command.Name.Length.ShouldBeGreaterThan(100);
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateWorkTaskCommand_BadStatus()
    {
        var handler = new UpdateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _mockAppLogger.Object);
        var command = new UpdateWorkTaskCommand
        {
            Id = 2,
            Name = "WorkTask7",
            Description = "Description7",
            PriorityId = 5,
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2024, 7, 10),
            AssignedPersonId = "User7"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));

        command.StatusId = 99;
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateWorkTaskCommand_BadPriority()
    {
        var handler = new UpdateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _mockAppLogger.Object);
        var command = new UpdateWorkTaskCommand
        {
            Id = 2,
            Name = "WorkTask7",
            Description = "Description7",
            StatusId = 5,
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2024, 7, 10),
            AssignedPersonId = "User7"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));

        command.PriorityId = 99;
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateWorkTaskCommand_BadStartDate()
    {
        var handler = new UpdateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _mockAppLogger.Object);
        var command = new UpdateWorkTaskCommand
        {
            Id = 2,
            Name = "WorkTask7",
            Description = "Description7",
            StatusId = 5,
            PriorityId = 5,
            EndDate = new DateTime(2024, 7, 10),
            AssignedPersonId = "User7"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));

        command.StartDate = new DateTime();
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateWorkTaskCommand_BadEndDate()
    {
        var handler = new UpdateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _mockAppLogger.Object);
        var command = new UpdateWorkTaskCommand
        {
            Id = 2,
            Name = "WorkTask7",
            Description = "Description7",
            StatusId = 5,
            PriorityId = 5,
            StartDate = new DateTime(2024, 7, 1),
            AssignedPersonId = "User7"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));

        command.EndDate = new DateTime();
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateWorkTaskCommand_EndBeforeStart()
    {
        var handler = new UpdateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _mockAppLogger.Object);
        var command = new UpdateWorkTaskCommand
        {
            Id = 2,
            Name = "WorkTask7",
            Description = "Description7",
            StatusId = 5,
            PriorityId = 5,
            StartDate = new DateTime(2024, 7, 10),
            EndDate = new DateTime(2024, 7, 1),
            AssignedPersonId = "User7"
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    //[Fact]
    //public async Task UpdateWorkTaskCommand_NoAssignedPerson()
    //{
    //    var handler = new UpdateWorkTaskCommandHandler(_mapper, _workTasksMockRepo.Object, _workTaskPriorityTypesMockRepo.Object, _workTaskStatusTypesMockRepo.Object, _mockAppLogger.Object);
    //    var command = new UpdateWorkTaskCommand
    //    {
    //        Id = 2,
    //        Name = "WorkTask7",
    //        Description = "Description7",
    //        StatusId = 5,
    //        PriorityId = 5,
    //        StartDate = new DateTime(2024, 7, 1),
    //        EndDate = new DateTime(2024, 7, 10),
    //    };
    //    await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
    //}
}
