using AutoMapper;
using Moq;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.MappingProfilesh;
using TaskManager.Application.UnitTests.Mocks;
using TaskManager.Application.Features.WorkTask.Commands.DeleteWorkTask;
using Shouldly;
using MediatR;
using TaskManager.Application.Exceptions;

namespace TaskManager.Application.UnitTests.Features.WorkTaskss.Commands;

public class DeleteWorkTaskCommandHandlerTests
{
    private readonly Mock<IWorkTaskRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<DeleteWorkTaskCommandHandler>> _mockAppLogger;

    public DeleteWorkTaskCommandHandlerTests()
    {
        _mockRepo = MockWorkTaskRepository.GetMockWorkTaskRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<DeleteWorkTaskCommandHandler>>();
    }

    [Fact]
    public async Task DeleteWorkTaskCommand()
    {
        var handler = new DeleteWorkTaskCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var inintialItemsCount = _mockRepo.Object.GetAsync().Result.Count;
        var result = await handler.Handle(new DeleteWorkTaskCommand { Id = 3 }, CancellationToken.None);

        result.ShouldBe(Unit.Value);
        var listAfterdeletion = _mockRepo.Object.GetAsync().Result;
        listAfterdeletion.Count.ShouldBe(inintialItemsCount - 1);
        listAfterdeletion.ShouldNotContain(x => x.Id == 3);
    }

    [Fact]
    public async Task DeleteWorkTaskCommand_TargetNotExists()
    {
        var handler = new DeleteWorkTaskCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(new DeleteWorkTaskCommand { Id = 99 }, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteWorkTaskCommand_NoIndex()
    {
        var handler = new DeleteWorkTaskCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(new DeleteWorkTaskCommand(), CancellationToken.None));
    }
}
