using AutoMapper;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetWorkTaskPriorityDetails;
using TaskManager.Application.MappingProfilesh;
using TaskManager.Application.UnitTests.Mocks;

namespace TaskManager.Application.UnitTests.Features.WorkTasksPriorityTypes.Queries;

public class GetWorkTaskPriorityTypeDetailsQueryHandlerTests
{
    private readonly Mock<IWorkTaskPriorityTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetWorkTaskPriorityTypeDetailsQueryHandler>> _mockAppLogger;

    public GetWorkTaskPriorityTypeDetailsQueryHandlerTests()
    {
        _mockRepo = MockWorkTaskPriorityTypeRepository.GetMockWorkTaskPriorityTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskPriorityTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetWorkTaskPriorityTypeDetailsQueryHandler>>();
    }

    [Fact]
    public async Task GetWorkTaskPriorityTypeDetails()
    {
        var handler = new GetWorkTaskPriorityTypeDetailsQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var result = await handler.Handle(new GetWorkTaskPriorityTypeDetailsQuery(3), CancellationToken.None);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<WorkTaskPriorityTypeDetailsDto>();
        result.Id.ShouldBe(3);
    }

    [Fact]
    public async Task GetWorkTaskPriorityTypeDetails_NoExists()
    {
        var handler = new GetWorkTaskPriorityTypeDetailsQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
        await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(new GetWorkTaskPriorityTypeDetailsQuery(99), CancellationToken.None));
    }
}
