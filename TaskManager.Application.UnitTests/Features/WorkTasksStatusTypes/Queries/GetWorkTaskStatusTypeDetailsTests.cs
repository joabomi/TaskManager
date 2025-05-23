﻿using AutoMapper;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTask.Queries.GetWorkTaskDetails;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetWorkTaskStatusTypeDetails;
using TaskManager.Application.MappingProfiles;
using TaskManager.Application.UnitTests.Mocks;

namespace TaskManager.Application.UnitTests.Features.WorkTasksStatusTypes.Queries;

public class GetWorkTaskStatusTypeDetailsQueryHandlerTests
{
    private readonly Mock<IWorkTaskStatusTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetWorkTaskStatusTypeDetailsQueryHandler>> _mockAppLogger;

    public GetWorkTaskStatusTypeDetailsQueryHandlerTests()
    {
        _mockRepo = MockWorkTaskStatusTypeRepository.GetMockWorkTaskStatusTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskStatusTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetWorkTaskStatusTypeDetailsQueryHandler>>();
    }

    [Fact]
    public async Task GetWorkTaskStatusTypeDetails()
    {
        var handler = new GetWorkTaskStatusTypeDetailsQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var result = await handler.Handle(new GetWorkTaskStatusTypeDetailsQuery(3), CancellationToken.None);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<WorkTaskStatusTypeDetailsDto>();
        result.Id.ShouldBe(3);
    }

    [Fact]
    public async Task GetWorkTaskStatusTypeDetails_NoExists()
    {
        var handler = new GetWorkTaskStatusTypeDetailsQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
        await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(new GetWorkTaskStatusTypeDetailsQuery(99), CancellationToken.None));
    }
}
