using AutoMapper;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTask.Queries.GetWorkTaskDetails;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Application.MappingProfiles;
using TaskManager.Application.UnitTests.Mocks;
using TaskManager.Domain;

namespace TaskManager.Application.UnitTests.Features.WorkTasks.Queries;

public class GetWorkTaskDetailsQueryHandlerTests
{
    private readonly Mock<IWorkTaskRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetWorkTaskDetailsQueryHandler>> _mockAppLogger;

    public GetWorkTaskDetailsQueryHandlerTests()
    {
        _mockRepo = MockWorkTaskRepository.GetMockWorkTaskRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskProfile>();
            c.AddProfile<WorkTaskStatusTypeProfile>();
            c.AddProfile<WorkTaskPriorityTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetWorkTaskDetailsQueryHandler>>();
    }

    [Fact]
    public async Task GetWorkTaskDetails()
    {
        var handler = new GetWorkTaskDetailsQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var result = await handler.Handle(new GetWorkTaskDetailsQuery(3), CancellationToken.None);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<WorkTaskDetailsDto>();
        result.Id.ShouldBe(3);

        Console.WriteLine(result.GetType());

        result.Priority.ShouldNotBeNull();
        result.Priority.ShouldBeOfType(typeof(WorkTaskPriorityTypeDto));
        result.Priority.Name.ShouldNotBeNull();
        result.Priority.Name.ShouldNotBeEmpty();

        result.Status.ShouldNotBeNull();
        result.Status.ShouldBeOfType(typeof(WorkTaskStatusTypeDto));
        result.Status.Name.ShouldNotBeNull();
        result.Status.Name.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetWorkTaskDetails_NoExists()
    {
        var handler = new GetWorkTaskDetailsQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
        await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(new GetWorkTaskDetailsQuery(99), CancellationToken.None));
    }
}
