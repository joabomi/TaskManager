using AutoMapper;
using Moq;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.MappingProfilesh;
using TaskManager.Application.UnitTests.Mocks;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.DeleteWorkTaskStatusType;
using Shouldly;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.CreateWorkTaskStatusType;
using MediatR;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.UpdateWorkTaskStatusType;

namespace TaskManager.Application.UnitTests.Features.WorkTasksStatusTypes.Commands;

public class DeleteWorkTaskStatusTypeCommandHandlerTests
{
    private readonly Mock<IWorkTaskStatusTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<DeleteWorkTaskStatusTypeCommandHandler>> _mockAppLogger;

    public DeleteWorkTaskStatusTypeCommandHandlerTests()
    {
        _mockRepo = MockWorkTaskStatusTypeRepository.GetMockWorkTaskStatusTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskStatusTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<DeleteWorkTaskStatusTypeCommandHandler>>();
    }

    [Fact]
    public async Task DeleteWorkTaskStatusTypeCommand()
    {
        var handler = new DeleteWorkTaskStatusTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var inintialItemsCount = _mockRepo.Object.GetAsync().Result.Count;
        var result = await handler.Handle(new DeleteWorkTaskStatusTypeCommand { Id = 3}, CancellationToken.None);

        result.ShouldBe(Unit.Value);
        var listAfterdeletion = _mockRepo.Object.GetAsync().Result;
        listAfterdeletion.Count.ShouldBe(inintialItemsCount - 1);
        listAfterdeletion.ShouldNotContain(x => x.Id == 3);
    }

    [Fact]
    public async Task DeleteWorkTaskStatusTypeCommand_TargetNotExists()
    {
        var handler = new DeleteWorkTaskStatusTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(new DeleteWorkTaskStatusTypeCommand { Id = 99 }, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteWorkTaskStatusTypeCommand_NoIndex()
    {
        var handler = new DeleteWorkTaskStatusTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(new DeleteWorkTaskStatusTypeCommand(), CancellationToken.None));
    }
}
