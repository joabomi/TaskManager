using Moq;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Domain;

namespace TaskManager.Application.UnitTests.Mocks;

public class MockWorkTaskRepository
{
    public static Mock<IWorkTaskRepository> GetMockWorkTaskRepository()
    {
        var workTasks = new List<WorkTask>()
        {
            new WorkTask
            {
                Id = 2,
                Name = "WorkTask2",
                Description = "Description2",
                StatusId = 2,
                PriorityId = 2,
                StartDate = new DateTime(2024, 2, 1),
                EndDate = new DateTime(2024, 2, 10),
                AssignedPersonId = "User2"
            },
            new WorkTask
            {
                Id = 3,
                Name = "WorkTask3",
                Description = "Description3",
                StatusId = 3,
                PriorityId = 3,
                StartDate = new DateTime(2024, 3, 1),
                EndDate = new DateTime(2024, 3, 10),
                AssignedPersonId = "User3"
            },
            new WorkTask
            {
                Id = 4,
                Name = "WorkTask4",
                Description = "Description4",
                StatusId = 4,
                PriorityId = 4,
                StartDate = new DateTime(2024, 4, 1),
                EndDate = new DateTime(2024, 4, 10),
                AssignedPersonId = "User4"
            },
        };
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

        var mockRepo = new Mock<IWorkTaskRepository>();

        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(workTasks);

        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Returns((int id) =>
            {
                var workTask = workTasks.FirstOrDefault(w => w.Id == id);
                SetWorkTaskDetails(workTaskPriorityTypes, workTaskStatusTypes, workTask);
                return Task.FromResult(workTask);
            });

        mockRepo.Setup(r => r.CreateAsync(It.IsAny<WorkTask>()))
            .Returns((WorkTask workTask) =>
            {
                var newItemId = workTasks.Select(w => w.Id).Max() + 1;
                workTask.Id = newItemId;
                SetWorkTaskDetails(workTaskPriorityTypes, workTaskStatusTypes, workTask);
                workTasks.Add(workTask);
                return Task.FromResult(workTask.Id);
            });

        mockRepo.Setup(r => r.UpdateAsync(It.IsAny<WorkTask>()))
            .Returns((WorkTask workTask) =>
            {
                var itemToUpdate = workTasks.FirstOrDefault(item => item.Id == workTask.Id);
                if (itemToUpdate != null)
                {
                    itemToUpdate.Name = workTask.Name;
                    itemToUpdate.Description = workTask.Description;
                    itemToUpdate.StatusId = workTask.StatusId;
                    itemToUpdate.PriorityId = workTask.PriorityId;
                    itemToUpdate.StartDate = workTask.StartDate;
                    itemToUpdate.EndDate = workTask.EndDate;
                    itemToUpdate.AssignedPersonId = workTask.AssignedPersonId;
                    SetWorkTaskDetails(workTaskPriorityTypes, workTaskStatusTypes, workTask);
                }

                return Task.CompletedTask;
            });

        mockRepo.Setup(r => r.DeleteAsync(It.IsAny<WorkTask>()))
            .Returns((WorkTask workTask) =>
            {
                workTasks.RemoveAll(w => w.Id == workTask.Id);
                return Task.CompletedTask;
            });

        mockRepo.Setup(r => r.GetWorkTaskWithDetails(It.IsAny<int>()))
        .Returns((Delegate)((int id) =>
        {
            WorkTask? workTask = workTasks.FirstOrDefault(w => w.Id == id);
            SetWorkTaskDetails(workTaskPriorityTypes, workTaskStatusTypes, workTask);
            return Task.FromResult(workTask);
        }));

        mockRepo.Setup(r => r.GetWorkTasksWithDetails())
        .Returns(() =>
        {
            SetWorkTasksListDetails(workTaskPriorityTypes, workTaskStatusTypes, workTasks);
            return Task.FromResult(workTasks);
        });

        mockRepo.Setup(r => r.GetWorkTasksWithDetails(It.IsAny<string>()))
        .Returns((string userId) =>
        {
            var newList = workTasks.Where(w => w.AssignedPersonId == userId);
            SetWorkTasksListDetails(workTaskPriorityTypes, workTaskStatusTypes, workTasks);
            return Task.FromResult(newList.ToList());
        });

        return mockRepo;
    }

    private static void SetWorkTasksListDetails(List<WorkTaskPriorityType> workTaskPriorityTypes, List<WorkTaskStatusType> workTaskStatusTypes, List<WorkTask> workTasks)
    {
        if (workTasks != null && workTasks.Count > 0)
        {
            foreach (var workTask in workTasks)
            {
                SetWorkTaskDetails(workTaskPriorityTypes, workTaskStatusTypes, workTask);
            }
        }
    }

    private static void SetWorkTaskDetails(List<WorkTaskPriorityType> workTaskPriorityTypes, List<WorkTaskStatusType> workTaskStatusTypes, WorkTask workTask)
    {
        if (workTask != null)
        {
            var status = workTaskStatusTypes.FirstOrDefault(s => s.Id == workTask.StatusId);
            if (status != null)
                workTask.Status = status;
            var priority = workTaskPriorityTypes.FirstOrDefault(s => s.Id == workTask.PriorityId);
            if (priority != null)
                workTask.Priority = priority;
        }
    }
}
