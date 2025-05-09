using Moq;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Domain;
using TaskManager.Domain.Common;

namespace TaskManager.Application.UnitTests.Mocks;

public class MockWorkTaskStatusTypeRepository
{
    public static Mock<IWorkTaskStatusTypeRepository> GetMockWorkTaskStatusTypeRepository()
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

        var pagedWorkTasks = new PagedResult<WorkTaskStatusType>
        {
            TotalCount = workTaskStatusTypes.Count,
            PageNumber = 1,
            PageSize = 10,
            Items = workTaskStatusTypes
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
            .Returns((WorkTaskStatusType id) =>
            {
                workTaskStatusTypes.RemoveAll(w => w.Id == id.Id);
                return Task.CompletedTask;
            });

        mockRepo.Setup(r => r.IsWorkStatusTypeUnique(It.IsAny<string>()))
            .Returns((string name) =>
            {
                bool res = !workTaskStatusTypes.Any(w => w.Name == name);
                return Task.FromResult(res);
            });

        mockRepo.Setup(r => r.GetPagedAsync(It.IsAny<GetAllWorkTaskStatusTypesQuery>()))
        .ReturnsAsync((GetAllWorkTaskStatusTypesQuery query) =>
        {
            var filtered = workTaskStatusTypes.AsQueryable();
            return BaseRepositoryFeatures(query, ref filtered);
        });


        return mockRepo;
    }

    private static PagedResult<WorkTaskStatusType> BaseRepositoryFeatures(GetAllWorkTaskStatusTypesQuery query,ref IQueryable<WorkTaskStatusType> filtered)
    {
        // Apply filters:
        if (!string.IsNullOrEmpty(query.Name_Filter))
            filtered = filtered.Where(w => w.Name.Contains(query.Name_Filter));

        //Apply sorting:
        if (query.SortBy == "Name")
        {
            filtered = query.SortDescending ? filtered.OrderByDescending(w => w.Name) : filtered.OrderBy(w => w.Name);
        }

        // Total before pagination
        var total = filtered.Count();

        // Apply pagination
        var items = filtered
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return new PagedResult<WorkTaskStatusType>
        {
            TotalCount = total,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            Items = items
        };
    }
}
