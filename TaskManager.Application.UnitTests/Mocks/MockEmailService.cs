using Moq;
using TaskManager.Application.Contracts.Email;
using TaskManager.Application.Models.Email;
using TaskManager.Application.Models.Identity;

namespace TaskManager.Application.UnitTests.Mocks;

public class MockEmailService
{
    public static Mock<IEmailSender> GetMockEmailService()
    {
        var mockEmailSender = new Mock<IEmailSender>();

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

        mockEmailSender
            .Setup(s => s.SendEmail(It.IsAny<Email>()))
            .ReturnsAsync(true);

        return mockEmailSender;
    }
}
