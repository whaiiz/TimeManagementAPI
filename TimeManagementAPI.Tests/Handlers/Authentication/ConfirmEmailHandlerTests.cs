using Moq;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Handlers.Authentication;
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;
using TimeManagementAPI.Utils;
using Xunit;

namespace TimeManagementAPI.Tests.Handlers.Authentication
{
    public class ConfirmEmailHandlerTests
    {
        [Fact]
        public async Task ConfirmEmail_InvalidEmailConfirmationToken()
        {
            // Arrange
            var taskRepository = new Mock<IUserRepository>();
            var confirmEmailHandler = new ConfirmEmailHandler(taskRepository.Object);
            var request = new ConfirmEmailCommand("token");

            taskRepository.Setup(m => m.GetByEmailConfirmationToken(It.IsAny<string>()))
                .Returns(Task.FromResult<UserModel>(null));

            // Act
           var result = await confirmEmailHandler.Handle(request, default);

            // Assert
            Assert.IsType<ResponseModel>(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("The token sent is invalid or it has already expired!", result.Message);
        }
    }
}
