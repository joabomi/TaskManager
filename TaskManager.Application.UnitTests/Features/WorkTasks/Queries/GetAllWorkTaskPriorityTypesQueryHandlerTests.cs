
using Shouldly;
using AutoMapper;
using Moq;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;
using TaskManager.Application.MappingProfiles;
using TaskManager.Application.UnitTests.Mocks;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Identity.Services;

namespace TaskManager.Application.UnitTests.Features.WorkTasks.Queries;

public class GetAllWorkTasksQueryHandlerTests
{
    private readonly Mock<IWorkTaskRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetAllWorkTasksQueryHandler>> _mockAppLogger;
    private readonly Mock<IUserService> _mockUserService;

    public GetAllWorkTasksQueryHandlerTests()
    {
        _mockRepo = MockWorkTaskRepository.GetMockWorkTaskRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskProfile>();
            c.AddProfile<WorkTaskStatusTypeProfile>();
            c.AddProfile<WorkTaskPriorityTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetAllWorkTasksQueryHandler>>();
        _mockUserService = MockUserServiceRepository.GetMockUserServiceRepository();
    }

    [Fact]
    public async Task GetAllWorkTasksTest()
    {
        var handler = new GetAllWorkTasksQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object, _mockUserService.Object);

        var result = await handler.Handle(new GetAllWorkTasksQuery(false, false), CancellationToken.None);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<WorkTaskDto>>();
        result.Count.ShouldBe(3);

        foreach (var item in result)
        {
            item.Priority.ShouldNotBeNull();
            item.Priority.ShouldBeOfType(typeof(WorkTaskPriorityTypeDto));
            item.Priority.Name.ShouldNotBeNull();
            item.Priority.Name.ShouldNotBeEmpty();
            
            item.Status.ShouldNotBeNull();
            item.Status.ShouldBeOfType(typeof(WorkTaskStatusTypeDto));
            item.Status.Name.ShouldNotBeNull();
            item.Status.Name.ShouldNotBeEmpty();
        }
    }
}
