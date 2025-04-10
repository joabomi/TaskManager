using AutoMapper;
using MediatR;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTaskPriorityType.Commands.UpdateWorkTaskPriorityType;
using TaskManager.Application.MappingProfilesh;
using TaskManager.Application.UnitTests.Mocks;

namespace TaskManager.Application.UnitTests.Features.WorkTasksPriorityTypes.Commands;

public class UpdateWorkTaskPriorityTypeCommandHandlerTests
{
    private readonly Mock<IWorkTaskPriorityTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<UpdateWorkTaskPriorityTypeCommandHandler>> _mockAppLogger;

    public UpdateWorkTaskPriorityTypeCommandHandlerTests()
    {
        _mockRepo = MockWorkTaskPriorityTypeRepository.GetMockWorkTaskPriorityTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskPriorityTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<UpdateWorkTaskPriorityTypeCommandHandler>>();
    }

    [Fact]
    public async Task UpdateWorkTaskPriorityTypeCommand()
    {
        var handler = new UpdateWorkTaskPriorityTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var inintialItemsCount = _mockRepo.Object.GetAsync().Result.Count;
        UpdateWorkTaskPriorityTypeCommand editedItem = new UpdateWorkTaskPriorityTypeCommand
        {
            Id = 2,
            Name = "Lowest_Modified",
            PriorityWeight = 150,
        };
        var result = await handler.Handle(editedItem, CancellationToken.None);

        result.ShouldBe(Unit.Value);
        _mockRepo.Object.GetAsync().Result.Count.ShouldBe(inintialItemsCount);
        _mockRepo.Object.GetByIdAsync(2).Result.Name.ShouldBe(editedItem.Name);
        _mockRepo.Object.GetByIdAsync(2).Result.PriorityWeight.ShouldBe(editedItem.PriorityWeight);
    }

    [Fact]
    public async Task UpdateWorkTaskPriorityTypeCommand_TargetNotExists()
    {
        var handler = new UpdateWorkTaskPriorityTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var inintialItemsCount = _mockRepo.Object.GetAsync().Result.Count;
        UpdateWorkTaskPriorityTypeCommand editedItem = new UpdateWorkTaskPriorityTypeCommand
        {
            Id = 99,
            Name = "Lowest_Modified",
            PriorityWeight = 150,
        };

        await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(editedItem, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateWorkTaskPriorityTypeCommand_NoIndex()
    {
        var handler = new UpdateWorkTaskPriorityTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
        var command = new UpdateWorkTaskPriorityTypeCommand 
        { 
            Name = "Lowest_Modified", 
            PriorityWeight = 150,
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(new UpdateWorkTaskPriorityTypeCommand { Name = " Created_Modified" }, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateWorkTaskPriorityTypeCommand_BadRequest()
    {
        var handler = new UpdateWorkTaskPriorityTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
        var command1 = new UpdateWorkTaskPriorityTypeCommand
        {
            Id = 2,
            Name = "Lowest_Modified",
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command1, CancellationToken.None));

        var command2 = new UpdateWorkTaskPriorityTypeCommand
        {
            Id = 2,
            PriorityWeight = 150,
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command2, CancellationToken.None));

        var command3 = new UpdateWorkTaskPriorityTypeCommand
        {
            Id = 2,
            Name = "",
            PriorityWeight = 150,
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command3, CancellationToken.None));

        var command4 = new UpdateWorkTaskPriorityTypeCommand
        {
            Id = 2,
            Name = "Lowest_Modified",
            PriorityWeight = -100,
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command4, CancellationToken.None));

        var command5 = new UpdateWorkTaskPriorityTypeCommand
        {
            Id = 2,
            Name = "Lowest_Modified",
            PriorityWeight = 10000,
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command5, CancellationToken.None));

        var command6 = new UpdateWorkTaskPriorityTypeCommand
        {
            Id = 2,
            Name = "LargeName",
            PriorityWeight = 150,
        };
        for(int i= 0; i < 50; i++)
        {
            command6.Name += "--";
        }
        command6.Name.Length.ShouldBeGreaterThan(100);
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command6, CancellationToken.None));
    }
}
