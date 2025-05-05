using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Application.MappingProfiles;
using TaskManager.Application.UnitTests.Mocks;

namespace TaskManager.Application.UnitTests.Features.WorkTasksStatusTypes.Queries;

public class GetAllWorkTaskStatusTypesQueryHandlerTests
{
    private readonly Mock<IWorkTaskStatusTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetAllWorkTaskStatusTypesQueryHandler>> _mockAppLogger;

    public GetAllWorkTaskStatusTypesQueryHandlerTests()
    {
        _mockRepo = MockWorkTaskStatusTypeRepository.GetMockWorkTaskStatusTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskStatusTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetAllWorkTaskStatusTypesQueryHandler>>();
    }

    [Fact]
    public async Task GetAllWorkTaskStatusTypesTest()
    {
        var handler = new GetAllWorkTaskStatusTypesQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var result = await handler.Handle(new GetAllWorkTaskStatusTypesQuery(), CancellationToken.None);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<WorkTaskStatusTypeDto>>();
        result.Count.ShouldBe(5);
    }
}
