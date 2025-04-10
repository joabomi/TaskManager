using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Domain;

namespace TaskManager.Application.UnitTests.Mocks;

public class MockWorkTaskPriorityTypeRepository
{
    public static Mock<IWorkTaskPriorityTypeRepository> GetMockWorkTaskPriorityTypeRespository()
    {
        var workTaskPriorityTypes = new List<WorkTaskPriorityType>()
        {
            new WorkTaskPriorityType
            {
                Id = 2,
                Name = "Lowest",
                PriorityWeight = 200,
            },
            new WorkTaskPriorityType
            {
                Id = 3,
                Name = "Low",
                PriorityWeight = 300,
            },
            new WorkTaskPriorityType
            {
                Id = 4,
                Name = "Normal",
                PriorityWeight = 400,
            },
            new WorkTaskPriorityType
            {
                Id = 5,
                Name = "High",
                PriorityWeight = 500,
            },
            new WorkTaskPriorityType
            {
                Id = 6,
                Name = "Higher",
                PriorityWeight = 600,
            }
        };

        var mockRepo = new Mock<IWorkTaskPriorityTypeRepository>();

        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(workTaskPriorityTypes);

        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Returns((int id) =>
            {
                var workTaskPriorityType = workTaskPriorityTypes.FirstOrDefault(w => w.Id == id);
                return Task.FromResult(workTaskPriorityType);
            });

        mockRepo.Setup(r => r.CreateAsync(It.IsAny<WorkTaskPriorityType>()))
            .Returns((WorkTaskPriorityType workTaskPriorityType) =>
            {
                var newItemId = workTaskPriorityTypes.Select(w => w.Id).Max() + 1;
                workTaskPriorityType.Id = newItemId;
                workTaskPriorityTypes.Add(workTaskPriorityType);
                return Task.FromResult(workTaskPriorityType.Id);
            });

        mockRepo.Setup(r => r.UpdateAsync(It.IsAny<WorkTaskPriorityType>()))
            .Returns((WorkTaskPriorityType workTaskPriorityType) =>
            {
                var itemToUpdate = workTaskPriorityTypes.FirstOrDefault(type => type.Id == workTaskPriorityType.Id);
                if (itemToUpdate != null)
                {
                    itemToUpdate.Name = workTaskPriorityType.Name;
                    itemToUpdate.PriorityWeight = workTaskPriorityType.PriorityWeight;
                }
                return Task.CompletedTask;
            });

        mockRepo.Setup(r => r.DeleteAsync(It.IsAny<WorkTaskPriorityType>()))
            .Returns((WorkTaskPriorityType workTaskPriorityType) =>
            {
                workTaskPriorityTypes.RemoveAll(w => w.Id == workTaskPriorityType.Id);
                return Task.CompletedTask;
            });

        mockRepo.Setup(r => r.IsWorkPriorityTypeUnique(It.IsAny<string>(), It.IsAny<int>()))
            .Returns((string name, int weight) =>
            {
                bool res = !workTaskPriorityTypes.Any(w => w.Name == name || w.PriorityWeight == weight);
                return Task.FromResult(res);
            });

        return mockRepo;
    }
}
