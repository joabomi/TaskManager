using Microsoft.EntityFrameworkCore;
using Shouldly;
using TaskManager.Domain;
using TaskManager.Persistance.DatabaseContext;

namespace TaskManager.Persistance.IntegrationTests
{
    public class TaskManagerDatabaseContextTests
    {
        private TaskManagerDatabaseContext _taskManagerDatabaseContext;

        public TaskManagerDatabaseContextTests()
        {
            var dbOptions = new DbContextOptionsBuilder<TaskManagerDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _taskManagerDatabaseContext = new TaskManagerDatabaseContext(dbOptions);
        }

        [Fact]
        public async Task Save_SetDateCreateValueAsync()
        {
            var statusType = new WorkTaskStatusType
            {
                Id = 2,
                Name = "Created"
            };

            await _taskManagerDatabaseContext.WorkTaskStatusTypes.AddAsync(statusType);
            await _taskManagerDatabaseContext.SaveChangesAsync();

            statusType.CreationDate.ShouldNotBeNull();
        }

        [Fact]
        public async Task Save_SetDateModifiedValueAsync()
        {
            var statusType = new WorkTaskStatusType
            {
                Id = 2,
                Name = "Created"
            };

            await _taskManagerDatabaseContext.WorkTaskStatusTypes.AddAsync(statusType);
            await _taskManagerDatabaseContext.SaveChangesAsync();

            statusType.LastModificationDate.ShouldNotBeNull();

        }
    }
}