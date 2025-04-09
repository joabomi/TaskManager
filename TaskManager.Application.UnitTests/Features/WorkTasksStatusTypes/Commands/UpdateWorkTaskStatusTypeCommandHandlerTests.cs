using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.CreateWorkTaskStatusType;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.UpdateWorkTaskStatusType;
using TaskManager.Application.MappingProfilesh;
using TaskManager.Application.UnitTests.Mocks;

namespace TaskManager.Application.UnitTests.Features.WorkTasksStatusTypes.Commands;

public class UpdateWorkTaskStatusTypeCommandHandlerTests
{
    private readonly Mock<IWorkTaskStatusTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<UpdateWorkTaskStatusTypeCommandHandler>> _mockAppLogger;

    public UpdateWorkTaskStatusTypeCommandHandlerTests()
    {
        _mockRepo = MockWorkTaskStatusTypeRepository.GetMockWorkTaskStatusTypeRespository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskStatusTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<UpdateWorkTaskStatusTypeCommandHandler>>();
    }

    [Fact]
    public async Task CreateWorkTaskStatusTypeCommand()
    {
        var handler = new UpdateWorkTaskStatusTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var inintialItemsCount = _mockRepo.Object.GetAsync().Result.Count;
        UpdateWorkTaskStatusTypeCommand editedItem = new UpdateWorkTaskStatusTypeCommand
        {
            Id = 2,
            Name = "Created_Modified",
        };
        var result = await handler.Handle(editedItem, CancellationToken.None);

        _mockRepo.Object.GetAsync().Result.Count.ShouldBe(inintialItemsCount);
        _mockRepo.Object.GetByIdAsync(2).Result.Name.ShouldBe(editedItem.Name);

        ////added item validation
        //var addedItem = _mockRepo.Object.GetAsync().Result.Last(); // El último elemento añadido
        //addedItem.Name.ShouldBe("Test_Item");
        //addedItem.Id.ShouldBe(7);
    }
    [Fact]
    public async Task CreateWorkTaskStatusTypeCommand_TargetNotExists()
    {
        var handler = new UpdateWorkTaskStatusTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var inintialItemsCount = _mockRepo.Object.GetAsync().Result.Count;
        UpdateWorkTaskStatusTypeCommand editedItem = new UpdateWorkTaskStatusTypeCommand
        {
            Id = 99,
            Name = "Created_Modified",
        };

        await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(editedItem, CancellationToken.None));
    }
}
