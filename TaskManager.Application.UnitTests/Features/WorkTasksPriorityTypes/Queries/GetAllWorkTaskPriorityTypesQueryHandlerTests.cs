using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using AutoMapper;
using Moq;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Application.MappingProfilesh;
using TaskManager.Application.UnitTests.Mocks;

namespace TaskManager.Application.UnitTests.Features.WorkTasksPriorityTypes.Queries;

public class GetAllWorkTaskPriorityTypesQueryHandlerTests
{
    private readonly Mock<IWorkTaskPriorityTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetAllWorkTaskPriorityTypesQueryHandler>> _mockAppLogger;

    public GetAllWorkTaskPriorityTypesQueryHandlerTests()
    {
        _mockRepo = MockWorkTaskPriorityTypeRepository.GetMockWorkTaskPriorityTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskPriorityTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetAllWorkTaskPriorityTypesQueryHandler>>();
    }

    [Fact]
    public async Task GetAllWorkTaskPriorityTypesTest()
    {
        var handler = new GetAllWorkTaskPriorityTypesQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var result = await handler.Handle(new GetAllWorkTaskPriorityTypesQuery(), CancellationToken.None);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<WorkTaskPriorityTypeDto>>();
        result.Count.ShouldBe(5);
    }
}
