using Moq;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Models.Identity;
using Xunit.Abstractions;

namespace TaskManager.Application.UnitTests.Mocks;

public class MockUserServiceRepository
{
    public static Mock<IUserService> GetMockUserServiceRepository()
    {
        var users = new List<TaskManagerUser>()
        {
            new TaskManagerUser
            {
                Id = "1",
                Email = "john@localhost.com",
                Firstname = "John",
                Lastname = "Doe"
            },
            new TaskManagerUser
            {
                Id = "2",
                Email = "jane@localhost.com",
                Firstname = "Jane",
                Lastname = "Doe"
            }
        };

        var mockRepo = new Mock<IUserService>();

        mockRepo.Setup(r => r.GetUsers()).ReturnsAsync(users);

        mockRepo.Setup(r => r.GetUser(It.IsAny<string>()))
            .Returns((string id) =>
            {
                var user = users.FirstOrDefault(w => w.Id == id);
                return Task.FromResult(user);
            });

        return mockRepo;
    }
}
