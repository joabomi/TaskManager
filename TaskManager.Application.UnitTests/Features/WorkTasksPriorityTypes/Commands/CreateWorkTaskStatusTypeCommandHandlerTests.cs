using AutoMapper;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTaskPriorityType.Commands.CreateWorkTaskPriorityType;
using TaskManager.Application.MappingProfilesh;
using TaskManager.Application.UnitTests.Mocks;

namespace TaskManager.Application.UnitTests.Features.WorkTasksPriorityTypes.Commands;

public class CreateWorkTaskPriorityTypeCommandHandlerTests
{
    private readonly Mock<IWorkTaskPriorityTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<CreateWorkTaskPriorityTypeCommandHandler>> _mockAppLogger;

    public CreateWorkTaskPriorityTypeCommandHandlerTests()
    {
        _mockRepo = MockWorkTaskPriorityTypeRepository.GetMockWorkTaskPriorityTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskPriorityTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<CreateWorkTaskPriorityTypeCommandHandler>>();
    }

    [Fact]
    public async Task CreateWorkTaskPriorityTypeCommand()
    {
        var handler = new CreateWorkTaskPriorityTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var inintialItemsCount = _mockRepo.Object.GetAsync().Result.Count;
        CreateWorkTaskPriorityTypeCommand newItem = new CreateWorkTaskPriorityTypeCommand
        {
            Name = "Test_Item",
            PriorityWeight = 1
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
    public async Task CreateWorkTaskPriorityTypeCommand_RepeatedRecordTry()
    {
        var handler = new CreateWorkTaskPriorityTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        CreateWorkTaskPriorityTypeCommand newItem1 = new CreateWorkTaskPriorityTypeCommand
        {
            Name = "High",//It is an existing name
            PriorityWeight = 500 //It is an existing weight
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(newItem1, CancellationToken.None));

        CreateWorkTaskPriorityTypeCommand newItem2 = new CreateWorkTaskPriorityTypeCommand
        {
            Name = "High",//It is an existing name
            PriorityWeight = 1
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(newItem2, CancellationToken.None));

        CreateWorkTaskPriorityTypeCommand newItem3 = new CreateWorkTaskPriorityTypeCommand
        {
            Name = "High2",
            PriorityWeight = 500//It is an existing weight
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(newItem3, CancellationToken.None));
    }

    [Fact]
    public async Task CreateWorkTaskPriorityTypeCommand_BadData()
    {
        var handler = new CreateWorkTaskPriorityTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        CreateWorkTaskPriorityTypeCommand newItem1 = new CreateWorkTaskPriorityTypeCommand
        {
            Name = "",//It is an existing name
            PriorityWeight = 150 //It is an existing weight
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(newItem1, CancellationToken.None));

        CreateWorkTaskPriorityTypeCommand newItem2 = new CreateWorkTaskPriorityTypeCommand
        {
            PriorityWeight = 150
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(newItem2, CancellationToken.None));

        CreateWorkTaskPriorityTypeCommand newItem3 = new CreateWorkTaskPriorityTypeCommand
        {
            Name = "High2",
            PriorityWeight = 1500//It is an existing weight
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(newItem3, CancellationToken.None));

        CreateWorkTaskPriorityTypeCommand newItem4 = new CreateWorkTaskPriorityTypeCommand
        {
            Name = "High2",
            PriorityWeight = -1500//It is an existing weight
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(newItem3, CancellationToken.None));

        CreateWorkTaskPriorityTypeCommand newItem5 = new CreateWorkTaskPriorityTypeCommand
        {
            Name = "High2",
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(newItem5, CancellationToken.None));
    }
}
