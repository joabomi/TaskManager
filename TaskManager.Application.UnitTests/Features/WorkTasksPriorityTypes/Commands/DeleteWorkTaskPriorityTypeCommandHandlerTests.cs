using AutoMapper;
using Moq;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.MappingProfiles;
using TaskManager.Application.UnitTests.Mocks;
using TaskManager.Application.Features.WorkTaskPriorityType.Commands.DeleteWorkTaskPriorityType;
using Shouldly;
using MediatR;
using TaskManager.Application.Exceptions;

namespace TaskManager.Application.UnitTests.Features.WorkTasksPriorityTypes.Commands;

public class DeleteWorkTaskPriorityTypeCommandHandlerTests
{
    private readonly Mock<IWorkTaskPriorityTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<DeleteWorkTaskPriorityTypeCommandHandler>> _mockAppLogger;

    public DeleteWorkTaskPriorityTypeCommandHandlerTests()
    {
        _mockRepo = MockWorkTaskPriorityTypeRepository.GetMockWorkTaskPriorityTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskPriorityTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<DeleteWorkTaskPriorityTypeCommandHandler>>();
    }

    [Fact]
    public async Task DeleteWorkTaskPriorityTypeCommand()
    {
        var handler = new DeleteWorkTaskPriorityTypeCommandHandler(_mockRepo.Object, _mockAppLogger.Object);

        var inintialItemsCount = _mockRepo.Object.GetAsync().Result.Count;
        var result = await handler.Handle(new DeleteWorkTaskPriorityTypeCommand { Id = 3 }, CancellationToken.None);

        result.ShouldBe(Unit.Value);
        var listAfterdeletion = _mockRepo.Object.GetAsync().Result;
        listAfterdeletion.Count.ShouldBe(inintialItemsCount - 1);
        listAfterdeletion.ShouldNotContain(x => x.Id == 3);
    }

    [Fact]
    public async Task DeleteWorkTaskPriorityTypeCommand_TargetNotExists()
    {
        var handler = new DeleteWorkTaskPriorityTypeCommandHandler(_mockRepo.Object, _mockAppLogger.Object);

        await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(new DeleteWorkTaskPriorityTypeCommand { Id = 99 }, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteWorkTaskPriorityTypeCommand_NoIndex()
    {
        var handler = new DeleteWorkTaskPriorityTypeCommandHandler(_mockRepo.Object, _mockAppLogger.Object);

        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(new DeleteWorkTaskPriorityTypeCommand(), CancellationToken.None));
    }
}
