using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
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
        _mockRepo = MockWorkTaskStatusTypeRepository.GetMockWorkTaskStatusTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WorkTaskStatusTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<UpdateWorkTaskStatusTypeCommandHandler>>();
    }

    [Fact]
    public async Task UpdateWorkTaskStatusTypeCommand()
    {
        var handler = new UpdateWorkTaskStatusTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var inintialItemsCount = _mockRepo.Object.GetAsync().Result.Count;
        UpdateWorkTaskStatusTypeCommand editedItem = new UpdateWorkTaskStatusTypeCommand
        {
            Id = 2,
            Name = "Created_Modified",
        };
        var result = await handler.Handle(editedItem, CancellationToken.None);

        result.ShouldBe(Unit.Value);
        _mockRepo.Object.GetAsync().Result.Count.ShouldBe(inintialItemsCount);
        _mockRepo.Object.GetByIdAsync(2).Result.Name.ShouldBe(editedItem.Name);
    }

    [Fact]
    public async Task UpdateWorkTaskStatusTypeCommand_TargetNotExists()
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

    [Fact]
    public async Task UpdateWorkTaskStatusTypeCommand_NoIndex()
    {
        var handler = new UpdateWorkTaskStatusTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(new UpdateWorkTaskStatusTypeCommand { Name = " Created_Modified" }, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateWorkTaskStatusTypeCommand_BadRequest()
    {
        var handler = new UpdateWorkTaskStatusTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
        var command1 = new UpdateWorkTaskStatusTypeCommand
        {
            Id = 2,
            Name = "",
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command1, CancellationToken.None));

        var command2 = new UpdateWorkTaskStatusTypeCommand
        {
            Id = 2,
        };
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command2, CancellationToken.None));

        var command3 = new UpdateWorkTaskStatusTypeCommand
        {
            Id = 2,
            Name = "LargeName",
        };
        for (int i = 0; i < 50; i++)
        {
            command3.Name += "--";
        }
        command3.Name.Length.ShouldBeGreaterThan(100);
        await Should.ThrowAsync<BadRequestException>(async () => await handler.Handle(command3, CancellationToken.None));
    }
}
