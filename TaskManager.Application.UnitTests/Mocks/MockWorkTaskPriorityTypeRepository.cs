using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;
using TaskManager.Domain;
using TaskManager.Domain.Common;

namespace TaskManager.Application.UnitTests.Mocks;

public class MockWorkTaskPriorityTypeRepository
{
    public static Mock<IWorkTaskPriorityTypeRepository> GetMockWorkTaskPriorityTypeRepository()
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

        var pagedWorkTasks = new PagedResult<WorkTaskPriorityType>
        {
            TotalCount = workTaskPriorityTypes.Count,
            PageNumber = 1,
            PageSize = 10,
            Items = workTaskPriorityTypes
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

        mockRepo.Setup(r => r.IsWorkPriorityTypeUpdateValid(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns((string name, int weight, int id) =>
            {
                bool res = !workTaskPriorityTypes.Any(w => (w.Name == name || w.PriorityWeight == weight) && w.Id != id);
                return Task.FromResult(res);
            });

        mockRepo.Setup(r => r.GetPagedAsync(It.IsAny<GetAllWorkTaskPriorityTypesQuery>()))
        .ReturnsAsync((GetAllWorkTaskPriorityTypesQuery query) =>
        {
            var filtered = workTaskPriorityTypes.AsQueryable();
            return BaseRepositoryFeatures(query, ref filtered);
        });


        return mockRepo;
    }

    private static PagedResult<WorkTaskPriorityType> BaseRepositoryFeatures(GetAllWorkTaskPriorityTypesQuery query, ref IQueryable<WorkTaskPriorityType> filtered)
    {
        // Apply filters:
        if (!string.IsNullOrEmpty(query.Name_Filter))
            filtered = filtered.Where(w => w.Name.Contains(query.Name_Filter));
        else if(query.MinWeight_Filter != null)
            filtered = filtered.Where(w => w.PriorityWeight >= query.MinWeight_Filter);
        else if (query.MaxWeight_Filter != null)
            filtered = filtered.Where(w => w.PriorityWeight <= query.MaxWeight_Filter);

        //Apply sorting:
        if (query.SortBy == "Name")
        {
            filtered = query.SortDescending ? filtered.OrderByDescending(w => w.Name) : filtered.OrderBy(w => w.Name);
        }
        if(query.SortBy == "PriorityWeight")
        {
            filtered = query.SortDescending ? filtered.OrderByDescending(w => w.PriorityWeight) : filtered.OrderBy(w => w.PriorityWeight);
        }

        // Total before pagination
        var total = filtered.Count();

        // Apply pagination
        var items = filtered
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return new PagedResult<WorkTaskPriorityType>
        {
            TotalCount = total,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            Items = items
        };
    }
}
