using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTask.Commands.CreateWorkTask;
using TaskManager.Domain;

namespace TaskManager.Application.UnitTests.Mocks;

public class MockWorkTaskStatusTypeRepository
{
    public static Mock<IWorkTaskStatusTypeRepository> GetMockWorkTaskStatusTypeRespository()
    {
        var workTaskStatusTypes = new List<WorkTaskStatusType>()
        {
            new WorkTaskStatusType
            {
                Id = 2,
                Name = "Created"
            },
            new WorkTaskStatusType
            {
                Id = 3,
                Name = "Assigned"

            },
            new WorkTaskStatusType
            {
                Id = 4,
                Name = "In_Progress"
            },
            new WorkTaskStatusType
            {
                Id = 5,
                Name = "Completed"
            },
            new WorkTaskStatusType
            {
                Id = 6,
                Name = "Canceled"
            }
        };
        
        var mockRepo = new Mock<IWorkTaskStatusTypeRepository>();
        
        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(workTaskStatusTypes);

        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Returns((int id) =>
            {
                var workTask = workTaskStatusTypes.FirstOrDefault(w => w.Id == id);
                return Task.FromResult(workTask);
            });

        mockRepo.Setup(r => r.CreateAsync(It.IsAny<WorkTaskStatusType>()))
            .Returns((WorkTaskStatusType workTaskStatusType) =>
            {
                var newItemId = workTaskStatusTypes.Select(w => w.Id).Max() + 1;
                workTaskStatusType.Id = newItemId;
                workTaskStatusTypes.Add(workTaskStatusType);
                return Task.FromResult(workTaskStatusType.Id);
            });

        mockRepo.Setup(r => r.UpdateAsync(It.IsAny<WorkTaskStatusType>()))
            .Returns((WorkTaskStatusType workTaskStatusType) =>
            {
                var itemToUpdate = workTaskStatusTypes.FirstOrDefault(type => type.Id == workTaskStatusType.Id);
                if (itemToUpdate != null)
                {
                    itemToUpdate.Name = workTaskStatusType.Name;
                    itemToUpdate.CreationDate = workTaskStatusType.CreationDate;
                    itemToUpdate.LastModificationDate = workTaskStatusType.LastModificationDate;
                }
                return Task.CompletedTask;
            });

        mockRepo.Setup(r => r.DeleteAsync(It.IsAny<WorkTaskStatusType>()))
            .Returns((WorkTaskStatusType workTaskStatusType) =>
            {
                workTaskStatusTypes.RemoveAll(w => w.Id == workTaskStatusType.Id);
                return Task.CompletedTask;
            });

        mockRepo.Setup(r => r.IsWorkStatusTypeUnique(It.IsAny<string>()))
            .Returns((string name) =>
            {
                bool res = !workTaskStatusTypes.Any(w => w.Name == name);
                return Task.FromResult(res);
            });
        
        return mockRepo;
    }
}
