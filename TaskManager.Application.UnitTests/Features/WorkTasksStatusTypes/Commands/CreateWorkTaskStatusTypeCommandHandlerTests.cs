using AutoMapper;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.CreateWorkTaskStatusType;
using TaskManager.Application.MappingProfiles;
using TaskManager.Application.UnitTests.Mocks;

namespace TaskManager.Application.UnitTests.Features.WorkTasksStatusTypes.Commands;

public class CreateWorkTaskStatusTypeCommandHandlerTests
{
    private readonly Mock<IWorkTaskStatusTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<CreateWorkTaskStatusTypeCommandHandler>> _mockAppLogger;

    public CreateWorkTaskStatusTypeCommandHandlerTests()
    {
        _mockRepo = MockWorkTaskStatusTypeRepository.GetMockWorkTaskStatusTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskStatusTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<CreateWorkTaskStatusTypeCommandHandler>>();
    }

    [Fact]
    public async Task CreateWorkTaskStatusTypeCommand()
    {
        var handler = new CreateWorkTaskStatusTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var inintialItemsCount = _mockRepo.Object.GetAsync().Result.Count;
        CreateWorkTaskStatusTypeCommand newItem = new CreateWorkTaskStatusTypeCommand
        {
            Name = "Test_Item"
        };
        var result = await handler.Handle(newItem, CancellationToken.None);

        result.ShouldBe(7);
        _mockRepo.Object.GetAsync().Result.Count.ShouldBe(inintialItemsCount + 1);

        //added item validation
        var addedItem = _mockRepo.Object.GetAsync().Result.Last();
        addedItem.Name.ShouldBe("Test_Item");
        addedItem.Id.ShouldBe(7);
    }

    [Fact]
    public async Task CreateWorkTaskStatusTypeCommand_RepeatedRecordTry()
    {
        var handler = new CreateWorkTaskStatusTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        CreateWorkTaskStatusTypeCommand newItem = new CreateWorkTaskStatusTypeCommand
        {
            Name = "Completed"//It is an existing name
        };

        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(newItem, CancellationToken.None));
    }
}
